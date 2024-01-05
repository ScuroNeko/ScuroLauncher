using System.IO.Compression;

namespace ScuroLauncher;

public enum DownloadTaskType: byte
{
    DOWNLOAD,
    UNZIP,
    HDIFF,
}

public record DownloadTask
{
    public string Name { get; set; } = "";
    public string? Url { get; set; }
    public Label TaskProgress { get; set; }
    public ProgressBar ProgressBar { get; set; }
    public Panel ProgressPanel { get; set; }
    public DownloadTaskType TaskType { get; set; }
}

public partial class DownloadsForm : Form
{
    private int _taskY = 12;
    private const int TaskYStep = 72;
    private readonly List<DownloadTask> _tasks = [];
    private DownloadTask? _currentTask;
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
                    case DownloadTaskType.DOWNLOAD:
                    {
                        var fileSize = await API.API.GetFileSize(_currentTask.Url);
                        await foreach (var progress in API.API.Download(_currentTask.Url))
                        {
                            if(IsHandleCreated) Invoke(UpdateProgress, [progress, fileSize]);
                            Console.WriteLine($@"{Utils.SizeSuffix(progress)}/{Utils.SizeSuffix(fileSize)} {(float)progress / fileSize * 100L}");
                        }
                        RemoveTask(_currentTask);
                        _currentTask = null;
                        break;
                    }
                    case DownloadTaskType.UNZIP:
                        break;
                    case DownloadTaskType.HDIFF:
                        break;
                    default:
                        Console.WriteLine($@"Unknown task type! Report this to developer:\n{_currentTask}");
                        break;
                }
            }
        }
    }

    public void AddDownloadTask(string url)
    {
        var task = new DownloadTask { Name = Path.GetFileName(url), Url = url, TaskType = DownloadTaskType.DOWNLOAD };
        _tasks.Add(task);
        DrawTask(task);
    }

    public void AddUnzipTask(string name)
    {
        var task = new DownloadTask { Name = name, TaskType = DownloadTaskType.UNZIP };
        _tasks.Add(task);
        DrawTask(task);

        using var file = File.OpenRead(name);
        using var zip = new ZipArchive(file, ZipArchiveMode.Read);
        
        // TODO Count all entries and total unzipped for progress
        zip.Entries.ToList().ForEach(entry =>
        {
            entry.Open();
        });
    }

    public void AddHdiffTask(string name)
    {
        var task = new DownloadTask { Name = name, TaskType = DownloadTaskType.HDIFF };
    }

    public void RemoveTask(DownloadTask task)
    {
        _tasks.Remove(task);
        task.TaskProgress.Text = "Done";
    }

    private void DrawTask(DownloadTask task)
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
            Text = @"",
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
        
        Console.WriteLine($@"New {task.TaskType} {task} was register");

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
