using CoreAPI.Core.Models;
using CoreAPI.Core.Interfaces;
using Lab2.CoreAPI.Core.Interfaces;
using Lab2.CoreAPI.Core.Helpers;
using Lab2.CoreAPI.Core.Movements;

using System.Text;
using System.Drawing;
using System.Runtime.Versioning;

namespace Lab2.CoreAPI.Core.Models;

public class AssemblyWorkshop : Workshop, IDisplayable, IDrawable, IMoveable
{
    public const uint MIN_LINES_NUMBER = 1;
    public const uint MAX_LINES_NUMBER = 10_000;
    private static readonly Image _image;
    private static readonly string ImageResourcePath = "Core.WorkshopsImages.assembly_workshop_image.jpg";
    private static readonly string ImageLoadErrorMessage = "Failed to load assembly workshop image from embedded resources. Path: {0}";
    public uint AssemblyLines { get; set; }
    public bool UsesAutomation { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int ImageWidth { get; set; }
    public int ImageHeight { get; set; }
    public IMovementFunction MovementFunction { get; set; }

    [SupportedOSPlatform("windows")]
    static AssemblyWorkshop()
    {
        _image = ResourceLoader.LoadImage(ImageResourcePath)
                    ?? throw new InvalidOperationException(
                        string.Format(ImageLoadErrorMessage, ImageResourcePath));
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
                            int x = 0,
                            int y = 0,
                            IMovementFunction? movementFunction = null,
                            int imageWidth = 30,
                            int imageHeight = 30)
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
        ImageWidth = imageWidth;
        ImageHeight = imageHeight;
        X = x;
        Y = y;
        MovementFunction = movementFunction ?? new EmptyMovement();
    }

    [SupportedOSPlatform("windows")]
    public void Draw(Graphics g)
    {
        // drawing the object at the current coordinates
        g.DrawImage(_image, X, Y, ImageWidth, ImageHeight);
    }

    public void Move(double timeElapsed, int boundaryX, int boundaryY)
    {
        // Calculating the shift along the axes using a function of time
        var (dx, dy) = MovementFunction.Shift(timeElapsed);
        X += dx;
        Y += dy;

        // Handling collisions with window borders
        if (X < 0 || X > boundaryX) dx = -dx;
        if (Y < 0 || Y > boundaryY) dy = -dy;

        X += dx;
        Y += dy;
    }

    public string GetAssemblyWorkshopInfo()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Assembly lines: {AssemblyLines}");
        sb.AppendLine($"Automation: {(UsesAutomation ? "Yes" : "No")}\n");
        sb.AppendLine($"Position: X = {X}, Y = {Y}");
        sb.AppendLine($"Image Size: {ImageWidth} x {ImageHeight}");
        sb.AppendLine($"Movement Function: {MovementFunction.GetType().Name}");

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
