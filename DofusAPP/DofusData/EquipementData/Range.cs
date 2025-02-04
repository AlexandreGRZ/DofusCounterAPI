using MongoDB.Bson.Serialization.Attributes;

namespace DofusEquipementFetcher.Data;

public class Range
{
    [BsonElement("min")]
    public int Min { get; set; }

    [BsonElement("max")]
    public int Max { get; set; }
}