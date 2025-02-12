using DofusData.PriceData;
using DofusEquipementFetcher.Data;
using Newtonsoft.Json;
using Range = System.Range;

namespace DofusData.EquipementData;

public class EquipmentToReturn
{
    
    [JsonProperty("ankama_id")]
    public int AnkamaId { get; set; }

    public string Name { get; set; }
    
    public int Price { get; set;}

    public EquipmentType Type { get; set; }

    public int Level { get; set; }

    public ImageUrls ImageUrls { get; set; }

    public string Description { get; set; }

    public List<RecipeItem> Recipe { get; set; }

    public List<Effect> Effects { get; set; }

    public bool IsWeapon { get; set; }

    public int Pods { get; set; }

    public int CriticalHitProbability { get; set; }

    public int CriticalHitBonus { get; set; }

    public int MaxCastPerTurn { get; set; }

    public int ApCost { get; set; }


    public EquipmentToReturn(Equipment equipment, Price? price)
    {
        if (price == null)
        {
            AnkamaId = equipment.AnkamaId;
            Name = equipment.Name;
            Price = 0;
            Type = equipment.Type;
            Level = equipment.Level;
            ImageUrls = equipment.ImageUrls;
            Description = equipment.Description;
            Recipe = equipment.Recipe;
            Effects = equipment.Effects;
            IsWeapon = equipment.IsWeapon;
            Pods = equipment.Pods;
            CriticalHitProbability = equipment.CriticalHitProbability;
            CriticalHitBonus = equipment.CriticalHitBonus;
            MaxCastPerTurn = equipment.MaxCastPerTurn;
            ApCost = equipment.ApCost;
        }
        else
        {
            AnkamaId = equipment.AnkamaId;
            Name = equipment.Name;
            Price = price.Value;
            Type = equipment.Type;
            Level = equipment.Level;
            ImageUrls = equipment.ImageUrls;
            Description = equipment.Description;
            Recipe = equipment.Recipe;
            Effects = equipment.Effects;
            IsWeapon = equipment.IsWeapon;
            Pods = equipment.Pods;
            CriticalHitProbability = equipment.CriticalHitProbability;
            CriticalHitBonus = equipment.CriticalHitBonus;
            MaxCastPerTurn = equipment.MaxCastPerTurn;
            ApCost = equipment.ApCost;
        }
        
        
        
    }

}