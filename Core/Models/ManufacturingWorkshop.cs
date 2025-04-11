using CoreAPI.Core.Models;
using CoreAPI.Core.Interfaces;
using CoreAPI.Core.Helpers;

using System.Text;
using System.Drawing;
using System.Runtime.Versioning;
using System.Runtime.InteropServices;

namespace Lab2.CoreAPI.Core.Models;

public class ManufacturingWorkshop : Workshop, IDisplayable, IDrawable, IMoveable
{
    public const uint MIN_MACHINES_NUMBER = 1;
    public const uint MAX_MACHINES_NUMBER = 10_000;
    private static Image _image;
    private static string _imageResourcePath;
    private static string ImageResourcePath => _imageResourcePath;
    public uint MachinesCount { get; set; }
    public bool HasHazardousMaterials { get; set; }

    [SupportedOSPlatform("windows")]
    static ManufacturingWorkshop()
    {
        _imageResourcePath = "Core.WorkshopsImages.manufacturing_workshop_1.jpg";
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

    public ManufacturingWorkshop(string name, 
                                 string manager, 
                                 uint workerCount, 
                                 List<string> productList, 
                                 uint id,  
                                 List<Brigade> brigades,
                                 List<Shift> shifts,
                                 List<ScheduleElement> schedule,
                                 uint machinesCount,
                                 bool hasHazardousMaterials,
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
        MachinesCount = machinesCount;
        HasHazardousMaterials = hasHazardousMaterials;
    }

    [SupportedOSPlatform("windows")]
    public void Draw(Graphics g)
    {
        // drawing the object at the current coordinates
        g.DrawImage(_image, X, Y, ImageWidth, ImageHeight);
    }

    public string GetManufacturingWorkshopInfo()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Machines count: {MachinesCount}");
        sb.AppendLine($"Hazardous materials: {(HasHazardousMaterials ? "Yes" : "No")}\n");

        return sb.ToString();
    }
    public string GetShortManufacturingWorkshopInfo()
    {
        return GetManufacturingWorkshopInfo();
    }
    public override void ShowInfo(Action<string> output)
    {
        base.ShowInfo(output);
        output("\n" + GetManufacturingWorkshopInfo());
    }

    public override void ShowShortInfo(Action<string> output)
    {
        base.ShowShortInfo(output);
        output("\n" + GetShortManufacturingWorkshopInfo());
    }

    public override string ToString()
    {
        string productionPart = GetProductionInfo();
        string workshopPart = GetWorkshopInfo();
        string assemblyPart = GetManufacturingWorkshopInfo();

        return productionPart + "\n" + workshopPart + "\n" + assemblyPart;
    }
}
