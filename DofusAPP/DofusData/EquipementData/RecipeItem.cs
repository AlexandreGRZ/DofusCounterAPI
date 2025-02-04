using DofusResourceFetcher.Data;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace DofusEquipementFetcher.Data;

public class RecipeItem
{
    [BsonElement("item_ankama_id")]
    [JsonProperty("item_ankama_id")]
    public int ItemAnkamaId { get; set; } // ID de la ressource

    [BsonElement("item_subtype")]
    public string ItemSubtype { get; set; } // Type de la ressource

    [BsonElement("quantity")]
    public int Quantity { get; set; } // Quantité requise pour le craft
    
    [BsonIgnore]
    public Resource? ResourceInfo { get; set; } // Détails de la ressource
}