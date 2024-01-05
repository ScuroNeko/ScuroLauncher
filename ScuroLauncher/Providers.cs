using ScuroLauncher.Settings;

namespace ScuroLauncher;

// Singleton for some fields
public class Providers
{
    public static Config Config;
    public static Instance Instances;
    public static Theme Themes;
    public static ThemeItem SelectedTheme;

    public static ProxyService ProxyService;

    public static DownloadsForm DownloadsForm;

    public static void Load()
    {
        Config = Config.IsExist() ? Config.Load() : Config.New();
        Instances = Instance.IsExist() ? Instance.Load() : Instance.New();
        Themes = Theme.IsExist() ? Theme.Load() : Theme.New();

        SelectedTheme = Themes.Themes.Find(x => x.Name == Config.Theme) ?? SettingsDefaults.DefaultTheme;

        ProxyService = new ProxyService();
    }
}