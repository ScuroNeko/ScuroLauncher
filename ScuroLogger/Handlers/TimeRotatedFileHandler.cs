namespace ScuroLogger.Handlers;

public enum TimeRotateInterval : byte
{
    Hourly,
    Daily,
    Weekly,
    Midnight // Rotate in 00:00 every day
}

public class TimeRotatedFileHandler : Handler
{
    public TimeRotateInterval Interval = TimeRotateInterval.Daily;
    public string DateTimeFormat = "dd.MM.yyyy";
    
    private DateTime _openTime = DateTime.Now;
    private string _filename;
    private readonly string _baseFilename;
    private readonly string _ext;
    private readonly string _path;

    public TimeRotatedFileHandler(string baseFilename, string path = "./logs", string ext = ".log")
    {
        _baseFilename = baseFilename;
        _path = path;
        _ext = ext;
        
        _filename = GenerateFilename();
    }

    private string GenerateFilename()
    {
        return $"{_baseFilename}-{_openTime.ToString(DateTimeFormat)}{_ext}";
    }

    private void RotateLog()
    {
        _openTime = DateTime.Now;
        _filename = GenerateFilename();
    }
    
    public override void Log(LogLevels level, string msg)
    {
        var now = DateTime.Now;
        switch (Interval)
        {
            case TimeRotateInterval.Hourly:
                if(now.Subtract(_openTime).Minutes >= 60) RotateLog();
                break;
            case TimeRotateInterval.Daily:
                if(now.Subtract(_openTime).Days >= 1) RotateLog();
                break;
            case TimeRotateInterval.Weekly:
                if(now.Subtract(_openTime).Days >= 7) RotateLog();
                break;
            case TimeRotateInterval.Midnight:
            default:
                break;
        }
        
        var logFilePath = Path.GetFullPath(Path.Combine(_path, _filename));
        if (!File.Exists(logFilePath)) Directory.CreateDirectory(_path);
        using var sw = File.AppendText(logFilePath);
        sw.WriteLine(msg);
    }
}