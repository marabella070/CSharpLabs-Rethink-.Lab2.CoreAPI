using CoreAPI.Core.Models;
using CoreAPI.Core.Helpers;

namespace Lab2.CoreAPI.Core.Models;

public static class AssemblyWorkshopRandomizer
{
    // Use Random.Shared for thread-safe random number generation (available from .NET 6+)
    private static readonly Random random = Random.Shared;

    private static (uint assemblyLines, bool usesAutomation) GenerateAssemblyWorkshopFields()
    {
        uint assemblyLines = RandomHelper.GenerateRandomInRange(AssemblyWorkshop.MIN_LINES_NUMBER, AssemblyWorkshop.MAX_LINES_NUMBER, "Assembly Lines");
        bool usesAutomation = random.Next(2) == 0;

        return (assemblyLines, usesAutomation);
    }

    public static AssemblyWorkshop Generate()
    {
        var workshopFields = WorkshopRandomizer.GenerateWorkshopFields();
        var assemblyFields = GenerateAssemblyWorkshopFields();

        return new AssemblyWorkshop(
            workshopFields.productionName, workshopFields.manager, workshopFields.workerCount, 
            workshopFields.productList, workshopFields.workshopId, workshopFields.brigades, 
            workshopFields.shifts, workshopFields.schedule, assemblyFields.assemblyLines, 
            assemblyFields.usesAutomation
        );
    }

    // Generate a sequence of random workshops based on the given count
    public static IEnumerable<Workshop> GenerateMultiple(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return Generate(); // Lazily generates a random workshop
        }
    }
}