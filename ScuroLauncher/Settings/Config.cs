using Newtonsoft.Json;

namespace ScuroLauncher.Settings;

public class Config
{
    public string Theme { get; set; } = SettingsDefaults.DefaultThemeName;
    public bool Debug { get; set; } = false;

    public static bool IsExist()
    {
        return File.Exists(SettingsLocations.ConfigPath);
    }
    
    public static Config New()
    {
        var config = new Config();
        var jsonString = JsonConvert.SerializeObject(config, Formatting.Indented);
        
        Directory.CreateDirectory(SettingsLocations.ConfigFolder);
        File.WriteAllText(SettingsLocations.ConfigPath, jsonString);
        File.SetAttributes(SettingsLocations.ConfigPath, FileAttributes.Normal);
        return config;
    }
    
    public static Config Load()
    {
        var jsonString = File.ReadAllText(SettingsLocations.ConfigPath);
        return JsonConvert.DeserializeObject<Config>(jsonString) ?? throw new JsonException("Can't deserialize json config");
    }

    public void Save()
    {
        var jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
        File.WriteAllText(SettingsLocations.ConfigPath, jsonString);
        Providers.Logger.Debug(jsonString);
    }
}