using MongoDB.Driver;
using Perguntech.Core.Entities;

namespace Perguntech.Data.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IMongoCollection<Question> _questions;
        private readonly IMongoCollection<Category> _categories;

        public QuestionRepository(string connectionString,
                                  string databaseName,
                                  string questionCollectionName,
                                  string categoryCollectionName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _questions = database.GetCollection<Question>(questionCollectionName);
            _categories = database.GetCollection<Category>(categoryCollectionName);
        }

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
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

        public async Task<Question> GetQuestionByIdAsync(Guid id)
        {
            try
            {
                return await _questions.Find<Question>(question => question.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching question with ID: {id}", ex);
            }
        }

        public async Task AddQuestionAsync(Question question)
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

        public async Task UpdateQuestionAsync(Question question)
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

        public async Task<Category> GetCategoryByNameAsync(string categoryName, CancellationToken cancellationToken)
        {
            try
            {
                return await _categories.Find<Category>(cat => cat.CategoryName == categoryName).FirstOrDefaultAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching category with name: {categoryName}", ex);
            }
        }

        public async Task AddCategoryAsync(Category category)
        {
            try
            {
                await _categories.InsertOneAsync(category);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding category with name: {category.CategoryName}", ex);
            }
        }
    }

}
