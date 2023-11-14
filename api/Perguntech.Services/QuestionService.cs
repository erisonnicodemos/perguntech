using Perguntech.Core.Domain;
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

        public Task<IEnumerable<QuestionDomain>> GetAllQuestionsAsync() => _repository.GetAllQuestionsAsync();

        public Task<QuestionDomain> GetQuestionByIdAsync(Guid id) => _repository.GetQuestionByIdAsync(id);

        public async Task AddOrUpdateQuestionAsync(QuestionDomain question)
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

        public Task<IEnumerable<QuestionDomain>> GetQuestionsByTitleAsync(string title) => _repository.GetQuestionsByTitleAsync(title);
    }

}
