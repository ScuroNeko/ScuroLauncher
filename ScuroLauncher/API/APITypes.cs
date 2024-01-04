namespace ScuroLauncher.API;

// Murasaki types
public class GenshinResponse
{
    public List<GameItem> genshin { get; set; }
}
public class HsrResponse
{
    public List<GameItem> hsr { get; set; }
}
public class GameItem
{
    public string name { get; set; }
    public List<Link> links { get; set; }
    public bool outdated { get; set; }
    public bool beta { get; set; }
}

public class Link
{
    public string name { get; set; }
    public string url { get; set; }
}

// hoYoVerse Types
public class HoYoVerseResponse<T>
{
    public int retcode { get; set; }
    public string message { get; set; }
    public T data { get; set; }
}

public class HoyoverseGenshinData
{
    public Game game { get; set; }
    public Plugin plugin { get; set; }
    public string web_url { get; set; }
    public object force_update { get; set; }
    public object pre_download_game { get; set; }
    public List<DeprecatedPackage> deprecated_packages { get; set; }
    public Sdk sdk { get; set; }
    public List<DeprecatedFile> deprecated_files { get; set; }
}

public class DeprecatedFile
{
    public string name { get; set; }
    public string md5 { get; set; }
}

public class DeprecatedPackage
{
    public string name { get; set; }
    public string md5 { get; set; }
}

public class Diff
{
    public string name { get; set; }
    public string version { get; set; }
    public string path { get; set; }
    public string size { get; set; }
    public string md5 { get; set; }
    public bool is_recommended_update { get; set; }
    public List<VoicePack> voice_packs { get; set; }
    public string package_size { get; set; }
}

public class Game
{
    public Latest latest { get; set; }
    public List<Diff> diffs { get; set; }
}

public class Latest
{
    public string name { get; set; }
    public string version { get; set; }
    public string path { get; set; }
    public string size { get; set; }
    public string md5 { get; set; }
    public string entry { get; set; }
    public List<VoicePack> voice_packs { get; set; }
    public string decompressed_path { get; set; }
    public List<Segment> segments { get; set; }
    public string package_size { get; set; }
}

public class Plugin
{
    public List<Plugin> plugins { get; set; }
    public string version { get; set; }
}

public class Plugin2
{
    public string name { get; set; }
    public string version { get; set; }
    public string path { get; set; }
    public string size { get; set; }
    public string md5 { get; set; }
    public string entry { get; set; }
    public string package_size { get; set; }
}

public class Root
{
    public int retcode { get; set; }
    public string message { get; set; }
    public HoyoverseGenshinData HoyoverseGenshinData { get; set; }
}

public class Sdk
{
    public string version { get; set; }
    public string path { get; set; }
    public string size { get; set; }
    public string md5 { get; set; }
    public string pkg_version { get; set; }
    public string desc { get; set; }
    public string channel_id { get; set; }
    public string sub_channel_id { get; set; }
    public string package_size { get; set; }
}

public class Segment
{
    public string path { get; set; }
    public string md5 { get; set; }
    public string package_size { get; set; }
}

public class VoicePack
{
    public string language { get; set; }
    public string name { get; set; }
    public string path { get; set; }
    public string size { get; set; }
    public string md5 { get; set; }
    public string package_size { get; set; }
}

