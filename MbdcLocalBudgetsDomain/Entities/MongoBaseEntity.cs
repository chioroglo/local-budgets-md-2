using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MbdcLocalBudgetsDomain.Entities;

public abstract class MongoBaseEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}