using MongoDB.Bson.Serialization.Attributes;

namespace DofusEquipementFetcher.Data;

public class Effect
{
    [BsonElement("int_minimum")]
    public int IntMinimum { get; set; }

    [BsonElement("int_maximum")]
    public int IntMaximum { get; set; }

    [BsonElement("type")]
    public EffectType Type { get; set; }

    [BsonElement("formatted")]
    public string Formatted { get; set; }
}