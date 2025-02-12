using System.Text.Json.Serialization;

namespace DofusData.DofusBookData;

public class StuffItem
{
    [JsonPropertyName("a1")]
    public int A1 { get; set; }

    [JsonPropertyName("a2")]
    public int A2 { get; set; }

    [JsonPropertyName("am")]
    public int Am { get; set; }

    [JsonPropertyName("ar")]
    public int Ar { get; set; }

    [JsonPropertyName("bo")]
    public int Bo { get; set; }

    [JsonPropertyName("br")]
    public int Br { get; set; }

    [JsonPropertyName("ca")]
    public int Ca { get; set; }

    [JsonPropertyName("ce")]
    public int Ce { get; set; }

    [JsonPropertyName("ch")]
    public int Ch { get; set; }

    [JsonPropertyName("d1")]
    public int D1 { get; set; }

    [JsonPropertyName("d2")]
    public int D2 { get; set; }

    [JsonPropertyName("d3")]
    public int D3 { get; set; }

    [JsonPropertyName("d4")]
    public int D4 { get; set; }

    [JsonPropertyName("d5")]
    public int D5 { get; set; }

    [JsonPropertyName("d6")]
    public int D6 { get; set; }

    [JsonPropertyName("fa")]
    public int Fa { get; set; }

    // "mo" peut être null, on le déclare donc en type nullable
    [JsonPropertyName("mo")]
    public int? Mo { get; set; }
}