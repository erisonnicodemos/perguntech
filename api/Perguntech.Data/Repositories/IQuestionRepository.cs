using Perguntech.Core.Domain;

namespace Perguntech.Data.Repositories
{
    public interface IQuestionRepository
    {
        Task<(IEnumerable<QuestionDomain>, long)> GetPaginatedQuestionsAsync(int page, int pageSize, string search);
        Task<QuestionDomain> GetQuestionByIdAsync(Guid id);
        Task AddQuestionAsync(QuestionDomain question);
        Task UpdateQuestionAsync(QuestionDomain question);
        Task DeleteQuestionAsync(Guid id);
        Task<IEnumerable<QuestionDomain>> GetQuestionsByTitleAsync(string title);
    }
}
