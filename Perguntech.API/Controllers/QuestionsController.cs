using Microsoft.AspNetCore.Mvc;
using Perguntech.Core.Entities;
using Perguntech.Services;

namespace Perguntech.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly QuestionService _service;

        public QuestionsController(QuestionService service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetAllQuestions() => Ok(await _service.GetAllQuestionsAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestionById(Guid id)
        {
            var question = await _service.GetQuestionByIdAsync(id);
            if (question == null)
                return NotFound($"No question found with ID {id}");

            return Ok(question);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(Guid id, Question question)
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
        public async Task<ActionResult<Category>> GetCategoryByName(string name, CancellationToken cancellationToken)
        {
            var category = await _service.GetCategoryByNameAsync(name, cancellationToken);

            if (category == null)
                return NotFound($"No category found with name {name}");

            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion([FromBody] CreateQuestionModel model, CancellationToken cancellationToken)
        {
            var categoryIds = new List<string>();

            foreach (var categoryName in model.CategoryNames)
            {
                var category = await _service.GetCategoryByNameAsync(categoryName, cancellationToken);
                if (category == null)
                {
                    category = await _service.CreateCategoryAsync(categoryName);
                }
                categoryIds.Add(category.Id.ToString());
            }

            model.Question.CategoryIds = categoryIds;
            await _service.AddOrUpdateQuestionAsync(model.Question);
            return CreatedAtAction(nameof(GetQuestionById), new { id = model.Question.Id }, model.Question);
        }

    }

}
