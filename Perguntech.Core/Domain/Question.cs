
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Perguntech.Core.Domain
{
    public class QuestionDomain
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonElement("question")]
        public string Question { get; set; }

        [BsonElement("answer")]
        public string Answer { get; set; }

        [BsonElement("categoryIds")]
        public List<Guid> CategoryIds { get; set; }
    }
}
