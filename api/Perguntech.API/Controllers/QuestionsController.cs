using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Perguntech.API.DTO;
using Perguntech.Core.Domain;
using Perguntech.Services;

namespace Perguntech.API.Controllers
{
    [ApiController]
    [Route("questions")]
    public class QuestionsController : ControllerBase
    {
        private readonly QuestionService _service;
        private readonly RedisService _redisService;
        private readonly IMapper _mapper;

        public QuestionsController(QuestionService questionService, RedisService redisService, IMapper mapper)
        {
            _service = questionService ?? throw new ArgumentNullException(nameof(questionService));
            _redisService = redisService ?? throw new ArgumentNullException(nameof(redisService));
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<QuestionDomain>>> GetAllQuestions() => Ok(await _service.GetAllQuestionsAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionDomain>> GetQuestionById(Guid id)
        {
            var question = await _service.GetQuestionByIdAsync(id);
            if (question == null)
                return NotFound($"No question found with ID {id}");

            return Ok(question);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateQuestion(Guid id, [FromBody] QuestionDto questionDto)
        {

            var questionDomain = _mapper.Map<QuestionDomain>(questionDto);
            await _service.AddOrUpdateQuestionAsync(questionDomain);
            var updatedQuestionDto = _mapper.Map<QuestionDto>(questionDomain);

            return Ok(updatedQuestionDto);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(Guid id)
        {
            await _service.DeleteQuestionAsync(id);
            return Ok();
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddQuestion([FromBody] QuestionDto questionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var questionDomain = _mapper.Map<QuestionDomain>(questionDto);
            await _service.AddOrUpdateQuestionAsync(questionDomain);

            var createdQuestionDto = _mapper.Map<QuestionDto>(questionDomain);

            return CreatedAtAction(nameof(GetQuestionById), new { id = questionDomain.Id }, createdQuestionDto);
        }

        [HttpGet("search/{title}")]
        public async Task<ActionResult<IEnumerable<QuestionDomain>>> FindQuestion(string title)
        {

            if (title.Length < 3)
            {
                return BadRequest("The search term must be at least 3 words");
            }

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
