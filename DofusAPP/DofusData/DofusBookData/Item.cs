using System.Text.Json.Serialization;

namespace DofusData.DofusBookData;

public class Item
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("picture")]
    public int Picture { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}