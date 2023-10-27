using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Perguntech.Core.Domain
{
    public class CategoryDomain
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("questionIds")]
        public List<string> QuestionIds { get; set; }
    }
}
