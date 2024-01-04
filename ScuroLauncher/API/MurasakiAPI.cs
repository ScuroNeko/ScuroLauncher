using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScuroLauncher.API;

internal class MurasakiAPI
{
    public static async Task<List<GameItem>> GetGenshinVersions()
    {
        var response = await API.Request<GenshinResponse>("https://murasaki.nix13.pw/genshin");
        return response.genshin;
    }

    public static async Task<List<GameItem>> GetHsrVersions()
    {
        var response = await API.Request<HsrResponse>("https://murasaki.nix13.pw/hsr");
        return response.hsr;
    }
}
