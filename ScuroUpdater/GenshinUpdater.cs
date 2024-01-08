using System.Diagnostics;
using ScuroHttp;
using ScuroLib;

namespace ScuroUpdater;

public static class GenshinUpdater
{
    public static async Task<UpdateInfo> Check(string instancePath)
    {
        var configIni = new IniFile(Path.Combine(instancePath, "config.ini"));
        var configVersion = configIni.Read("game_version", "General");
        var apiVersion = await HoyoverseApi.GetLatestGenshinVersion();
        return new UpdateInfo { HasUpdate = configVersion != apiVersion, NewVersion = apiVersion};
    }

    public static void DoUpdate()
    {
        Console.WriteLine("Типа обновляемся");
    }
}