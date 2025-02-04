using DofusResourceFetcher.Data;
using MongoDB.Driver;

namespace DofusAPI.Services;

public class ResourceService
{
    private readonly IMongoCollection<Resource> _resources;

    public ResourceService(IConfiguration config)
    {
        var client = new MongoClient(config["MongoDB:ConnectionString"]);
        var database = client.GetDatabase(config["MongoDB:DatabaseName"]);
        _resources = database.GetCollection<Resource>(config["MongoDB:ResourcesCollection"]);
    }

    public async Task<List<Resource>> GetAll() => await _resources.Find(r => true).ToListAsync();

    public async Task<Resource> GetById(string id) => await _resources.Find(r => r.AnkamaId.ToString() == id).FirstOrDefaultAsync();

    public async Task<List<Resource>> GetByLevel(int level) =>
        await _resources.Find(r => r.Level == level).ToListAsync();

    public async Task Create(Resource resource) => await _resources.InsertOneAsync(resource);

    public async Task<bool> Delete(string id)
    {
        var result = await _resources.DeleteOneAsync(r => r.AnkamaId.ToString() == id);
        return result.DeletedCount > 0;
    }
}