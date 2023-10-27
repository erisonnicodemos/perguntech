using Perguntech.Core.Domain;

namespace Perguntech.Data.Repositories
{
    public interface IQuestionRepository
    {
        Task<IEnumerable<QuestionDomain>> GetAllQuestionsAsync();
        Task<QuestionDomain> GetQuestionByIdAsync(Guid id);
        Task AddQuestionAsync(QuestionDomain question);
        Task UpdateQuestionAsync(QuestionDomain question);
        Task DeleteQuestionAsync(Guid id);
        Task<CategoryDomain> GetCategoryByNameAsync(string categoryName, CancellationToken cancellationToken);
        Task AddCategoryAsync(CategoryDomain category);
        Task<IEnumerable<QuestionDomain>> GetQuestionsByTitleAsync(string title);
    }
}
