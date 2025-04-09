using CoreAPI.Core.Helpers;
using CoreAPI.Core.Models;
using CoreAPI.Core.Randomizers;
using Lab2.CoreAPI.Core.Models;

namespace Lab2.CoreAPI.Core.Randomizers;

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

    public static AssemblyWorkshop Generate(CoordinateRange? xRange = null, CoordinateRange? yRange = null)
    {
        var workshopFields = WorkshopRandomizer.GenerateWorkshopFields(xRange, yRange);

        var assemblyFields = GenerateAssemblyWorkshopFields();

        return new AssemblyWorkshop(
            workshopFields.productionName, workshopFields.manager, workshopFields.workerCount, 
            workshopFields.productList, workshopFields.workshopId, workshopFields.brigades, 
            workshopFields.shifts, workshopFields.schedule, assemblyFields.assemblyLines, 
            assemblyFields.usesAutomation, workshopFields.x, workshopFields.y
        );
    }

    // Generate a sequence of random workshops based on the given count
    public static IEnumerable<AssemblyWorkshop> GenerateMultiple(int count, CoordinateRange? xRange = null, CoordinateRange? yRange = null)
    {
        for (int i = 0; i < count; i++)
        {
            yield return Generate(xRange, yRange); // Lazily generates a random workshop
        }
    }
}