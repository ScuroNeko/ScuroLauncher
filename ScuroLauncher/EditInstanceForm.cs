using ScuroLauncher.Settings;

namespace ScuroLauncher;

public partial class EditInstanceForm : Form
{
    private readonly MainForm mainForm;
    private readonly InstanceItem _instance;
    private readonly string _oldInstanceName;

    public EditInstanceForm(MainForm mainForm, InstanceItem instance)
    {
        this.mainForm = mainForm;
        _instance = instance;
        _oldInstanceName = _instance.Name;

        InitializeComponent();
        InitializeValues();
        LoadTheme();
    }

    private void Save_Click(object sender, EventArgs e)
    {
        Providers.Instances.UpdateInstance(_instance, _oldInstanceName);
        Providers.Instances.Save();
        mainForm.LoadInstances();
        Close();
    }

    private void InstanceName_TextChanged(object sender, EventArgs e)
    {
        _instance.Name = InstanceName.Text;
    }

    private void InstancePath_TextChanged(object sender, EventArgs e)
    {
        _instance.Path = InstancePath.Text;
    }

    private void UseProxy_CheckedChanged(object sender, EventArgs e)
    {
        _instance.UseProxy = UseProxy.Checked;
    }

    private void ProxyUrl_TextChanged(object sender, EventArgs e)
    {
        _instance.ProxyUrl = ProxyUrl.Text;
    }

    private void InstanceImage_Click(object sender, EventArgs e)
    {
        using var fileDialog = new OpenFileDialog
        {
            InitialDirectory = ".",
            Filter = "Image files|*.png;*.jpg;*.jpeg|All files|*.*"
        };

        var result = fileDialog.ShowDialog();
        if (result != DialogResult.OK) return;
        var filePath = fileDialog.FileName;
        _instance.Icon = filePath;
        InstanceImage.Image = Image.FromFile(filePath);
    }

    private void InitializeValues()
    {
        InstanceImage.Image = Utils.LoadImageForInstance(_instance);
        InstanceName.Text = _instance.Name;
        InstancePath.Text = _instance.Path;

        UseProxy.Checked = _instance.UseProxy;
        ProxyUrl.Text = _instance.ProxyUrl;
    }

    private void LoadTheme()
    {
        BackColor = Providers.SelectedTheme.BackgroundColor;

        InstanceName.BackColor = Providers.SelectedTheme.SurfaceColor;
        InstanceName.ForeColor = Providers.SelectedTheme.TextColor;
        InstancePath.BackColor = Providers.SelectedTheme.SurfaceColor;
        InstancePath.ForeColor = Providers.SelectedTheme.TextColor;

        UseProxy.ForeColor = Providers.SelectedTheme.TextColor;
        ProxyUrl.BackColor = Providers.SelectedTheme.SurfaceColor;
        ProxyUrl.ForeColor = Providers.SelectedTheme.TextColor;

        Save.BackColor = Providers.SelectedTheme.SurfaceColor;
        Save.ForeColor = Providers.SelectedTheme.TextColor;
    }
}
