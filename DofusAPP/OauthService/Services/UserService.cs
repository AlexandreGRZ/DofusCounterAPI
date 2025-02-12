using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using System.Text;
using DofusData.OauthData;

namespace OauthService.Services;

public class UserService
{
    private readonly IMongoCollection<User> _users;

    public UserService(IOptions<MongoDbSettings> settings)
    {
        if (settings == null || string.IsNullOrEmpty(settings.Value.ConnectionString))
        {
            throw new ArgumentNullException(nameof(settings), "La connexion MongoDB est vide ou nulle !");
        }

        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _users = database.GetCollection<User>(settings.Value.UsersCollection);
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        return await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
    }

    public async Task<User> CreateUserAsync(User user)
    {
        user.PasswordHash = HashPassword(user.PasswordHash);
        await _users.InsertOneAsync(user);
        return user;
    }

    private string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        return Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(password)));
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return HashPassword(password) == hashedPassword;
    }
}