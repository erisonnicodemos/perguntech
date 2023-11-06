using Microsoft.AspNetCore.Mvc;
using Perguntech.Core.Domain;
using Perguntech.Services;

namespace Perguntech.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly QuestionService _service;
        private readonly RedisService _redisService;

        public QuestionsController(QuestionService questionService, RedisService redisService)
        {
            _service = questionService ?? throw new ArgumentNullException(nameof(questionService));
            _redisService = redisService ?? throw new ArgumentNullException(nameof(redisService));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionDomain>>> GetAllQuestions() => Ok(await _service.GetAllQuestionsAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionDomain>> GetQuestionById(Guid id)
        {
            var question = await _service.GetQuestionByIdAsync(id);
            if (question == null)
                return NotFound($"No question found with ID {id}");

            return Ok(question);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(Guid id, QuestionDomain question)
        {
            if (id != question.Id)
                return BadRequest("ID mismatch between URL and body.");

            await _service.AddOrUpdateQuestionAsync(question);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(Guid id)
        {
            await _service.DeleteQuestionAsync(id);
            return Ok();
        }

        [HttpGet("categories/{name}")]
        public async Task<ActionResult<CategoryDomain>> GetCategoryByName(string name, CancellationToken cancellationToken)
        {
            var category = await _service.GetCategoryByNameAsync(name, cancellationToken);

            if (category == null)
                return NotFound($"No category found with name {name}");

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion([FromBody] CreateQuestionDomain model, CancellationToken cancellationToken)
        {
            var categoryIds = new List<Guid>();

            foreach (var categoryName in model.CategoryNames)
            {
                var category = await _service.GetCategoryByNameAsync(categoryName, cancellationToken);
                if (category == null)
                {
                    category = await _service.CreateCategoryAsync(categoryName);
                }
                categoryIds.Add(category.Id);
            }

            model.Question.CategoryIds = categoryIds;
            await _service.AddOrUpdateQuestionAsync(model.Question);
            return CreatedAtAction(nameof(GetQuestionById), new { id = model.Question.Id }, model.Question);
        }

        [HttpGet("questions/{title}")]
        public async Task<ActionResult<IEnumerable<QuestionDomain>>> FindQuestion(string title)
        {
            var cachedQuestions = await _redisService.GetObjectAsync<List<QuestionDomain>>(title);

            if (cachedQuestions != null && cachedQuestions.Any())
                return Ok(cachedQuestions);

            var questionsFromDb = await _service.GetQuestionsByTitleAsync(title);

            if (!questionsFromDb.Any())
                return NotFound($"No questions with the title '{title}' were found.");

            await _redisService.SetObjectAsync(title, questionsFromDb);

            return Ok(questionsFromDb);
        }


    }

}
