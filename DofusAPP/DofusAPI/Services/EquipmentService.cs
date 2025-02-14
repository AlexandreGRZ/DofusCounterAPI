using DofusData.EquipementData;
using DofusData.PriceData;
using DofusEquipementFetcher.Data;
using DofusResourceFetcher.Data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DofusAPI.Services;

public class EquipmentService
{
    private readonly IMongoCollection<Equipment> _equipments;
    private readonly IMongoCollection<Resource> _resources;
    private readonly IMongoCollection<Price> _price;

    public EquipmentService(IConfiguration config)
    {
        var client = new MongoClient(config["MongoDB:ConnectionString"]);
        var database = client.GetDatabase(config["MongoDB:DatabaseName"]);
        _equipments = database.GetCollection<Equipment>(config["MongoDB:EquipmentsCollection"]);
        _resources = database.GetCollection<Resource>(config["MongoDB:ResourcesCollection"]);
        _price = database.GetCollection<Price>(config["MongoDB:PricesCollection"]);
    }

    public async Task<List<Equipment>> GetAll() => await _equipments.Find(e => true).ToListAsync();

    public async Task<Equipment> GetById(string id) => await _equipments.Find(e => e.AnkamaId.ToString() == id).FirstOrDefaultAsync();

    public async Task<List<Equipment>> GetByLevel(int level) =>
        await _equipments.Find(e => e.Level == level).ToListAsync();

    public async Task Create(Equipment equipment) => await _equipments.InsertOneAsync(equipment);

    public async Task<bool> Delete(string id)
    {
        var result = await _equipments.DeleteOneAsync(e => e.AnkamaId.ToString() == id);
        return result.DeletedCount > 0;
    } 
    
    public async Task<List<Equipment>> GetPaged(int page, int pageSize, string? search = null)
    {
        var filter = Builders<Equipment>.Filter.Empty;

        
        if (!string.IsNullOrEmpty(search))
        {
            filter = Builders<Equipment>.Filter.Regex(e => e.Name, new BsonRegularExpression(search, "i"));
        }

        return await _equipments.Find(filter)
            .Sort(Builders<Equipment>.Sort.Ascending(e => e.Level))
            .Skip((page - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();
    }
    public async Task<EquipmentToReturn?> GetByAnkamaId(int ankamaId)
    {
        Equipment equipment = await _equipments.Find(e => e.AnkamaId == ankamaId).FirstOrDefaultAsync();
        if (equipment == null) return null;
        else
        {
            if (equipment.Recipe == null)
            {
                List<Price>? ListEquipmentPrice = await _price.Find(p => p.AnkamaId == equipment.AnkamaId).ToListAsync();
                Price? Equipmentprice = null;
            
                if (ListEquipmentPrice.Count != 0)
                {
                    Equipmentprice = ListEquipmentPrice.First();
                }
                
                if (Equipmentprice == null)
                {
                    return new EquipmentToReturn(equipment, null);
                }
                else
                {
                    return new EquipmentToReturn(equipment, Equipmentprice);
                }
                
            }
            else
            {
                var resourceIds = equipment.Recipe.Select(r => r.ItemAnkamaId).ToList();
                var resources = await _resources.Find(r => resourceIds.Contains(r.AnkamaId)).ToListAsync();
                List<Price>? ListEquipmentPrice = await _price.Find(p => p.AnkamaId == equipment.AnkamaId).ToListAsync();
                Price? Equipmentprice = null;
            
                if (ListEquipmentPrice.Count != 0)
                {
                    Equipmentprice = ListEquipmentPrice.First();
                }
            
                foreach (var ingredient in equipment.Recipe)
                {
                    var resource = resources.FirstOrDefault(r => r.AnkamaId == ingredient.ItemAnkamaId);
                    if (resource != null)
                    {
                        List<Price>? ListIngredientPrice = await _price.Find(p => p.AnkamaId == resource.AnkamaId).ToListAsync();
                        Price? ResourcesPrice = null;
                        if (ListIngredientPrice.Count != 0)
                        {
                            ResourcesPrice = ListIngredientPrice.First();
                        }

                        if (resource != null && ResourcesPrice != null )
                        {
                            ResourcesDataToReturn resourcesDataToReturn = new ResourcesDataToReturn(resource, ResourcesPrice);
                            ingredient.ResourceInfo = resourcesDataToReturn; 
                        }
                    }
                }
                
                if (Equipmentprice == null)
                {
                    return new EquipmentToReturn(equipment, null);
                }
                else
                {
                    return new EquipmentToReturn(equipment, Equipmentprice);
                }
                
            }
        }
    }


}