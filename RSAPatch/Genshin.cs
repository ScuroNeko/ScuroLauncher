namespace RSAPatch;

public enum GameVersionBranch: byte
{
    Release,
    Beta,
    Unknown = 255
}
public record GameVersion
{
    public int Major { get; set; }
    public int Minor { get; set; }
    public int Patch { get; set; }

    public GameVersionBranch Branch { get; set; }

    // Allow both 4.3 and 4.3.0
    public static GameVersion FromString(string version)
    {
        var gameVersionBranch = GameVersionBranch.Unknown;

        var v = version.Split(' ');
        if (v.Length == 2)
        {
            gameVersionBranch = v[0] switch
            {
                "Release" => GameVersionBranch.Release,
                "Beta" => GameVersionBranch.Beta,
                _ => gameVersionBranch
            };
        }

        v = v[^1].Split('.');
        if (2 > v.Length && v.Length > 3) throw new Exception("Error in version");

        if(v.Length == 3 && gameVersionBranch == GameVersionBranch.Unknown)
            gameVersionBranch = v[2].StartsWith('5') ? GameVersionBranch.Beta : GameVersionBranch.Release;
        

        return new GameVersion
        { 
            Major = int.Parse(v[0]), Minor = int.Parse(v[1]), 
            Patch = int.Parse(v.Length == 3 ? v[2]: "0"),
            Branch = gameVersionBranch
        };
    }

    public static string ToString(GameVersion version) => $"{version.Major}.{version.Minor}.{version.Patch}";
    public static bool operator>(GameVersion version1, GameVersion version2)
    {
        if(version1.Major == version2.Major) return version1.Minor > version2.Minor;
        return version1.Major > version2.Major;
    }
    public static bool operator<(GameVersion version1, GameVersion version2)
    {
        if (version1.Major == version2.Major) return version1.Minor < version2.Minor;
        return version1.Major < version2.Major;
    }

    public static bool operator>=(GameVersion version1, GameVersion version2)
    {
        if (version1.Major == version2.Major) return version1.Minor >= version2.Minor;
        return version1.Major >= version2.Major;
    }
    public static bool operator<=(GameVersion version1, GameVersion version2)
    {
        if (version1.Major == version2.Major) return version1.Minor <= version2.Minor;
        return version1.Major <= version2.Major;
    }
}

// Use RSAPatch435x.zip for 4.3.5x-?
// Use RSAPatch40.dll for 3.1-4.0
// Use RSAPatch42.dll for 4.?-4.2
// Copy RSA to Path -> Rename to version.dll -> Create PublicKey.txt
// Remove version.dll
public static class Genshin
{
    private static readonly Dictionary<string, List<GameVersion>> patchVersions = new()
    {
        { "RSAPatch40.dll", [GameVersion.FromString("3.1"), GameVersion.FromString("4.0")] },
        { "RSAPatch42.dll", [GameVersion.FromString("4.2")] },
        { "RSAPatch435x.zip", [GameVersion.FromString("4.3.50")] }
    };

    public static void Patch(string v)
    {
        Patcher.Logger.Info("Starting patching...");
        var version = GameVersion.FromString(v);
        foreach (var (filename, versions) in patchVersions)
        {
            Patcher.Logger.Debug(filename);
            if (versions.Count == 2)
            {
                var min = versions[0];
                var max = versions[1];
                if(version >= min && version <= max)
                {
                    Console.WriteLine("This version can be patched!");
                }
                else
                {
                    Console.WriteLine("This version can't be patched :(");
                }
            }
            else
            {
                if (versions[0] == version)
                {
                    Console.WriteLine("This version can be patched!");
                }
                else
                {
                    Console.WriteLine("This version can't be patched :(");
                }
            }
        }
    }
}
