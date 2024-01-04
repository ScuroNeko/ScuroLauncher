using System.Text.Json;

namespace ScuroLauncher.Settings;

public class ThemeItem
{
    public string Name { get; set; } = "";
    public string Font { get; set; } = "";
    public string BackgroundColor { get ; set; } = "";
    public string TextColor { get; set; } = "";
    public string SurfaceColor { get; set; } = "";
    public string OverlayColor { get; set; } = "";
    public string AccentColor { get; set; } = "";
}

public class Theme
{
    // JSON Properties
    public List<ThemeItem> Themes { get; set; } = [SettingsDefaults.CatppuccinLatte, SettingsDefaults.CatppuccinMocha];
    
    // Service
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public static bool IsExist()
    {
        return File.Exists(SettingsLocations.ThemesPath);
    }

    public static Theme New()
    {
        var themes = new Theme();
        var jsonString = JsonSerializer.Serialize(themes, JsonOptions);

        SettingsLocations.CreateFolder();
        File.WriteAllText(SettingsLocations.ThemesPath, jsonString);

        return themes;
    }

    public static Theme Load()
    {
        var jsonString = File.ReadAllText(SettingsLocations.ThemesPath);
        return JsonSerializer.Deserialize<Theme>(jsonString) ?? throw new Exception();
    }

    public void Save()
    {
        var jsonString = JsonSerializer.Serialize(this, JsonOptions);
        File.WriteAllText(SettingsLocations.ThemesPath, jsonString);
        Console.WriteLine(jsonString);
    }
}