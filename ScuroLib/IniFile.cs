using System.Runtime.InteropServices;
using System.Text;

namespace ScuroLib;

public partial class IniFile(string iniPath)
{
    private readonly string _path = new FileInfo(iniPath).FullName;

    [LibraryImport("kernel32", StringMarshalling = StringMarshalling.Utf16)]
    private static partial long WritePrivateProfileString(string section, string? key, string? value, string filePath);

    [DllImport("kernel32", CharSet = CharSet.Unicode)]
    private static extern int GetPrivateProfileString(string section, string key, string @default, StringBuilder retVal, int size, string filePath);

    public string Read(string key, string section)
    {
        var retVal = new StringBuilder(255);
        GetPrivateProfileString(section, key, "", retVal, 255, _path);
        return retVal.ToString();
    }

    public void Write(string? key, string? value, string section)
    {
        WritePrivateProfileString(section, key, value, _path);
    }

    public void DeleteKey(string key, string section)
    {
        Write(key, null, section);
    }

    public void DeleteSection(string section)
    {
        Write(null, null, section);
    }

    public bool KeyExists(string key, string section)
    {
        return Read(key, section).Length > 0;
    }
}