using System.Text.Json.Serialization;

namespace DofusData.DofusBookData;

public class Folder
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}