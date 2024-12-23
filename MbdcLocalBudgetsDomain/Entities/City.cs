using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MbdcLocalBudgetsDomain.Entities;

public class City
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Name { get; set; }
}