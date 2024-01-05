namespace ScuroLauncher.Settings;

public class SettingsDefaults
{
    // Themes
    public static readonly ThemeItem CatppuccinLatte = new()
    {
        Name = "Catppuccin Latte",
        Font = "Segoe UI",
        BackgroundColor = ColorTranslator.FromHtml("#eff1f5"),
        SurfaceColor = ColorTranslator.FromHtml("#ccd0da"),
        OverlayColor = ColorTranslator.FromHtml("#9ca0b0"),
        TextColor = ColorTranslator.FromHtml("#4c4f69"),
        AccentColor = ColorTranslator.FromHtml("#8839ef")
    };
    public static readonly ThemeItem CatppuccinMocha = new()
    {
        Name = "Catppuccin Mocha",
        Font = "Segoe UI",
        BackgroundColor = ColorTranslator.FromHtml("#1e1e2e"),
        SurfaceColor = ColorTranslator.FromHtml("#313244"),
        OverlayColor = ColorTranslator.FromHtml("#6c7086"),
        TextColor = ColorTranslator.FromHtml("#cdd6f4"),
        AccentColor = ColorTranslator.FromHtml("#cba6f7")
    };
    public static ThemeItem DefaultTheme = CatppuccinLatte;
    public static string DefaultThemeName = CatppuccinLatte.Name;
}

