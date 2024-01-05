using ScuroLauncher.Settings;
using ScuroLauncher.Updaters;
using ScuroLauncher.RSAPatch;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Security.Policy;

namespace ScuroLauncher;

public partial class MainForm : Form
{
    // private readonly DownloadsForm _downloadsForm;
    private InstanceItem? _selectedInstance;

    private InstanceItem? runningInstance;
    private Process? runningInstanceProcess;

    public MainForm()
    {
        InitializeComponent();
        Providers.Load();

        if (Providers.Config.Debug) AllocConsole();

        Providers.DownloadsForm = new DownloadsForm();
        LoadTheme(Providers.SelectedTheme);
        LoadInstances();
    }

    ~MainForm()
    {
        Providers.ProxyService.Stop();
    }

    private void GameProcess_Exited(object? sender, EventArgs e)
    {
        Providers.ProxyService.Stop();

        // Clear running instance
        runningInstance = null;
        runningInstanceProcess = null;
    }

    private void Kill_Click(object? sender, EventArgs e)
    {
        if (runningInstanceProcess == null) throw new NullReferenceException();
        runningInstanceProcess.Kill();
        runningInstanceProcess = null;
        runningInstance = null;
        Providers.ProxyService.Stop();
        DrawInstanceInfo(_selectedInstance != null);
    }

    private void Launch_Click(object? sender, EventArgs e)
    {
        var exeName = "";
        if (_selectedInstance == null) throw new NullReferenceException();
        switch (_selectedInstance.Type)
        {
            case InstanceType.Genshin:
                exeName = "GenshinImpact.exe";
                break;
            case InstanceType.StarRail:
                exeName = "StarRail.exe";
                break;
            case InstanceType.Honkai:
                break;
            case InstanceType.Unknown:
            default:
                break;
        }
        if (exeName.Length <= 0) return;

        // Settings up Proxy
        if (_selectedInstance.UseProxy)
        {
            Providers.ProxyService.RedirectUrl = _selectedInstance.ProxyUrl;
            Providers.ProxyService.Start();
        }
        var startInfo = new ProcessStartInfo
        {
            WorkingDirectory = _selectedInstance.Path,
            FileName = Path.Combine(_selectedInstance.Path, exeName),
            UseShellExecute = true
        };

        runningInstance = _selectedInstance;
        runningInstanceProcess = Process.Start(startInfo);
        if (runningInstanceProcess != null) runningInstanceProcess.Exited += GameProcess_Exited;
        DrawInstanceInfo(true);
    }

    private void EditInstance_Click(object? sender, EventArgs e)
    {
        if (_selectedInstance == null) throw new NullReferenceException();
        var editInstanceForm = new EditInstanceForm(this, _selectedInstance);
        editInstanceForm.Show();
    }

    private async void CheckUpdates_Click(object? sender, EventArgs e)
    {
        // TODO
        // _downloadsForm.AddDownloadTask("https://autopatchhk.yuanshen.com/client_app/pc_diff/10/1.0.0_1.0.1_diff_cSQJ5eOD.zip");
        Genshin.PatchGame(_selectedInstance);
        if (_selectedInstance == null) throw new NullReferenceException();
        UpdateInfo updateInfo;
        if (_selectedInstance.Type == InstanceType.StarRail)
            updateInfo = await StarRailUpdater.Check(_selectedInstance.Path);
        else if (_selectedInstance.Type == InstanceType.Genshin)
            updateInfo = await GenshinUpdater.Check(_selectedInstance.Path);
        else
        {
            MessageBox.Show("This game currently cannot be updated :(", "Error!");
            return;
        }

        if (!updateInfo.HasUpdate)
        {
            MessageBox.Show("No new updates", "No new updates");
        }
        else
        {
            var result = MessageBox.Show("New version " + updateInfo.NewVersion + " is available!\nUpdate now?", "Update available", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                await GenshinUpdater.DoUpdate();
            }
        }
    }

    private void DeleteInstance_Click(object? sender, EventArgs e)
    {
        if (_selectedInstance == null) throw new NullReferenceException();
        var result = MessageBox.Show("Are you sure to delete this instance?", "Are you sure?", MessageBoxButtons.YesNo);
        if (result != DialogResult.Yes) return;
        Providers.Instances.Instances.Remove(_selectedInstance);
        Providers.Instances.Save();
        LoadInstances();
        InstanceList.SelectedItems.Clear();
        DrawInstanceInfo(false);
    }

    private void NewInstance_Click(object? sender, EventArgs e)
    {
        var newInstanceForm = new NewInstanceForm(this);
        newInstanceForm.Show();
    }

    // Right-click handler
    private void InstanceList_MouseClick(object? sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Right) return;

        var focusedItem = InstanceList.FocusedItem;
        Console.WriteLine(focusedItem);
        if (focusedItem == null) return;

