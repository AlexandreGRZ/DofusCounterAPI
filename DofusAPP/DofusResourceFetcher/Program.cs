using DofusResourceFetcher.Data;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace DofusResourceFetcher;

class Program
{
    private static readonly HttpClient Client = new HttpClient();
    private static readonly string ConnectionString = "mongodb://admin:password@193.203.169.114:27017/";
    private static readonly string DatabaseName = "DofusDB";
    private static readonly string CollectionName = "resources";
    
    static async Task Main(string[] args)
    {
        try
        {
            // Connexion à MongoDB
            var clientMongo = new MongoClient(ConnectionString);
            var database = clientMongo.GetDatabase(DatabaseName);
            var collection = database.GetCollection<Resource>(CollectionName);
            
            // Vérifier si les équipements existent déjà
            var existingCount = await collection.CountDocumentsAsync(FilterDefinition<Resource>.Empty);
            if (existingCount > 0)
            {
                Console.WriteLine("Les équipements sont déjà stockés dans la base de données.");
                return;
            }
            string apiUrl = "https://api.dofusdu.de/dofus3/v1/fr/items/resources/all";
            
            HttpResponseMessage response = await Client.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();
            
            string responseBody = await response.Content.ReadAsStringAsync();
            
            ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseBody);
            
            if (apiResponse?.items != null)
            {
                await collection.InsertManyAsync(apiResponse.items);
                Console.WriteLine($"{apiResponse.items.Count} ressources insérées dans MongoDB !");
            }
            else
            {
                Console.WriteLine("Aucune ressource trouvée.");
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Erreur lors de la requête HTTP: {e.Message}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erreur: {e.Message}");
        }
    }
}