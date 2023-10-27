using MongoDB.Driver;
using Perguntech.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Perguntech.Data.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IMongoCollection<Question> _questions;
        private readonly IMongoCollection<Category> _category;

        public QuestionRepository(string connectionString, 
            string databaseName,
            string questionCollectionName,
            string categoryCollectionName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _questions = database.GetCollection<Question>(questionCollectionName);
            _category = database.GetCollection<Category>(categoryCollectionName);
        }


        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            return await _questions.Find(question => true).ToListAsync();
        }

        public async Task<Question> GetQuestionByIdAsync(Guid id)
        {
            return await _questions.Find<Question>(question => question.Id == id).FirstOrDefaultAsync();
        }

        public async Task AddQuestionAsync(Question question)
        {
            await _questions.InsertOneAsync(question);
        }

        public async Task UpdateQuestionAsync(Question question)
        {
            await _questions.ReplaceOneAsync(q => q.Id == question.Id, question);
        }

        public async Task DeleteQuestionAsync(Guid id)
        {
            await _questions.DeleteOneAsync(question => question.Id == id);
        }

        public async Task<Category> GetCategoryByNameAsync(string categoryName, CancellationToken cancellationToken)
        {
            return await _category.Find<Category>(cat => cat.CategoryName == categoryName).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task AddCategoryAsync(Category category)
        {
            await _category.InsertOneAsync(category);
        }

    }
}
