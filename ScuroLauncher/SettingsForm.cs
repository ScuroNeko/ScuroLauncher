using ScuroLauncher.Settings;

namespace ScuroLauncher;

public partial class SettingsForm : Form
{
    private string _selectedTheme = "";
    private readonly MainForm _mainForm;

    public SettingsForm(MainForm mainForm)
    {
        InitializeComponent();

        _mainForm = mainForm;
        ThemeComboBox.Text = Providers.Config.Theme;
        Providers.Themes.Themes.ForEach(theme =>
        {
            ThemeComboBox.Items.Add(theme.Name);
        });
        LoadTheme(Providers.SelectedTheme);
    }

    private void ThemeComboBoxSelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void Save_Click(object sender, EventArgs e)
    {
        _selectedTheme = ThemeComboBox.SelectedItem?.ToString() ?? SettingsDefaults.DefaultThemeName;
        var selectedThemeItem = Providers.Themes.Themes.Find(x => x.Name == _selectedTheme) ?? SettingsDefaults.DefaultTheme;
        Providers.Config.Theme = selectedThemeItem.Name;
        Providers.Config.Save();
        Providers.SelectedTheme = selectedThemeItem;
        _mainForm.LoadTheme(selectedThemeItem);
        LoadTheme(selectedThemeItem);
    }

    private void LoadTheme(ThemeItem theme)
    {
        BackColor = theme.BackgroundColor;
        
        ThemeComboBox.BackColor = theme.SurfaceColor;
        ThemeComboBox.ForeColor = theme.TextColor;
        
        Save.BackColor = theme.SurfaceColor;
        Save.ForeColor = theme.TextColor;
    }
}