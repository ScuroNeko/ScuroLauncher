using ScuroLauncher.API;
using ScuroLauncher.Lib;

namespace ScuroLauncher.Updaters;

public class GenshinUpdater
{
    public static async Task<UpdateInfo> Check(string instancePath)
    {
        var configIni = new IniFile(Path.Combine(instancePath, "config.ini"));
        var configVersion = configIni.Read("game_version", "General");
        var apiVersion = await HoyoverseAPI.GetLatestGenshinVersion();
        return new UpdateInfo { HasUpdate = configVersion != apiVersion, NewVersion = apiVersion};
    }

    public static async Task DoUpdate()
    {
        Console.WriteLine("Типа обновляемся");
    }
}