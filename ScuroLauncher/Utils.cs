using ScuroLauncher.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ScuroLauncher;

internal static class Utils
{
    public static Image? LoadImageForInstance(InstanceItem instance)
    {
        if(File.Exists(instance.Icon)) return Image.FromFile(instance.Icon);

        return instance.Type switch
        {
            InstanceType.Genshin => Properties.Resources.default_genshin_icon,
            InstanceType.StarRail => Properties.Resources.default_hsr_icon,
            InstanceType.Honkai => Properties.Resources.default_honkai_icon,
            InstanceType.Zzz => Properties.Resources.default_zzz_icon,
            _ => null
        };
    }

    public static void OpenUrl(string url)
    {
        try
        {
            Process.Start(url);
        }
        catch
        {
            // hack because of this: https://github.com/dotnet/corefx/issues/10361
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                url = url.Replace("&", "^&");
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            else
            {
                throw;
            }
        }
    }

    private static readonly string[] SizeSuffixes = ["bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB"];
    public static string SizeSuffix(long value, int decimalPlaces = 1)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(decimalPlaces);
        switch (value)
        {
            case < 0:
                return "-" + SizeSuffix(-value, decimalPlaces);
            case 0:
                return string.Format("{0:n" + decimalPlaces + "} bytes", 0);
        }

        // mag is 0 for bytes, 1 for KB, 2, for MB, etc.
        var mag = (int)Math.Log(value, 1024);

        // 1L << (mag * 10) == 2 ^ (10 * mag) 
        // [i.e. the number of bytes in the unit corresponding to mag]
        var adjustedSize = (decimal)value / (1L << (mag * 10));

        // make adjustment when the value is large enough that
        // it would round up to 1000 or more
        if (Math.Round(adjustedSize, decimalPlaces) < 1000)
            return string.Format("{0:n" + decimalPlaces + "} {1}",
                adjustedSize,
                SizeSuffixes[mag]);
        mag += 1;
        adjustedSize /= 1024;

        return string.Format("{0:n" + decimalPlaces + "} {1}", 
            adjustedSize, 
            SizeSuffixes[mag]);
    }
}
