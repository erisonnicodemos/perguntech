using Perguntech.Core.Entities;

namespace Perguntech.Data.Repositories
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<Question>> GetAllQuestionsAsync();
        Task<Question> GetQuestionByIdAsync(Guid id);
        Task AddQuestionAsync(Question question);
        Task UpdateQuestionAsync(Question question);
        Task DeleteQuestionAsync(Guid id);
        Task<Category> GetCategoryByNameAsync(string categoryName, CancellationToken cancellationToken);
        Task AddCategoryAsync(Category category);
    }
}
