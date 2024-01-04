namespace ScuroLauncher.Settings;

public class SettingsLocations
{
    private static readonly string AppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
    public static readonly string ConfigFolder = Path.Combine(AppDataFolder, "ScuroLauncher");
    
    public static readonly string InstancesPath = Path.Combine(ConfigFolder, "instances.json");
    public static readonly string ConfigPath = Path.Combine(ConfigFolder, "config.json");
    public static readonly string ThemesPath = Path.Combine(ConfigFolder, "themes.json");

    public static void CreateFolder()
    {
        Directory.CreateDirectory(ConfigFolder);
    }
}