using Newtonsoft.Json;
using System.ComponentModel;

namespace ScuroLauncher.Settings;

public class ColorJsonConverter : JsonConverter<Color>
{
    public ColorJsonConverter() {}

    public override void WriteJson(JsonWriter writer, Color color, JsonSerializer serializer)
    {
        writer.WriteValue(ColorTranslator.ToHtml(color));
    }

    public override Color ReadJson(JsonReader reader, Type objectType, Color existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        var color = reader.Value;
        return color == null ? throw new NullReferenceException() : ColorTranslator.FromHtml((string)color);
    }
}

public class ThemeItem
{
    public string Name { get; set; } = "";
    public string Font { get; set; } = "";

    [JsonConverter(typeof(ColorJsonConverter))]
    public Color BackgroundColor { get ; set; } = Color.Black;
    [JsonConverter(typeof(ColorJsonConverter))]
    public Color TextColor { get; set; } = Color.Black;
    [JsonConverter(typeof(ColorJsonConverter))]
    public Color SurfaceColor { get; set; } = Color.Black;
    [JsonConverter(typeof(ColorJsonConverter))]
    public Color OverlayColor { get; set; } = Color.Black;
    [JsonConverter(typeof(ColorJsonConverter))]
    public Color AccentColor { get; set; } = Color.Black;
}

public class Theme
{
    // JSON Properties
    public List<ThemeItem> Themes { get; set; } = [];

    // Service
    public static bool IsExist()
    {
        return File.Exists(SettingsLocations.ThemesPath);
    }

    public static Theme New()
    {
        var themes = new Theme() {
            Themes = [SettingsDefaults.CatppuccinLatte, SettingsDefaults.CatppuccinMocha]
        };
        var jsonString = JsonConvert.SerializeObject(themes, Formatting.Indented);

        SettingsLocations.CreateFolder();
        File.WriteAllText(SettingsLocations.ThemesPath, jsonString);

        return themes;
    }

    public static Theme Load()
    {
        var jsonString = File.ReadAllText(SettingsLocations.ThemesPath);
        return JsonConvert.DeserializeObject<Theme>(jsonString) ?? throw new JsonException();
    }

    public void Save()
    {
        var jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
        File.WriteAllText(SettingsLocations.ThemesPath, jsonString);
        Console.WriteLine(jsonString);
    }
}