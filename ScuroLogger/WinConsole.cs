using System.Runtime.InteropServices;

namespace ScuroLogger;

public class WinConsole
{
    public static void CreateConsole()
    {
        AllocConsole();

        var defaultStdout = new IntPtr(7);
        var currentStdout = GetStdHandle(StdOutputHandle);

        if (currentStdout != defaultStdout)
            SetStdHandle(StdOutputHandle, defaultStdout);

        TextWriter writer = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };
        Console.SetOut(writer);
    }

    private const uint StdOutputHandle = 0xFFFFFFF5;

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetStdHandle(uint nStdHandle);
    
    [DllImport("kernel32.dll")]
    private static extern void SetStdHandle(uint nStdHandle, IntPtr handle);
    
    [DllImport("kernel32")]
    private static extern bool AllocConsole();
}
