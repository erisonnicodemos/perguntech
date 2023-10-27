
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Perguntech.Core.Domain
{
    public class QuestionDomain
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonElement("questionText")]
        public string Question { get; set; }

        [BsonElement("answerText")]
        public string Answer { get; set; }

        [BsonElement("categoryIds")]
        public List<string> CategoryIds { get; set; }
    }
}
