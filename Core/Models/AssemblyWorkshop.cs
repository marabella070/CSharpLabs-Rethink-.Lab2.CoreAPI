using CoreAPI.Core.Models;
using CoreAPI.Core.Interfaces;
using Lab2.CoreAPI.Core.Interfaces;

using System.Text;
using System.Drawing;
using System.Runtime.Versioning;

namespace Lab2.CoreAPI.Core.Models;

public class AssemblyWorkshop : Workshop, IDisplayable, IDrawable
{
    public const uint MIN_LINES_NUMBER = 1;
    public const uint MAX_LINES_NUMBER = 10_000;
    public static readonly string ImagePath;
    private static readonly Image _image;
    public uint AssemblyLines { get; set; }
    public bool UsesAutomation { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int ImageWidth { get; set; } = 30;
    public int ImageHeight { get; set; } = 30;

    [SupportedOSPlatform("windows")]
    static AssemblyWorkshop()
    {
        ImagePath = "../WorkshopsImages/assembly_workshop_image.jpg";
        _image = Image.FromFile(ImagePath);
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
                            bool usesAutomation)
        : base(name, 
               manager, 
               workerCount, 
               productList,
               id,
               brigades,
               shifts,
               schedule)
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
