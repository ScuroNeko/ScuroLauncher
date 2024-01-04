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

    private List<API.GameItem> _genshinList = [];
    private List<API.GameItem> _hsrList = [];
    private List<API.GameItem> _honkaiList = [];

    public NewInstanceForm(MainForm mainForm)
    {
        _mainForm = mainForm;
        _newInstance = new InstanceItem();
        _importInstance = new InstanceItem();

        InitializeComponent();
        LoadTheme(mainForm.SelectedTheme);
    }

    private void BuildVersionList()
    {
        var versions = _instanceType switch
        {
            InstanceType.Hsr => _hsrList,
            InstanceType.Honkai => _honkaiList,
            _ => _genshinList,
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
            InstanceType.Hsr => _hsrList,
            InstanceType.Honkai => _honkaiList,
            _ => _genshinList,
        };
        // _version = versions.Where(v => v.name == selectedItem.ToString()).First();
        _newInstance.Version = selectedItem.ToString();
    }

    private void Button_EnabledChanged(object sender, EventArgs e)
    {
        if (sender is not Button buttonSender) return;
        buttonSender.BackColor = buttonSender.Enabled ? ColorTranslator.FromHtml(_mainForm.SelectedTheme.SurfaceColor) : ColorTranslator.FromHtml(_mainForm.SelectedTheme.OverlayColor);
        buttonSender.ForeColor = ColorTranslator.FromHtml(_mainForm.SelectedTheme.TextColor);
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
        _instanceType = InstanceType.Hsr;
        if (_hsrList.Count == 0)
            _hsrList = await API.MurasakiAPI.GetHsrVersions();
        BuildVersionList();
    }

    private void Honkai_Click(object sender, EventArgs e)
    {
        Genshin.Enabled = true;
        Hsr.Enabled = true;
        Honkai.Enabled = false;
        _instanceType = InstanceType.Honkai;
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
        _mainForm.Instances.AddInstance(_newInstance);
        _mainForm.Instances.Save();
        _mainForm.LoadInstances();
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
        if (File.Exists(Path.Combine(_importInstance.Path, "StarRail.exe"))) _importInstance.Type = InstanceType.Hsr;
        var iniFile = new IniFile(Path.Combine(_importInstance.Path, "config.ini"));
        _importInstance.Version = $@"Release {iniFile.Read("game_version", "General")}";
        _mainForm.Instances.AddInstance(_importInstance);
        _mainForm.Instances.Save();
        _mainForm.LoadInstances();
        Close();
    }

    private void LoadTheme(ThemeItem selectedTheme)
    {
        BackColor = ColorTranslator.FromHtml(selectedTheme.BackgroundColor);
        TabPage1.BackColor = ColorTranslator.FromHtml(selectedTheme.BackgroundColor);
        TabPage2.BackColor = ColorTranslator.FromHtml(selectedTheme.BackgroundColor);

        // Create instance
        VersionSelector.BackColor = ColorTranslator.FromHtml(selectedTheme.SurfaceColor);
        VersionSelector.ForeColor = ColorTranslator.FromHtml(selectedTheme.TextColor);

        InstanceName.BackColor = ColorTranslator.FromHtml(selectedTheme.SurfaceColor);
        InstanceName.ForeColor = ColorTranslator.FromHtml(selectedTheme.TextColor);

        InstancePath.BackColor = ColorTranslator.FromHtml(selectedTheme.SurfaceColor);
        InstancePath.ForeColor = ColorTranslator.FromHtml(selectedTheme.TextColor);

        Genshin.BackColor = ColorTranslator.FromHtml(selectedTheme.SurfaceColor);
        Genshin.ForeColor = ColorTranslator.FromHtml(selectedTheme.TextColor);
        Hsr.BackColor = ColorTranslator.FromHtml(selectedTheme.SurfaceColor);
        Hsr.ForeColor = ColorTranslator.FromHtml(selectedTheme.TextColor);
        Honkai.BackColor = ColorTranslator.FromHtml(selectedTheme.SurfaceColor);
        Honkai.ForeColor = ColorTranslator.FromHtml(selectedTheme.TextColor);

        ChooseFolder.BackColor = ColorTranslator.FromHtml(selectedTheme.SurfaceColor);
        ChooseFolder.ForeColor = ColorTranslator.FromHtml(selectedTheme.TextColor);

        CreateInstance.BackColor = ColorTranslator.FromHtml(selectedTheme.SurfaceColor);
        CreateInstance.ForeColor = ColorTranslator.FromHtml(selectedTheme.TextColor);

        ReleasesCheckBox.ForeColor = ColorTranslator.FromHtml(selectedTheme.TextColor);
        BetaCheckBox.ForeColor = ColorTranslator.FromHtml(selectedTheme.TextColor);
        
        // Import instance
        ImportInstanceName.BackColor = ColorTranslator.FromHtml(selectedTheme.SurfaceColor);
        ImportInstanceName.ForeColor = ColorTranslator.FromHtml(selectedTheme.TextColor);
        
        ImportInstancePath.BackColor = ColorTranslator.FromHtml(selectedTheme.SurfaceColor);
        ImportInstancePath.ForeColor = ColorTranslator.FromHtml(selectedTheme.TextColor);
        
        ImportChooseFolder.BackColor = ColorTranslator.FromHtml(selectedTheme.SurfaceColor);
        ImportChooseFolder.ForeColor = ColorTranslator.FromHtml(selectedTheme.TextColor);
        ImportInstance.BackColor = ColorTranslator.FromHtml(selectedTheme.SurfaceColor);
        ImportInstance.ForeColor = ColorTranslator.FromHtml(selectedTheme.TextColor);
    }
}