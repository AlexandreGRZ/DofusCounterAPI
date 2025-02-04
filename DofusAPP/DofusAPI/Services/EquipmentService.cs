using DofusEquipementFetcher.Data;
using DofusResourceFetcher.Data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DofusAPI.Services;

public class EquipmentService
{
    private readonly IMongoCollection<Equipment> _equipments;
    private readonly IMongoCollection<Resource> _resources;

    public EquipmentService(IConfiguration config)
    {
        var client = new MongoClient(config["MongoDB:ConnectionString"]);
        var database = client.GetDatabase(config["MongoDB:DatabaseName"]);
        _equipments = database.GetCollection<Equipment>(config["MongoDB:EquipmentsCollection"]);
        _resources = database.GetCollection<Resource>(config["MongoDB:ResourcesCollection"]);
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
    public async Task<Equipment?> GetByAnkamaId(int ankamaId)
    {
        var equipment = await _equipments.Find(e => e.AnkamaId == ankamaId).FirstOrDefaultAsync();
        if (equipment == null || equipment.Recipe == null) return equipment;
        
        var resourceIds = equipment.Recipe.Select(r => r.ItemAnkamaId).ToList();
        var resources = await _resources.Find(r => resourceIds.Contains(r.AnkamaId)).ToListAsync();

        // Ajouter les informations complètes aux ressources de la recette
        foreach (var ingredient in equipment.Recipe)
        {
            var resource = resources.FirstOrDefault(r => r.AnkamaId == ingredient.ItemAnkamaId);
            if (resource != null)
            {
                ingredient.ResourceInfo = resource; 
            }
        }

        return equipment;
    }


}