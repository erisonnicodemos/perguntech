
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Perguntech.Core.Entities
{
    public class Question
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonElement("QuestionText")]
        public string QuestionText { get; set; }

        [BsonElement("AnswerText")]
        public string AnswerText { get; set; }

        [BsonElement("CategoryIds")]
        public List<string> CategoryIds { get; set; }
    }
}
