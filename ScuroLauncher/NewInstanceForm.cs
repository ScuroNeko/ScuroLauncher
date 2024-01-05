using ScuroLauncher.Settings;
using System;
using ScuroLauncher.Lib;

namespace ScuroLauncher;

public partial class NewInstanceForm : Form
{
    private readonly MainForm _mainForm;
    
    private readonly InstanceItem _newInstance;
    private readonly InstanceItem _importInstance;
    private InstanceType _instanceType;
    private string _instanceDownloadUrl;

    private List<API.GameItem> _genshinList = [];
    private List<API.GameItem> _starRailList = [];
    private List<API.GameItem> _honkaiList = [];

    public NewInstanceForm(MainForm mainForm)
    {
        _mainForm = mainForm;
        _newInstance = new InstanceItem();
        _importInstance = new InstanceItem();

        InitializeComponent();
        LoadTheme(Providers.SelectedTheme);
    }

    private void BuildVersionList()
    {
        var versions = _instanceType switch
        {
            InstanceType.StarRail => _starRailList,
            InstanceType.Honkai => _honkaiList,
            InstanceType.Genshin => _genshinList,
            InstanceType.Zzz => [],
            _ => []
        };
        VersionSelector.Items.Clear();
        versions.ForEach(v =>
        {
            if (ReleasesCheckBox.Checked && !v.beta) VersionSelector.Items.Add(v.name);
            if (BetaCheckBox.Checked && v.beta) VersionSelector.Items.Add(v.name);
        });
    }

    private void VersionSelector_SelectedIndexChanged(object sender, EventArgs e)
    {
        var selectedItem = VersionSelector.SelectedItem;
        if (selectedItem == null) return;
        var versions = _instanceType switch
        {
            InstanceType.StarRail => _starRailList,
            InstanceType.Honkai => _honkaiList,
            InstanceType.Genshin => _genshinList,
            InstanceType.Zzz => [],
            _ => []
        };
        var version = versions.Find(v => v.name == selectedItem.ToString()) ?? throw new NullReferenceException();
        var link = version.links.Find(link => link.name.StartsWith("Client")) ?? throw new NullReferenceException();
        _instanceDownloadUrl = link.url;
        _newInstance.Version = selectedItem.ToString() ?? throw new NullReferenceException();
    }

    private void Button_EnabledChanged(object sender, EventArgs e)
    {
        if (sender is not Button buttonSender) return;
        buttonSender.BackColor = buttonSender.Enabled ? Providers.SelectedTheme.SurfaceColor : Providers.SelectedTheme.OverlayColor;
        buttonSender.ForeColor = Providers.SelectedTheme.TextColor;
    }

    private async void Genshin_Click(object sender, EventArgs e)
    {
        Genshin.Enabled = false;
        Hsr.Enabled = true;
        Honkai.Enabled = true;
        _instanceType = InstanceType.Genshin;

        if (_genshinList.Count == 0)
            _genshinList = await API.MurasakiAPI.GetGenshinVersions();
        BuildVersionList();
    }

    private async void Hsr_Click(object sender, EventArgs e)
    {
        Genshin.Enabled = true;
        Hsr.Enabled = false;
        Honkai.Enabled = true;
        _instanceType = InstanceType.StarRail;
        if (_starRailList.Count == 0)
            _starRailList = await API.MurasakiAPI.GetStarRailVersions();
        BuildVersionList();
    }

