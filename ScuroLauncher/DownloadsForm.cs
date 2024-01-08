using System.IO.Compression;
using ScuroLogger;
using ScuroHttp;
using ScuroUpdater;

namespace ScuroLauncher;

public enum TaskType: byte
{
    DOWNLOAD,
    UNZIP,
    HDIFF,
}

public record WorkTask {
    public string Name { get; set; } = "";
    public Label TaskProgress { get; set; }
    public ProgressBar ProgressBar { get; set; }
    public Panel ProgressPanel { get; set; }
    public TaskType TaskType { get; set; }
    
    public string Url { get; set; } = "";
    public string OutDirectory { get; set; } = Path.Combine(Path.GetTempPath(), "ScuroLauncher");
    public string InputFilePath { get; set; } = "";
}

public partial class DownloadsForm : Form
{
    private int _taskY = 12;
    private const int TaskYStep = 72;
    private readonly List<WorkTask> _tasks = [];
    private WorkTask? _currentTask;
    private bool _isRunning = true;

    public DownloadsForm()
    {
        InitializeComponent();

        LoadTheme();
        Draw();
        Task.Run(Worker);
    }

    ~DownloadsForm()
    {
        _isRunning = false;
    }

    private async void Worker()
    {
        while (_isRunning) 
        {
            if(_currentTask == null)
            {
                // Waiting for new tasks
                if (_tasks.Count == 0) await Task.Delay(100);
                else _currentTask = _tasks[0]; // Take first task to work
            }
            else
            {
                switch (_currentTask.TaskType)
                {
                    case TaskType.DOWNLOAD:
                    {
                        if (_currentTask.Url == null)
                        {
                            Providers.Logger.Error($"No url for task: {_currentTask}");
                            RemoveTask(_currentTask);
                            break;
                        }
                        var fileSize = await Api.GetFileSize(_currentTask.Url);
                        await foreach (var progress in Api.Download(_currentTask.Url, _currentTask.OutDirectory))
                        {
                            if(IsHandleCreated)
                                Invoke(UpdateProgress, [progress, fileSize]);
                            Providers.Logger.Debug($"{Utils.SizeSuffix(progress)}/{Utils.SizeSuffix(fileSize)} {Math.Round((float)progress / fileSize * 100L, 2)}");
                        }
                        RemoveTask(_currentTask);
                        break;
                    }
                    case TaskType.UNZIP:
                    {
                        await using var file = File.OpenRead(_currentTask.Name);
                        using var zip = new ZipArchive(file, ZipArchiveMode.Read);
                        var total = zip.Entries.Count;
                        var done = 0;
                        var buffer = new byte[8192];
                        var outDirectory = Path.Combine(_currentTask.OutDirectory,
                            Path.GetFileNameWithoutExtension(_currentTask.Name));
                        foreach (var entry in zip.Entries)
                        {
                            var stream = entry.Open();
                            var read = await stream.ReadAsync(buffer);
                            
                            var path = Path.Combine(outDirectory, entry.FullName);
                            if (!Directory.Exists(path))
                                Directory.CreateDirectory(Path.GetDirectoryName(path)!);
                            await using var outFile = File.OpenWrite(path);
                            await outFile.WriteAsync(buffer.AsMemory(0, read));
                            
                            Providers.Logger.Info($"{entry.FullName} Progress: {done}/{total}");
                            done++;
                        }
                        RemoveTask(_currentTask);
                        break;
                    }
                    case TaskType.HDIFF:
                        Updater.CheckHdiff(Path.Combine(_currentTask.OutDirectory, _currentTask.Name));
                        break;
                    default:
                        Providers.Logger.Error($@"Unknown task type! Report this to developer:\n{_currentTask}");
                        break;
                }
            }
        }
    }

    public void AddDownloadTask(string url)
    {
        var task = new WorkTask { Name = Path.GetFileName(url), Url = url, TaskType = TaskType.DOWNLOAD };
        _tasks.Add(task);
        DrawTask(task);
    }

    public void AddUnzipTask(string name)
    {
        var task = new WorkTask { Name = name, TaskType = TaskType.UNZIP };
        _tasks.Add(task);
        DrawTask(task);
    }

    public void AddHdiffTask(string name)
    {
        var task = new WorkTask { Name = name, TaskType = TaskType.HDIFF };
        _tasks.Add(task);
        DrawTask(task);
    }

    public void RemoveTask(WorkTask task)
    {
        _tasks.Remove(task);
        _currentTask = null;
        task.TaskProgress.Text = "Done";
    }

    private void DrawTask(WorkTask task)
    {
        var taskPanel = new Panel
        {
            Name = task.Name,
            Size = new Size(Panel.Width-24, 60),
            Location = new Point(12, _taskY),
            BackColor = Providers.SelectedTheme.SurfaceColor,
        };
        var taskTitle = new Label
        {
            Name = "TaskTitle",
            Text = $@"{task.TaskType} {task.Name}",
            Location = new Point(12, 12),
            AutoSize = true,
            ForeColor = Providers.SelectedTheme.TextColor,
        };
        var taskProgress = new Label
        {
            Text = "",
            Location = new Point(taskTitle.Width + 24, 12),
            ForeColor = Providers.SelectedTheme.TextColor,
        };
        var taskProgressBar = new ProgressBar { 
            Location = new Point(12, 36),
            Size = new Size(taskPanel.Width-24, 12),
            Minimum = 0,
            Value = 0,
            Maximum = 100,
        };

        taskPanel.Controls.Add(taskTitle);
        taskPanel.Controls.Add(taskProgress);
        taskPanel.Controls.Add(taskProgressBar);
        
        Panel.Controls.Add(taskPanel);
        
        task.ProgressBar = taskProgressBar;
        task.TaskProgress = taskProgress;
        task.ProgressPanel = taskPanel;
        
        Providers.Logger.Info($@"New {task.TaskType} {task} was register");

        _taskY += TaskYStep;
    }

    private void UpdateProgress(long progress, long total)
    {
        if (_currentTask == null) throw new Exception();
        var progressString = $@"{Utils.SizeSuffix(progress)}/{Utils.SizeSuffix(total)} {Math.Round((double)progress / total * 100d, 2)}%";
        _currentTask.ProgressBar.Value = (int)((float)progress / total * 100);
        _currentTask.TaskProgress.Text = progressString;
        
        var taskTitle = _currentTask.ProgressPanel.Controls.Find("TaskTitle", false).First();
        _currentTask.TaskProgress.Location = new Point(taskTitle.Width+24, 12);
        _currentTask.TaskProgress.Width = TextRenderer.MeasureText(progressString, _currentTask.TaskProgress.Font).Width;
    }
    
    private void Draw()
    {
        Panel.Controls.Clear();
        _taskY = 12;
        _tasks.ForEach(DrawTask);
    }

    private void LoadTheme()
    {
        var selectedTheme = Providers.SelectedTheme;
        Panel.BackColor = selectedTheme.BackgroundColor;
    }

    private void OnClose(object sender, FormClosingEventArgs e)
    {
        Hide();
        e.Cancel = true;
    }

    private void OnResize(object sender, EventArgs e)
    {
        Draw();
    }
}
