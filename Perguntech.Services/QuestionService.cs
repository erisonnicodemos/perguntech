using Perguntech.Core.Entities;
using Perguntech.Data.Repositories;

namespace Perguntech.Services
{
    public class QuestionService
    {
        private readonly IQuestionRepository _repository;

        public QuestionService(IQuestionRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            return _repository.GetAllQuestionsAsync();
        }

        public Task<Question> GetQuestionByIdAsync(Guid id)
        {
            return _repository.GetQuestionByIdAsync(id);
        }

        public Task AddQuestionAsync(Question question)
        {
            question.Id = Guid.NewGuid(); 
            return _repository.AddQuestionAsync(question);
        }

        public Task UpdateQuestionAsync(Question question)
        {
            return _repository.UpdateQuestionAsync(question);
        }

        public Task DeleteQuestionAsync(Guid id)
        {
            return _repository.DeleteQuestionAsync(id);
        }

        public Task<Category> GetCategoryByNameAsync(string categoryName, CancellationToken cancellationToken)
        {
            return _repository.GetCategoryByNameAsync(categoryName, cancellationToken);
        }

        public Task AddCategoryAsync(Category category)
        {
            category.Id = Guid.NewGuid();
            return _repository.AddCategoryAsync(category);
        }

    }
}
