using MongoDB.Bson;
using MongoDB.Driver;
using Perguntech.Core.Domain;

namespace Perguntech.Data.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IMongoCollection<QuestionDomain> _questions;
        private readonly IMongoCollection<CategoryDomain> _categories;

        public QuestionRepository(string connectionString,
                                  string databaseName,
                                  string questionCollectionName,
                                  string categoryCollectionName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _questions = database.GetCollection<QuestionDomain>(questionCollectionName);
            _categories = database.GetCollection<CategoryDomain>(categoryCollectionName);
        }

        public async Task<IEnumerable<QuestionDomain>> GetAllQuestionsAsync()
        {
            try
            {
                return await _questions.Find(question => true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching all questions.", ex);
            }
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

        public async Task<CategoryDomain> GetCategoryByNameAsync(string categoryName, CancellationToken cancellationToken)
        {
            try
            {
                return await _categories.Find<CategoryDomain>(cat => cat.Name == categoryName).FirstOrDefaultAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching category with name: {categoryName}", ex);
            }
        }

        public async Task AddCategoryAsync(CategoryDomain category)
        {
            try
            {
                await _categories.InsertOneAsync(category);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding category with name: {category.Name}", ex);
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
