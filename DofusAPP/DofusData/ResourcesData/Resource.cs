using DofusEquipementFetcher.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace DofusResourceFetcher.Data;

public class Resource
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    [BsonElement("ankama_id")]
    [JsonProperty("ankama_id")]
    public int AnkamaId { get; set; }

    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("type")]
    public ResourceType Type { get; set; }

    [BsonElement("level")]
    public int Level { get; set; }

    [BsonElement("image_urls")]
    [JsonProperty("image_urls")]
    public ImageUrls ImageUrls { get; set; }

    [BsonElement("description")]
    public string Description { get; set; }
}