namespace ScuroLauncher.API;

public class HoyoverseAPI
{
    public static async Task<string> GetLatestGenshinVersion()
    {
        HoyoverseGameData data = await API.HoYoRequest<HoyoverseGameData>("https://hk4e-launcher-static.hoyoverse.com/hk4e_global/mdk/launcher/api/resource?launcher_id=10&key=gcStgarh&channel_id=1&sub_channel_id=3");
        return data.game.latest.version;
    }

    public static async Task<string> GetLatestStarRailVersion()
    {
        var data = await API.HoYoRequest<HoyoverseGameData>("https://hkrpg-launcher-static.hoyoverse.com/hkrpg_global/mdk/launcher/api/resource?channel_id=1&key=vplOVX8Vn7cwG8yb&launcher_id=35&sub_channel_id=1");
        return data.game.latest.version;
    }
}