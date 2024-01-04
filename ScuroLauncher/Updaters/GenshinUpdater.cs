using ScuroLauncher.API;
using ScuroLauncher.Lib;

namespace ScuroLauncher.Updaters;

public class GenshinUpdateInfo
{
    public bool hasUpdate { get; set; } = false;
    public string newVersion { get; set; } = "";
}

public class GenshinUpdater
{
    public static async Task<GenshinUpdateInfo> Check(string instancePath)
    {
        var configIni = new IniFile(Path.Combine(instancePath, "config.ini"));
        var configVersion = configIni.Read("game_version", "General");
        var apiVersion = await HoyoverseAPI.GetLatestGenshinVersion();
        return new GenshinUpdateInfo { hasUpdate = configVersion != apiVersion, newVersion = apiVersion};
    }

    public static async Task DoUpdate()
    {
        Console.WriteLine("Типа обновляемся");
    }
}