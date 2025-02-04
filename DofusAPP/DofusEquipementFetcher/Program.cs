
using DofusEquipementFetcher.Data;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace DofusEquipementFetcher;
class Program
{
    private static readonly HttpClient client = new HttpClient();
    private static readonly string connectionString = "mongodb://admin:password@193.203.169.114:27017/";
    private static readonly string databaseName = "DofusDB";
    private static readonly string collectionName = "equipments";

    static async Task Main(string[] args)
    {
        try
        {
            // Connexion à MongoDB
            var clientMongo = new MongoClient(connectionString);
            var database = clientMongo.GetDatabase(databaseName);
            var collection = database.GetCollection<Equipment>(collectionName);

            // Vérifier si les équipements existent déjà
            var existingCount = await collection.CountDocumentsAsync(FilterDefinition<Equipment>.Empty);
            if (existingCount > 0)
            {
                Console.WriteLine("Les équipements sont déjà stockés dans la base de données.");
                return;
            }

            // URL de l'API des équipements
            string apiUrl = "https://api.dofusdu.de/dofus3/v1/fr/items/equipment/all";

            // Requête HTTP GET
            HttpResponseMessage response = await client.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            // Lecture de la réponse JSON
            string responseBody = await response.Content.ReadAsStringAsync();

            // Désérialisation en objet ApiResponse
            ApiResponse? apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseBody);

            // Vérification et insertion dans MongoDB
            if (apiResponse?.Items != null)
            {
                await collection.InsertManyAsync(apiResponse.Items);
                Console.WriteLine($"{apiResponse.Items.Count} équipements insérés dans MongoDB !");
            }
            else
            {
                Console.WriteLine("Aucun équipement récupéré.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Erreur : {e.Message}");
        }
    }
}