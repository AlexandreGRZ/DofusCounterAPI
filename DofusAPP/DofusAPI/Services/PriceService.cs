using DofusData.PriceData;
using MongoDB.Driver;

namespace DofusAPI.Services;

public class PriceService
{
    
    private readonly IMongoCollection<Price> _prices;

    public PriceService(IConfiguration config)
    {
        var client = new MongoClient(config["MongoDB:ConnectionString"]);
        var database = client.GetDatabase(config["MongoDB:DatabaseName"]);
        _prices = database.GetCollection<Price>(config["MongoDB:PricesCollection"]);
    }

    public async Task<List<Price>> GetAll() => await _prices.Find(price => true).ToListAsync();

    public async Task<List<Price>> GetByItem(int ankamaId) =>
        await _prices.Find(price => price.AnkamaId == ankamaId).ToListAsync();

    public async Task Add(Price price) => await _prices.InsertOneAsync(price);
    
    
    public async Task<bool> UpsertPrice(int ankamaId, int value)
    {
        var filter = Builders<Price>.Filter.Eq(p => p.AnkamaId, ankamaId);
        var update = Builders<Price>.Update
            .Set(p => p.Value, value)
            .Set(p => p.UpdatedAt, DateTime.UtcNow); // La date est générée automatiquement

        var result = await _prices.UpdateOneAsync(filter, update, new UpdateOptions { IsUpsert = true });

        return result.ModifiedCount > 0 || result.UpsertedId != null;
    }
}