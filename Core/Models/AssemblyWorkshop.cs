using CoreAPI.Core.Models;
using CoreAPI.Core.Interfaces;
using CoreAPI.Core.Helpers;

using System.Text;
using System.Drawing;
using System.Runtime.Versioning;
using System.Runtime.InteropServices;

namespace Lab2.CoreAPI.Core.Models;

public class AssemblyWorkshop : Workshop, IDisplayable, IDrawable, IMoveable
{
    public const uint MIN_LINES_NUMBER = 1;
    public const uint MAX_LINES_NUMBER = 10_000;
    private static Image _image;
    private static string _imageResourcePath;
    private static string ImageResourcePath => _imageResourcePath;
    public uint AssemblyLines { get; set; }
    public bool UsesAutomation { get; set; }

    [SupportedOSPlatform("windows")]
    static AssemblyWorkshop()
    {
        _imageResourcePath = "Core.WorkshopsImages.assembly_workshop_image.jpg";
        string assemblyName = "Core";

        // Uploading the image if the path has changed
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            _image = ResourceLoader.LoadImageFromAssembly(_imageResourcePath, assemblyName)
                ?? throw new InvalidOperationException(
                    string.Format("Failed to load image from path: {0}.", _imageResourcePath));
        }
        else 
        {
            throw new PlatformNotSupportedException(
                $"Image loading is supported only on Windows. Tried path: {_imageResourcePath}");
        }
    }

    public AssemblyWorkshop(string name, 
                            string manager, 
                            uint workerCount, 
                            List<string> productList, 
                            uint id,  
                            List<Brigade> brigades,
                            List<Shift> shifts,
                            List<ScheduleElement> schedule,
                            uint assemblyLines,
                            bool usesAutomation,
                            int? x = null,
                            int? y = null,
                            IMovementFunction? movementFunction = null,
                            int? imageWidth = null,
                            int? imageHeight = null)
        : base(name, 
               manager, 
               workerCount, 
               productList,
               id,
               brigades,
               shifts,
               schedule,
               x,
               y,
               movementFunction,
               imageWidth,
               imageHeight)
    {
        AssemblyLines = assemblyLines;
        UsesAutomation = usesAutomation;
    }

    [SupportedOSPlatform("windows")]
    public void Draw(Graphics g)
    {
        // drawing the object at the current coordinates
        g.DrawImage(_image, X, Y, ImageWidth, ImageHeight);
    }

    public string GetAssemblyWorkshopInfo()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Assembly lines: {AssemblyLines}");
        sb.AppendLine($"Automation: {(UsesAutomation ? "Yes" : "No")}\n");

        return sb.ToString();
    }

    public string GetShortAssemblyWorkshopInfo()
    {
        return GetAssemblyWorkshopInfo();
    }

    public override void ShowInfo(Action<string> output)
    {
        base.ShowInfo(output);
        output("\n" + GetAssemblyWorkshopInfo());
    }

    public override void ShowShortInfo(Action<string> output)
    {
        base.ShowShortInfo(output);
        output("\n" + GetShortAssemblyWorkshopInfo());
    }

    public override string ToString()
    {
        string productionPart = GetProductionInfo();
        string workshopPart = GetWorkshopInfo();
        string assemblyPart = GetAssemblyWorkshopInfo();

        return productionPart + "\n" + workshopPart + "\n" + assemblyPart;
    }
}