        if (focusedItem.Bounds.Contains(e.Location))
        {
            var context = new ContextMenuStrip();
            var launchMenuItem = new ToolStripMenuItem("Launch");
            var editMenuItem = new ToolStripMenuItem("Edit");
            var deleteMenuItem = new ToolStripMenuItem("Delete");
            launchMenuItem.Click += Launch_Click;
            editMenuItem.Click += EditInstance_Click;
            deleteMenuItem.Click += DeleteInstance_Click;
            context.Items.Add(launchMenuItem);
            context.Items.Add(new ToolStripSeparator());
            context.Items.Add(editMenuItem);
            context.Items.Add(deleteMenuItem);
            context.Show(Cursor.Position);
        }
        else
        {
            var context = new ContextMenuStrip();
            var newInstanceMenuItem = new ToolStripMenuItem("New instance");
            newInstanceMenuItem.Click += NewInstance_Click;
            context.Items.Add(newInstanceMenuItem);
            context.Show(Cursor.Position);
        }
    }

    private void DrawInstanceInfo(bool visible)
    {
        InstanceImage.Visible = visible;
        InstanceName.Visible = visible;
        InstanceVersion.Visible = visible;
        EditInstance.Visible = visible;
        CheckUpdates.Visible = visible;
        DeleteInstance.Visible = visible;
        LaunchInstance.Visible = visible;
        Kill.Visible = visible;

        // If any instance running, disable launch button
        if (runningInstanceProcess != null && _selectedInstance != runningInstance)
        {
            LaunchInstance.Enabled = false;
            LaunchInstance.Visible = visible;
            Kill.Visible = false;
        }
        else if (runningInstanceProcess != null && _selectedInstance == runningInstance)
        {
            LaunchInstance.Visible = false;
            Kill.Visible = visible;
        }
        else if (_selectedInstance != null)
        {
            LaunchInstance.Enabled = true;
            LaunchInstance.Visible = visible;
            Kill.Visible = false;
        }
        else
        {
            LaunchInstance.Visible = false;
            Kill.Visible = false;
        }
    }

    // Select an instance
    private void InstanceList_SelectedIndexChanged(object? sender, EventArgs e)
    {
        DrawInstanceInfo(InstanceList.SelectedItems.Count != 0);
        if (InstanceList.SelectedItems.Count == 0) return;
        var selectedItem = InstanceList.SelectedItems[0];

        _selectedInstance = Providers.Instances.Instances.Find(x => x.Name == selectedItem.Text);
        if (_selectedInstance == null) throw new NullReferenceException();
        InstanceName.Text = _selectedInstance.Name;
        InstanceVersion.Text = _selectedInstance.Version;
        InstanceImage.Image = Utils.LoadImageForInstance(_selectedInstance);
    }

    private void SettingsToolMenuItem_Click(object? sender, EventArgs e)
    {
        var settingsForm = new SettingsForm(this);
        settingsForm.Show();
    }

    public void LoadInstances()
    {
        InstanceList.Items.Clear();

        var imageList = new ImageList();
        Providers.Instances.Instances.ForEach(item =>
        {
            var img = Utils.LoadImageForInstance(item);
            if (img != null) imageList.Images.Add(img);
        });
        imageList.ImageSize = new Size(64, 64);

        InstanceList.SmallImageList = imageList;
        InstanceList.LargeImageList = imageList;
        var index = 0;
        Providers.Instances.Instances.ForEach(item =>
        {
            var listViewItem = new ListViewItem { Text = item.Name, ImageIndex = index++ };
            InstanceList.Items.Add(listViewItem);
        });
    }

    public void LoadTheme(ThemeItem selectedTheme)
    {
        BackColor = selectedTheme.BackgroundColor;

        InstanceName.ForeColor = selectedTheme.TextColor;
        InstanceVersion.ForeColor = selectedTheme.TextColor;

        Kill.BackColor = selectedTheme.SurfaceColor;
        Kill.ForeColor = selectedTheme.TextColor;
        LaunchInstance.BackColor = selectedTheme.SurfaceColor;
        LaunchInstance.ForeColor =  selectedTheme.TextColor;
        EditInstance.BackColor = selectedTheme.SurfaceColor;
        EditInstance.ForeColor = selectedTheme.TextColor;
        CheckUpdates.BackColor = selectedTheme.SurfaceColor;
        CheckUpdates.ForeColor = selectedTheme.TextColor;
        DeleteInstance.BackColor = selectedTheme.SurfaceColor;
        DeleteInstance.ForeColor = selectedTheme.TextColor;

        MainMenu.BackColor = selectedTheme.BackgroundColor;
        MainMenu.ForeColor = selectedTheme.TextColor;

        InstanceList.BackColor = selectedTheme.SurfaceColor;
        InstanceList.ForeColor = selectedTheme.TextColor;

        FileCategoryMenuItem.BackColor = selectedTheme.BackgroundColor;
        FileCategoryMenuItem.ForeColor = selectedTheme.TextColor;
        EditCategoryMenuItem.BackColor = selectedTheme.BackgroundColor;
        EditCategoryMenuItem.ForeColor = selectedTheme.TextColor;
        SettingsToolMenuItem.BackColor = selectedTheme.BackgroundColor;
        SettingsToolMenuItem.ForeColor = selectedTheme.TextColor;

        NewInstance.BackColor = selectedTheme.SurfaceColor;
        NewInstance.ForeColor = selectedTheme.TextColor;
        NewInstance.Font = new Font(selectedTheme.Font, NewInstance.Font.Size, NewInstance.Font.Style);
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool AllocConsole();

    private void DownloadsManagerToolStripMenuItem_Click(object? sender, EventArgs e)
    {
        Providers.DownloadsForm.Show();
    }

    private void AboutMenuItem_Click(object sender, EventArgs e)
    {
        var form = new AboutWindow();
        form.Show();
    }
}