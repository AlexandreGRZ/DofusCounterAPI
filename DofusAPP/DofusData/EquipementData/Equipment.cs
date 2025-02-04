using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace DofusEquipementFetcher.Data;

public class Equipment
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
    public EquipmentType Type { get; set; }

    [BsonElement("level")]
    public int Level { get; set; }

    [JsonProperty("image_urls")]
    public ImageUrls ImageUrls { get; set; }

    [BsonElement("description")]
    public string Description { get; set; }

    [BsonElement("recipe")]
    public List<RecipeItem> Recipe { get; set; }

    [BsonElement("effects")]
    public List<Effect> Effects { get; set; }

    [BsonElement("is_weapon")]
    public bool IsWeapon { get; set; }

    [BsonElement("pods")]
    public int Pods { get; set; }

    [BsonElement("critical_hit_probability")]
    public int CriticalHitProbability { get; set; }

    [BsonElement("critical_hit_bonus")]
    public int CriticalHitBonus { get; set; }

    [BsonElement("max_cast_per_turn")]
    public int MaxCastPerTurn { get; set; }

    [BsonElement("ap_cost")]
    public int ApCost { get; set; }

    [BsonElement("range")]
    public Range Range { get; set; }
}