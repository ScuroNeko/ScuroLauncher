using ScuroLauncher.Settings;
using ScuroLauncher.Updaters;
using System.Runtime.InteropServices;
using System.Diagnostics;
using ScuroLauncher.API;

namespace ScuroLauncher;

public partial class MainForm : Form
{
    private DownloadsForm downloadsForm;

    public Config Config;
    public Instance Instances;
    public Theme Themes;

    public ThemeItem SelectedTheme;
    public InstanceItem SelectedInstance = new() { Icon = "", Name = "" };

    public MainForm()
    {
        InitializeComponent();

        Config = Config.IsExist() ? Config.Load() : Config.New();
        Instances = Instance.IsExist() ? Instance.Load() : Instance.New();
        Themes = Theme.IsExist() ? Theme.Load() : Theme.New();

        SelectedTheme = Themes.Themes.Find(x => x.Name == Config.Theme) ?? SettingsDefaults.DefaultTheme;

        if (Config.Debug) AllocConsole();

        downloadsForm = new DownloadsForm(this);
        LoadTheme(SelectedTheme);
        LoadInstances();
    }

    private void Launch_Click(object sender, EventArgs e)
    {
        var exeName = "";
        switch (SelectedInstance.Type)
        {
            case InstanceType.Genshin:
                exeName = "GenshinImpact.exe";
                break;
            case InstanceType.Hsr:
                exeName = "StarRail.exe";
                break;
            case InstanceType.Honkai:
                break;
            case InstanceType.Unknown:
            default:
                break;
        }

        if (exeName.Length <= 0) return;
        var startInfo = new ProcessStartInfo
        {
            WorkingDirectory = SelectedInstance.Path,
            FileName = Path.Combine(SelectedInstance.Path, exeName),
            UseShellExecute = true
        };
        Process.Start(startInfo);
    }

    private void EditInstance_Click(object sender, EventArgs e)
    {
        var editInstanceForm = new EditInstanceForm(this, SelectedInstance);
        editInstanceForm.Show();
    }

    private async void CheckUpdates_Click(object sender, EventArgs e)
    {
        downloadsForm.AddDownloadTask("https://autopatchhk.yuanshen.com/client_app/pc_diff/10/1.0.0_1.0.1_diff_cSQJ5eOD.zip");
        var updateInfo = await GenshinUpdater.Check(SelectedInstance.Path);
        

        if (updateInfo.hasUpdate)
        {
            MessageBox.Show("No new updates", "No new updates");
        }
        else
        {
            var result = MessageBox.Show("New version " + updateInfo.newVersion + " is available!\nUpdate now?", "Update available", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                await GenshinUpdater.DoUpdate();
            }
        }
    }

    private void DeleteInstance_Click(object sender, EventArgs e)
    {
        var result = MessageBox.Show("Are you sure to delete this instance?", "Are you sure?", MessageBoxButtons.YesNo);
        if (result != DialogResult.Yes) return;
        Instances.Instances.Remove(SelectedInstance);
        Instances.Save();
        LoadInstances();
    }
    
    private void NewInstance_Click(object sender, EventArgs e)
    {
        var newInstanceForm = new NewInstanceForm(this);
        newInstanceForm.Show();
    }

    private void InstanceList_MouseClick(object sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Right) return;

        var focusedItem = InstanceList.FocusedItem;
        if (focusedItem == null) return;

