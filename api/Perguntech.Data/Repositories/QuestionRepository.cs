using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Perguntech.Core.Domain;

namespace Perguntech.Data.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IMongoCollection<QuestionDomain> _questions;

        public QuestionRepository(string connectionString,
                                  string databaseName,
                                  string questionCollectionName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _questions = database.GetCollection<QuestionDomain>(questionCollectionName);
        }

        public async Task<(IEnumerable<QuestionDomain>, long)> GetPaginatedQuestionsAsync(int page, int pageSize, string search)
        {
            var query = _questions.AsQueryable(); 

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(q => q.Question.ToLower().Contains(search.ToLower()));
            }

            var totalItems = await query.CountAsync();
            var questions = await query.Skip((page - 1) * pageSize)
                                       .Take(pageSize) 
                                       .ToListAsync();

            return (questions, totalItems);
        }

        public async Task<QuestionDomain> GetQuestionByIdAsync(Guid id)
        {
            try
            {
                return await _questions.Find<QuestionDomain>(question => question.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching question with ID: {id}", ex);
            }
        }

        public async Task AddQuestionAsync(QuestionDomain question)
        {
            try
            {
                await _questions.InsertOneAsync(question);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding question with ID: {question.Id}", ex);
            }
        }

        public async Task UpdateQuestionAsync(QuestionDomain question)
        {
            try
            {
                await _questions.ReplaceOneAsync(q => q.Id == question.Id, question);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating question with ID: {question.Id}", ex);
            }
        }

        public async Task DeleteQuestionAsync(Guid id)
        {
            try
            {
                await _questions.DeleteOneAsync(question => question.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting question with ID: {id}", ex);
            }
        }

        public async Task<IEnumerable<QuestionDomain>> GetQuestionsByTitleAsync(string title)
        {
            try
            {
                var filter = Builders<QuestionDomain>.Filter.Regex(q => q.Question, new BsonRegularExpression(title, "i"));
                return await _questions.Find(filter).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching questions with title: {title}", ex);
            }
        }

    }

}
