using System;

namespace ScuroLogger.Colors;

public static class ColorUtils
{
    
    public static string Format(string s, ForeColors? fore = null, BackColors? back = null, Styles? styles = null)
    {
        if (fore == null && back == null) throw new ArgumentException("One of argument must be");
        var outString = "";
        if (fore != null) outString += ColorToString((byte)fore);
        if (back != null) outString += ColorToString((byte)back);
        if (styles != null) outString += ColorToString((byte)styles);
        return outString+s+Reset();
    }
    
    public static string ColorToString(byte code) => $"\u001b[{code}m";
    public static string Reset() => ColorToString((byte)Styles.Reset);
}