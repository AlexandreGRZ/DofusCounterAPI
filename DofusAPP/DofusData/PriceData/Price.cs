using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace DofusData.PriceData;

public class Price
{
    [BsonId]  // Définit l'ID MongoDB
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } // MongoDB génère cet ID automatiquement

    [BsonElement("ankamaId")]
    public int AnkamaId { get; set; }  // ID de l'objet Dofus

    [BsonElement("value")]
    public int Value { get; set; } // Prix en kamas

    [BsonElement("updatedAt")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow; // Date générée automatiquement
}