using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DofusData.OauthData;

public class User
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [BsonElement("fullName")]
    public string FullName { get; set; }

    [BsonElement("email")]
    public string Email { get; set; }

    [BsonElement("passwordHash")]
    public string PasswordHash { get; set; }

    [BsonElement("provider")]
    public string Provider { get; set; } = "local";

}