using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace ScuroLauncher.Lib;

public class IniFile
{
    string _path;

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

    public IniFile(string IniPath)
    {
        _path = new FileInfo(IniPath).FullName;
    }

    public string Read(string Key, string Section)
    {
        var retVal = new StringBuilder(255);
        GetPrivateProfileString(Section, Key, "", retVal, 255, _path);
        return retVal.ToString();
    }

    public void Write(string? Key, string? Value, string Section)
    {
        WritePrivateProfileString(Section, Key, Value, _path);
    }

    public void DeleteKey(string Key, string Section)
    {
        Write(Key, null, Section);
    }

    public void DeleteSection(string Section)
    {
        Write(null, null, Section);
    }

    public bool KeyExists(string Key, string Section)
    {
        return Read(Key, Section).Length > 0;
    }
}