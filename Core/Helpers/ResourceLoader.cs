using System.IO;
using System.Reflection;
using System.Drawing;
using System.Runtime.Versioning;

namespace Lab2.CoreAPI.Core.Helpers;

public static class ResourceLoader
{
    [SupportedOSPlatform("windows")]
    public static Image? LoadImage(string resourceName)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using (Stream? stream = assembly.GetManifestResourceStream(resourceName))
        {
            return (stream != null) ? Image.FromStream(stream) : null;
        }
    }
}