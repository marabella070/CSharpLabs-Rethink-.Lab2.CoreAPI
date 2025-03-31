using CoreAPI.Core.Models;
using CoreAPI.Core.Interfaces;

using System.Text;

namespace Core;

public class AssemblyWorkshop : Workshop, IIdentifiable
{
    public const uint MIN_LINES_NUMBER = 1;
    public const uint MAX_LINES_NUMBER = 10_000;

    public uint AssemblyLines { get; set; }
    public bool UsesAutomation { get; set; }

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