        if (focusedItem.Bounds.Contains(e.Location))
        {
            var context = new ContextMenuStrip();
            context.Items.Add(new ToolStripMenuItem("Rename"));
            context.Items.Add(new ToolStripMenuItem("Delete"));
            context.Show(Cursor.Position);
        }
        else
        {
            var context = new ContextMenuStrip();
            context.Items.Add(new ToolStripMenuItem("New instance"));
            context.Show(Cursor.Position);
        }
    }

    private void InstanceList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (InstanceList.SelectedItems.Count == 0)
        {
            InstanceImage.Image = null;
            InstanceName.Visible = false;
            InstanceVersion.Visible = false;
            LaunchInstance.Visible = false;
            EditInstance.Visible = false;
            CheckUpdates.Visible = false;
            DeleteInstance.Visible = false;
            return;
        }
        var selectedItem = InstanceList.SelectedItems[0];
        InstanceName.Visible = true;
        InstanceVersion.Visible = true;
        LaunchInstance.Visible = true;
        EditInstance.Visible = true;
        CheckUpdates.Visible = true;
        DeleteInstance.Visible = true;

        SelectedInstance = Instances.Instances.Find(x => x.Name == selectedItem.Text);
        InstanceName.Text = SelectedInstance.Name;
        InstanceVersion.Text = SelectedInstance.Version;
        InstanceImage.Image = Utils.LoadImageForInstance(SelectedInstance);
    }

    private void SettingsToolMenuItem_Click(object sender, EventArgs e)
    {
        var settingsForm = new SettingsForm(this);
        settingsForm.Show();
    }

    public void LoadInstances()
    {
        InstanceList.Items.Clear();

        var imageList = new ImageList();
        Instances.Instances.ForEach(item =>
        {
            var img = Utils.LoadImageForInstance(item);
            imageList.Images.Add(img);
        });
        imageList.ImageSize = new Size(64, 64);

        InstanceList.SmallImageList = imageList;
        InstanceList.LargeImageList = imageList;
        var index = 0;
        Instances.Instances.ForEach(item =>
        {
            var listViewItem = new ListViewItem { Text = item.Name, ImageIndex = index++ };
            InstanceList.Items.Add(listViewItem);
        });
    }

    public void LoadTheme(ThemeItem selectedTheme)
    {
        BackColor = ColorTranslator.FromHtml(selectedTheme.BackgroundColor);

        InstanceName.ForeColor = ColorTranslator.FromHtml(selectedTheme.TextColor);
        InstanceVersion.ForeColor = ColorTranslator.FromHtml(selectedTheme.TextColor);

        LaunchInstance.BackColor = ColorTranslator.FromHtml(selectedTheme.SurfaceColor);
        LaunchInstance.ForeColor = ColorTranslator.FromHtml(selectedTheme.TextColor);
        EditInstance.BackColor = ColorTranslator.FromHtml(selectedTheme.SurfaceColor);
        EditInstance.ForeColor = ColorTranslator.FromHtml(selectedTheme.TextColor);
        CheckUpdates.BackColor = ColorTranslator.FromHtml(selectedTheme.SurfaceColor);
        CheckUpdates.ForeColor = ColorTranslator.FromHtml(selectedTheme.TextColor);
        DeleteInstance.BackColor = ColorTranslator.FromHtml(selectedTheme.SurfaceColor);
        DeleteInstance.ForeColor = ColorTranslator.FromHtml(selectedTheme.TextColor);

        MainMenu.BackColor = ColorTranslator.FromHtml(selectedTheme.BackgroundColor);
        MainMenu.ForeColor = ColorTranslator.FromHtml(selectedTheme.TextColor);

        InstanceList.BackColor = ColorTranslator.FromHtml(selectedTheme.SurfaceColor);
        InstanceList.ForeColor = ColorTranslator.FromHtml(selectedTheme.TextColor);

        FileCategoryMenuItem.BackColor = ColorTranslator.FromHtml(selectedTheme.BackgroundColor);
        FileCategoryMenuItem.ForeColor = ColorTranslator.FromHtml(selectedTheme.TextColor);
        EditCategoryMenuItem.BackColor = ColorTranslator.FromHtml(selectedTheme.BackgroundColor);
        EditCategoryMenuItem.ForeColor = ColorTranslator.FromHtml(selectedTheme.TextColor);
        SettingsToolMenuItem.BackColor = ColorTranslator.FromHtml(selectedTheme.BackgroundColor);
        SettingsToolMenuItem.ForeColor = ColorTranslator.FromHtml(selectedTheme.TextColor);

        NewInstance.BackColor = ColorTranslator.FromHtml(selectedTheme.SurfaceColor);
        NewInstance.ForeColor = ColorTranslator.FromHtml(selectedTheme.TextColor);
        NewInstance.Font = new Font(selectedTheme.Font, NewInstance.Font.Size, NewInstance.Font.Style);
    }

    public void LoadSettings()
    {
        Instances = Instance.Load();
        Config = Config.Load();
        Themes = Theme.Load();
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool AllocConsole();

    private void progressBar1_Click(object sender, EventArgs e)
    {

    }

    private void downloadsManagerToolStripMenuItem_Click(object sender, EventArgs e)
    {
        downloadsForm.Show();
    }
}