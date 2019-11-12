using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Granny.DataModel
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("Id")]
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
