namespace ScuroLauncher.API;

internal class MurasakiAPI
{
    public static async Task<List<GameItem>> GetGenshinVersions()
    {
        var response = await API.Request<GenshinResponse>("https://murasaki.nix13.pw/genshin");
        return response.genshin;
    }

    public static async Task<List<GameItem>> GetStarRailVersions()
    {
        var res = await API.Request<GameResponse>("https://murasaki.nix13.pw/star_rail");
        return res.response;
    }

    public static async Task<List<GameItem>> GetHonkaiImpactVersions()
    {
        var res = await API.Request<GameResponse>("https://murasaki.nix13.pw/honkai_impact");
        return res.response;
    }

    public static async Task<List<GameItem>> GetZzzVersions()
    {
        var res = await API.Request<GameResponse>("https://murasaki.nix13.pw/zzz");
        return res.response;
    }
}
