using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics;

namespace ScuroLauncher.Settings;

public enum InstanceType: byte
{
    Genshin,
    StarRail,
    Honkai,
    Zzz,
    Unknown = 255,
}

public record InstanceItem
{
    public string Name { get; set; } = "";
    public string Version { get; set; } = "1.0.0";
    public string Path { get; set; } = "";
    public string Icon { get; set; } = "default.png";
    [JsonConverter(typeof(StringEnumConverter))]
    public InstanceType Type { get; set; } = InstanceType.Unknown;

    public bool UseProxy { get; set; }
    public string ProxyUrl { get; set; } = "http://localhost:8888";

    [JsonIgnore]
    public Process? InstanceProcess { get; set; } = null;
}

public class Instance
{
    public List<InstanceItem> Instances { get; set; } = [];

    public void AddInstance(InstanceItem instance)
    {
        Instances.Add(instance);
    }

    public void UpdateInstance(InstanceItem newInstance)
    {
        UpdateInstance(newInstance, null);
    }

    public void UpdateInstance(InstanceItem newInstance, string? oldName)
    {
        oldName ??= newInstance.Name;

        var oldInstance = Instances.Find(instance => instance.Name == oldName) ?? throw new NullReferenceException("Old instance is null!");
        var oldIndex = Instances.IndexOf(oldInstance);
        Instances.Remove(oldInstance);
        Instances.Insert(oldIndex, newInstance);
    }

    public static bool IsExist()
    {
        return File.Exists(SettingsLocations.InstancesPath);
    }

    public static Instance New()
    {
        var instances = new Instance();
        var jsonString = JsonConvert.SerializeObject(instances, Formatting.Indented);

        Directory.CreateDirectory(SettingsLocations.ConfigFolder);
        File.WriteAllText(SettingsLocations.InstancesPath, jsonString);

        return instances;
    }

    public static Instance Load()
    {
        var jsonString = File.ReadAllText(SettingsLocations.InstancesPath);
        return JsonConvert.DeserializeObject<Instance>(jsonString) ?? throw new Exception();
    }

    public void Save()
    {
        var jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
        File.WriteAllText(SettingsLocations.InstancesPath, jsonString);
        Console.WriteLine(jsonString);
    }
}