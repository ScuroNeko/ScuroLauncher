using RSAPatch;
using ScuroLauncher.Settings;
using ScuroLogger;
using ScuroLogger.Handlers;

namespace ScuroLauncher;

// Singleton for some fields
public class Providers
{
    public static Logger Logger;
    public static Patcher Patcher;
    
    public static Config Config;
    public static Instance Instances;
    public static Theme Themes;
    public static ThemeItem SelectedTheme;

    public static ProxyService ProxyService;

    public static DownloadsForm DownloadsForm;

    public static void Load()
    {
        Logger = new Logger { Name = "ScuroLauncher" };
        var consoleHandler = new ConsoleHandler { LogLevels = LogLevels.Debug };
        var fileHandler = new TimeRotatedFileHandler("log") { LogLevels = LogLevels.Debug };
        Logger.AddHandler(consoleHandler);
        Logger.AddHandler(fileHandler);
        
        Patcher = new Patcher();

        Config = Config.IsExist() ? Config.Load() : Config.New();
        Instances = Instance.IsExist() ? Instance.Load() : Instance.New();
        Themes = Theme.IsExist() ? Theme.Load() : Theme.New();

        SelectedTheme = Themes.Themes.Find(x => x.Name == Config.Theme) ?? SettingsDefaults.DefaultTheme;

        ProxyService = new ProxyService();
        
        Logger.Info("Providers loaded");
    }
}