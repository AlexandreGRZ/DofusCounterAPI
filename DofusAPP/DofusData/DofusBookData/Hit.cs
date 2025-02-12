using System.Text.Json.Serialization;

namespace DofusData.DofusBookData;

public class Hit
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("avatar")]
    public string Avatar { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("user_name")]
    public string UserName { get; set; }

    [JsonPropertyName("allowed_classes")]
    public List<int> AllowedClasses { get; set; }

    [JsonPropertyName("character_class")]
    public int CharacterClass { get; set; }

    [JsonPropertyName("character_gender")]
    public int CharacterGender { get; set; }

    [JsonPropertyName("folder")]
    public Folder Folder { get; set; }

    [JsonPropertyName("visible")]
    public int Visible { get; set; }

    [JsonPropertyName("stat")]
    public Stat Stat { get; set; }

    [JsonPropertyName("tags")]
    public List<string> Tags { get; set; }

    [JsonPropertyName("slug")]
    public string Slug { get; set; }

    [JsonPropertyName("elapsed_time")]
    public string ElapsedTime { get; set; }

    [JsonPropertyName("elapsed_time_long")]
    public string ElapsedTimeLong { get; set; }

    [JsonPropertyName("with_guide")]
    public bool WithGuide { get; set; }

    [JsonPropertyName("update")]
    public long Update { get; set; }

    [JsonPropertyName("items")]
    public List<Item> Items { get; set; }

    [JsonPropertyName("stuffItem")]
    public StuffItem StuffItem { get; set; }
}