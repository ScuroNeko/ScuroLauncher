using System.ComponentModel;
using System.Text.Json;

namespace ScuroLauncher.Settings;

public class Config
{
    public string Theme { get; set; } = SettingsDefaults.DefaultThemeName;
    public uint NextID { get; set; } = 1;
    public bool Debug { get; set; } = false;
    
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public static bool IsExist()
    {
        return File.Exists(SettingsLocations.ConfigPath);
    }
    
    public static Config New()
    {
        var config = new Config();
        var jsonString = JsonSerializer.Serialize(config, JsonOptions);
        
        Directory.CreateDirectory(SettingsLocations.ConfigFolder);
        File.WriteAllText(SettingsLocations.ConfigPath, jsonString);
        File.SetAttributes(SettingsLocations.ConfigPath, FileAttributes.Normal);
        return config;
    }
    
    public static Config Load()
    {
        var jsonString = File.ReadAllText(SettingsLocations.ConfigPath);
        return JsonSerializer.Deserialize<Config>(jsonString) ?? throw new Exception("Can't deserialize json config");
    }

    public void Save()
    {
        var jsonString = JsonSerializer.Serialize(this, JsonOptions);
        File.WriteAllText(SettingsLocations.ConfigPath, jsonString);
        Console.WriteLine(jsonString);
    }
}