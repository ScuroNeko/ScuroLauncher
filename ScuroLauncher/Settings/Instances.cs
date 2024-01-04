using System.Text.Json;

namespace ScuroLauncher.Settings;

public enum InstanceType: byte
{
    Genshin,
    Hsr,
    Honkai,
    Unknown = 255,
}

public record InstanceItem
{
    public string Name { get; set; } = "";
    public string Version { get; set; } = "1.0.0";
    public string Path { get; set; } = "";
    public string Icon { get; set; } = "default.png";
    public InstanceType Type { get; set; } = InstanceType.Unknown;
}

public class Instance
{
    public List<InstanceItem> Instances { get; set; } = [];

    public void AddInstance(InstanceItem instance)
    {
        Instances.Add(instance);
    }
    
    private static readonly JsonSerializerOptions JsonOptions = new() { WriteIndented = true };

    public static bool IsExist()
    {
        return File.Exists(SettingsLocations.InstancesPath);
    }

    public static Instance New()
    {
        var instances = new Instance();
        var jsonString = JsonSerializer.Serialize(instances, JsonOptions);

        Directory.CreateDirectory(SettingsLocations.ConfigFolder);
        File.WriteAllText(SettingsLocations.InstancesPath, jsonString);

        return instances;
    }

    public static Instance Load()
    {
        var jsonString = File.ReadAllText(SettingsLocations.InstancesPath);
        return JsonSerializer.Deserialize<Instance>(jsonString) ?? throw new Exception();
    }

    public void Save()
    {
        var jsonString = JsonSerializer.Serialize(this, JsonOptions);
        File.WriteAllText(SettingsLocations.InstancesPath, jsonString);
        Console.WriteLine(jsonString);
    }
}