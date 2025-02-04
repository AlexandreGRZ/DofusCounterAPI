using MongoDB.Bson.Serialization.Attributes;

namespace DofusEquipementFetcher.Data;

public class EffectType
{
    [BsonElement("name")]
    public string Name { get; set; }

    [BsonElement("id")]
    public int Id { get; set; }
}