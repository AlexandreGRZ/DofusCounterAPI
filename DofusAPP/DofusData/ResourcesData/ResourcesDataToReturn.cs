using DofusData.PriceData;
using DofusEquipementFetcher.Data;
using Newtonsoft.Json;

namespace DofusResourceFetcher.Data;

public class ResourcesDataToReturn
{
    
    [JsonProperty("ankama_id")]
    public int AnkamaId { get; set; }

    public string Name { get; set; }
    
    public int Price { get; set; }

    public ResourceType Type { get; set; }

    public int Level { get; set; }

    [JsonProperty("image_urls")]
    public ImageUrls ImageUrls { get; set; }

    public string Description { get; set; }

    public ResourcesDataToReturn(Resource resource,Price? price)
    {
        if (resource == null)
        {
            throw new ArgumentNullException("Price and Resource must not be null");
        }
        else
        {
            if (Price == null)
            {
                AnkamaId = resource.AnkamaId;
                Name = resource.Name;
                Price = 0;
                Type = resource.Type;
                Level = resource.Level;
                ImageUrls = resource.ImageUrls;
                Description = resource.Description;
            }
            else
            {
                AnkamaId = resource.AnkamaId;
                Name = resource.Name;
                Price = price.Value;
                Type = resource.Type;
                Level = resource.Level;
                ImageUrls = resource.ImageUrls;
                Description = resource.Description;
            }
        }
    }
}