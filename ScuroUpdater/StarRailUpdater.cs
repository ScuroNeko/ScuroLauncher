using ScuroHttp;
using ScuroLib;

namespace ScuroUpdater;

public static class StarRailUpdater
{
    public static async Task<UpdateInfo> Check(string instancePath)
    {
        var configIni = new IniFile(Path.Combine(instancePath, "config.ini"));
        var configVersion = configIni.Read("game_version", "General");
        var apiVersion = await HoyoverseApi.GetLatestStarRailVersion();
        return new UpdateInfo { HasUpdate = configVersion != apiVersion, NewVersion = apiVersion };
    }
}
