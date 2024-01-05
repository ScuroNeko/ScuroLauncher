namespace ScuroLauncher;

public partial class AboutWindow : Form
{
    public AboutWindow()
    {
        InitializeComponent();
        // Utils.OpenUrl("https://github.com/ScuroNeko/ScuroLauncher");

        dotnet.Text += Environment.Version.ToString();
        os.Text += Environment.OSVersion.ToString();

        LoadTheme();
    }

    private void LoadTheme()
    {
        var theme = Providers.SelectedTheme;

        BackColor = theme.BackgroundColor;
        Title.ForeColor = theme.TextColor;
        Developer.ForeColor = theme.TextColor;
        dotnet.ForeColor = theme.TextColor;
        os.ForeColor = theme.TextColor;
    }
}
