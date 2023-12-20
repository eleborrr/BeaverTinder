using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BeaverTinder.Shared.Mongo;

public class MetadataDto{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    public string Key { get; set; }
    public Dictionary<string, string> Data { get; set; }
};