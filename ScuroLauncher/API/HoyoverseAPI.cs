namespace ScuroLauncher.API;

public class HoyoverseAPI
{
    public static async Task<string> GetLatestGenshinVersion()
    {
        HoyoverseGenshinData data = await API.HoYoRequest<HoyoverseGenshinData>("https://hk4e-launcher-static.hoyoverse.com/hk4e_global/mdk/launcher/api/resource?launcher_id=10&key=gcStgarh&channel_id=1&sub_channel_id=3");
        return data.game.latest.version;
    }
}