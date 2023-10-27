using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Perguntech.Core.Entities
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonElement("CategoryName")]
        public string CategoryName { get; set; }

        [BsonElement("QuestionIds")]
        public List<string> QuestionIds { get; set; }
    }
}
