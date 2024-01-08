using ScuroLogger.Colors;

namespace ScuroLogger;

public enum LogLevels : byte
{
    None,
    Info,
    Warn,
    Error,
    Fatal,
    Debug
}

public static class ColoredLevels
{
    private const string None = "";
    private static readonly string Info = ColorUtils.Format("Info", ForeColors.Cyan);
    private static readonly string Warn = ColorUtils.Format("Warn", ForeColors.Yellow);
    private static readonly string Error = ColorUtils.Format("Error", ForeColors.BrightRed);
    private static readonly string Fatal = ColorUtils.Format("Fatal", ForeColors.Red);
    private static readonly string Debug = ColorUtils.Format("Debug", ForeColors.Green);

    public static string GetColored(LogLevels level)
    {
        return level switch
        {
            LogLevels.Info => Info,
            LogLevels.Warn => Warn,
            LogLevels.Error => Error,
            LogLevels.Fatal => Fatal,
            LogLevels.Debug => Debug,
            _ => None // Just to shutoff warning
        };
    }
}