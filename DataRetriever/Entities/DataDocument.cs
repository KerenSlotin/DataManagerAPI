using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

internal class DataItem
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id { get; set; }

    [BsonRequired]
    [BsonElement("value")]
    public string? Value { get; set; }

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; }

    public DataItem()
    {
        Id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
    }
}