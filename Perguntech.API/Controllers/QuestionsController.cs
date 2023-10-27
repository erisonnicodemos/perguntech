using Microsoft.AspNetCore.Mvc;
using Perguntech.Core.Entities;
using Perguntech.Services;
using System.Threading;

namespace Perguntech.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly QuestionService _service;


        public QuestionsController(QuestionService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<Question>> GetAllQuestions()
        {
            return await _service.GetAllQuestionsAsync();
        }

        [HttpGet("{id}")]
        public async Task<Question> GetQuestionById(Guid id)
        {
            return await _service.GetQuestionByIdAsync(id);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuestion(Guid id, Question question)
        {
            if (id != question.Id)
            {
                return BadRequest();
            }
            await _service.UpdateQuestionAsync(question);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(Guid id)
        {
            await _service.DeleteQuestionAsync(id);
            return Ok();
        }

        [HttpGet("categories/{name}")]
        public async Task<Category> GetCategoryByName(string name, CancellationToken cancellationToken)
        {
            return await _service.GetCategoryByNameAsync(name, cancellationToken);
        }

        [HttpPost]
        public async Task<IActionResult> AddQuestion(Question question, string categoryName, CancellationToken cancellationToken)
        {
            Category category = await _service.GetCategoryByNameAsync(categoryName, cancellationToken);

            if (category == null)
            {
                category = new Category
                {
                    CategoryName = categoryName
                };
                await _service.AddCategoryAsync(category);
            }

            question.CategoryIds = new List<string> { category.Id.ToString() };
            await _service.AddQuestionAsync(question);

            return Ok();
        }

        [HttpPost("AddCategory")]
        public async Task<IActionResult> AddCategory(string categoryName, CancellationToken cancellationToken)
        {
            var existingCategory = await _service.GetCategoryByNameAsync(categoryName, cancellationToken);

            if (existingCategory != null)
            {
                return BadRequest("Category already exists.");
            }

            Category category = new Category
            {
                CategoryName = categoryName
            };

            await _service.AddCategoryAsync(category);

            return Ok();
        }

    }
}
