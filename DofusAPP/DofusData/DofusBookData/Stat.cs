using System.Text.Json.Serialization;

namespace DofusData.DofusBookData;

public class Stat
{
    [JsonPropertyName("views")]
    public int Views { get; set; }

    [JsonPropertyName("level")]
    public int Level { get; set; }

    [JsonPropertyName("pa")]
    public int Pa { get; set; }

    [JsonPropertyName("pm")]
    public int Pm { get; set; }

    [JsonPropertyName("po")]
    public int Po { get; set; }

    [JsonPropertyName("dp")]
    public int Dp { get; set; }

    // "in" est un mot réservé en C#, on le renomme ici en InValue
    [JsonPropertyName("in")]
    public int InValue { get; set; }

    [JsonPropertyName("fo")]
    public int Fo { get; set; }

    [JsonPropertyName("ag")]
    public int Ag { get; set; }

    [JsonPropertyName("ch")]
    public int Ch { get; set; }

    [JsonPropertyName("in_pu")]
    public int InPu { get; set; }

    [JsonPropertyName("fo_pu")]
    public int FoPu { get; set; }

    [JsonPropertyName("ag_pu")]
    public int AgPu { get; set; }

    [JsonPropertyName("ch_pu")]
    public int ChPu { get; set; }
}