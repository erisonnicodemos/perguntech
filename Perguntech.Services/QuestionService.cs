using Perguntech.Core.Entities;
using Perguntech.Data.Repositories;

namespace Perguntech.Services
{
    public class QuestionService
    {
        private readonly IQuestionRepository _repository;

        public QuestionService(IQuestionRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public Task<IEnumerable<Question>> GetAllQuestionsAsync() => _repository.GetAllQuestionsAsync();

        public Task<Question> GetQuestionByIdAsync(Guid id) => _repository.GetQuestionByIdAsync(id);

        public async Task AddOrUpdateQuestionAsync(Question question)
        {
            if (question.Id == Guid.Empty)
            {
                question.Id = Guid.NewGuid();
                await _repository.AddQuestionAsync(question);
            }
            else
            {
                await _repository.UpdateQuestionAsync(question);
            }
        }

        public Task DeleteQuestionAsync(Guid id) => _repository.DeleteQuestionAsync(id);

        public async Task<Category> GetCategoryByNameAsync(string categoryName, CancellationToken cancellationToken)
        {
            return await _repository.GetCategoryByNameAsync(categoryName, cancellationToken);
        }

        public async Task<Category> CreateCategoryAsync(string categoryName)
        {
            var category = new Category { CategoryName = categoryName, Id = Guid.NewGuid() };
            await _repository.AddCategoryAsync(category);
            return category;
        }
    }

}
