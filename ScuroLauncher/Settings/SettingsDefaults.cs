using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScuroLauncher.Settings;

public class SettingsDefaults
{
    // Themes
    public static readonly ThemeItem CatppuccinLatte = new()
    {
        Name = "Catppuccin Latte",
        Font = "Segoe UI",
        BackgroundColor = "#eff1f5",
        SurfaceColor = "#ccd0da",
        OverlayColor = "#9ca0b0",
        TextColor = "#4c4f69",
        AccentColor = "#8839ef"
    };
    public static readonly ThemeItem CatppuccinMocha = new()
    {
        Name = "Catppuccin Mocha",
        Font = "Segoe UI",
        BackgroundColor = "#1e1e2e",
        SurfaceColor = "#313244",
        OverlayColor = "#6c7086",
        TextColor = "#cdd6f4",
        AccentColor = "#cba6f7"
    };
    public static ThemeItem DefaultTheme = CatppuccinLatte;
    public static string DefaultThemeName = CatppuccinLatte.Name;
}

