using DofusResourceFetcher.Data;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace DofusEquipementFetcher.Data;

public class RecipeItem
{
    [BsonElement("item_ankama_id")]
    [JsonProperty("item_ankama_id")]
    public int ItemAnkamaId { get; set; }

    [BsonElement("item_subtype")]
    public string ItemSubtype { get; set; }

    [BsonElement("quantity")]
    public int Quantity { get; set; }
    
    [BsonIgnore]
    public ResourcesDataToReturn? ResourceInfo { get; set; }
}