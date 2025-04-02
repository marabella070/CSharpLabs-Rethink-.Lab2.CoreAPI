using CoreAPI.Core.Models;
using CoreAPI.Core.Interfaces;
using Lab2.CoreAPI.Core.Interfaces;

using System.Text;
using System.Drawing;
using System.Runtime.Versioning;

namespace Lab2.CoreAPI.Core.Models;

public class ManufacturingWorkshop : Workshop, IDisplayable, IDrawable
{
    public const uint MIN_MACHINES_NUMBER = 1;
    public const uint MAX_MACHINES_NUMBER = 10_000;
    public static readonly string ImagePath;
    private static readonly Image _image;
    public uint MachinesCount { get; set; }
    public bool HasHazardousMaterials { get; set; }
    public int X { get; set; } = 0;
    public int Y { get; set; } = 0;
    public int ImageWidth { get; set; } = 30;
    public int ImageHeight { get; set; } = 30;

    [SupportedOSPlatform("windows")]
    static ManufacturingWorkshop()
    {
        ImagePath = "../../WorkshopsImages/manufacturing_workshop_1.jpg";
        _image = Image.FromFile(ImagePath);
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
                                 bool hasHazardousMaterials)
        : base(name, 
               manager, 
               workerCount, 
               productList,
               id,
               brigades,
               shifts,
               schedule)
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

    public string GetAssemblyWorkshopInfo()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Machines count: {MachinesCount}");
        sb.AppendLine($"Hazardous materials: {(HasHazardousMaterials ? "Yes" : "No")}\n");

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
