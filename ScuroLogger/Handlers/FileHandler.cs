namespace ScuroLogger.Handlers;

public class FileHandler(string filename, string path = "./logs") : Handler
{
    public override void Log(LogLevels level, string msg)
    {
        var logFilePath = Path.GetFullPath(Path.Combine(path, filename));
        if (!File.Exists(logFilePath)) Directory.CreateDirectory(path);
        using var sw = File.AppendText(logFilePath);
        sw.WriteLine(msg);
    }
}