    private async void Honkai_Click(object sender, EventArgs e)
    {
        Genshin.Enabled = true;
        Hsr.Enabled = true;
        Honkai.Enabled = false;
        _instanceType = InstanceType.Honkai;
        if(_honkaiList.Count == 0)
            _honkaiList = await API.MurasakiAPI.GetHonkaiImpactVersions();
        BuildVersionList();
    }
    private void ReleasesCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        BuildVersionList();
    }

    private void BetaCheckBox_CheckedChanged(object sender, EventArgs e)
    {
        BuildVersionList();
    }

    private void ChooseFolder_Click(object sender, EventArgs e)
    {
        using var folderDialog = new FolderBrowserDialog();
        var result = folderDialog.ShowDialog();

        if (result != DialogResult.OK || string.IsNullOrWhiteSpace(folderDialog.SelectedPath)) return;
        InstancePath.Text = folderDialog.SelectedPath;
        _newInstance.Path = folderDialog.SelectedPath;
    }

    private void InstanceName_TextChanged(object sender, EventArgs e)
    {
        _newInstance.Name = InstanceName.Text;
        if (InstancePath.Text.Length > 0 && InstanceName.Text.Length > 0) CreateInstance.Enabled = true;
    }

    private void InstancePath_TextChanged(object sender, EventArgs e)
    {
        _newInstance.Path = InstancePath.Text;
        if (InstancePath.Text.Length > 0 && InstanceName.Text.Length > 0) CreateInstance.Enabled = true;
    }

    private void CreateInstance_Click(object sender, EventArgs e)
    {
        _newInstance.Type = _instanceType;
        Providers.Instances.AddInstance(_newInstance);
        Providers.Instances.Save();
        _mainForm.LoadInstances();
        Providers.DownloadsForm.AddDownloadTask(_instanceDownloadUrl);
        Close();
    }

    private void ImportInstanceName_TextChanged(object sender, EventArgs e)
    {
        _importInstance.Name = ImportInstanceName.Text;
        if (ImportInstancePath.Text.Length > 0 && ImportInstanceName.Text.Length > 0) ImportInstance.Enabled = true;
    }

    private void ImportInstancePath_TextChanged(object sender, EventArgs e)
    {
        _importInstance.Path = ImportInstancePath.Text;
        if (ImportInstancePath.Text.Length > 0 && ImportInstanceName.Text.Length > 0) ImportInstance.Enabled = true;
    }

    private void ImportChooseFolder_Click(object sender, EventArgs e)
    {
        using var folderDialog = new FolderBrowserDialog();
        var result = folderDialog.ShowDialog();

        if (result != DialogResult.OK || string.IsNullOrWhiteSpace(folderDialog.SelectedPath)) return;
        ImportInstancePath.Text = folderDialog.SelectedPath;
        _importInstance.Path = folderDialog.SelectedPath;
    }

    private void ImportInstance_Click(object sender, EventArgs e)
    {
        if (File.Exists(Path.Combine(_importInstance.Path, "GenshinImpact.exe"))) _importInstance.Type = InstanceType.Genshin;
        if (File.Exists(Path.Combine(_importInstance.Path, "StarRail.exe"))) _importInstance.Type = InstanceType.StarRail;
        var iniFile = new IniFile(Path.Combine(_importInstance.Path, "config.ini"));
        var version = iniFile.Read("game_version", "General");
        var channel = iniFile.Read("cps", "General") == "beta" ? "Beta" : "Release";
        _importInstance.Version = $@"{channel} {version}";
        Providers.Instances.AddInstance(_importInstance);
        Providers.Instances.Save();
        _mainForm.LoadInstances();
        Close();
    }

    private void LoadTheme(ThemeItem selectedTheme)
    {
        BackColor = selectedTheme.BackgroundColor;
        TabPage1.BackColor = selectedTheme.BackgroundColor;
        TabPage2.BackColor = selectedTheme.BackgroundColor;

        // Create instance
        VersionSelector.BackColor = selectedTheme.SurfaceColor;
        VersionSelector.ForeColor = selectedTheme.TextColor;

        InstanceName.BackColor = selectedTheme.SurfaceColor;
        InstanceName.ForeColor = selectedTheme.TextColor;

        InstancePath.BackColor = selectedTheme.SurfaceColor;
        InstancePath.ForeColor = selectedTheme.TextColor;

        Genshin.BackColor = selectedTheme.SurfaceColor;
        Genshin.ForeColor = selectedTheme.TextColor;
        Hsr.BackColor = selectedTheme.SurfaceColor;
        Hsr.ForeColor = selectedTheme.TextColor;
        Honkai.BackColor = selectedTheme.SurfaceColor;
        Honkai.ForeColor = selectedTheme.TextColor;

        ChooseFolder.BackColor = selectedTheme.SurfaceColor;
        ChooseFolder.ForeColor = selectedTheme.TextColor;

        CreateInstance.BackColor = selectedTheme.SurfaceColor;
        CreateInstance.ForeColor = selectedTheme.TextColor;

        ReleasesCheckBox.ForeColor = selectedTheme.TextColor;
        BetaCheckBox.ForeColor = selectedTheme.TextColor;
        
        // Import instance
        ImportInstanceName.BackColor = selectedTheme.SurfaceColor;
        ImportInstanceName.ForeColor = selectedTheme.TextColor;
        
        ImportInstancePath.BackColor = selectedTheme.SurfaceColor;
        ImportInstancePath.ForeColor = selectedTheme.TextColor;
        
        ImportChooseFolder.BackColor = selectedTheme.SurfaceColor;
        ImportChooseFolder.ForeColor = selectedTheme.TextColor;
        ImportInstance.BackColor = selectedTheme.SurfaceColor;
        ImportInstance.ForeColor = selectedTheme.TextColor;
    }
}