using ScuroLauncher.Settings;

namespace ScuroLauncher;

public partial class SettingsForm : Form
{
    private string _selectedTheme;
    private readonly MainForm _mainForm;

    public SettingsForm(MainForm mainForm)
    {
        InitializeComponent();

        _mainForm = mainForm;
        ThemeComboBox.Text = mainForm.Config.Theme;
        mainForm.Themes.Themes.ForEach(theme =>
        {
            ThemeComboBox.Items.Add(theme.Name);
        });
        LoadTheme(mainForm.SelectedTheme);
    }

    private void ThemeComboBoxSelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void Save_Click(object sender, EventArgs e)
    {
        _selectedTheme = ThemeComboBox.SelectedItem?.ToString() ?? SettingsDefaults.DefaultThemeName;
        var selectedThemeItem = _mainForm.Themes.Themes.Find(x => x.Name == _selectedTheme) ?? SettingsDefaults.DefaultTheme;
        _mainForm.Config.Theme = selectedThemeItem.Name;
        _mainForm.Config.Save();
        _mainForm.SelectedTheme = selectedThemeItem;
        _mainForm.LoadTheme(selectedThemeItem);
        LoadTheme(selectedThemeItem);
    }

    private void LoadTheme(ThemeItem theme)
    {
        BackColor = ColorTranslator.FromHtml(theme.BackgroundColor);
        
        ThemeComboBox.BackColor = ColorTranslator.FromHtml(theme.SurfaceColor);
        ThemeComboBox.ForeColor = ColorTranslator.FromHtml(theme.TextColor);
        
        Save.BackColor = ColorTranslator.FromHtml(theme.SurfaceColor);
        Save.ForeColor = ColorTranslator.FromHtml(theme.TextColor);
    }
}