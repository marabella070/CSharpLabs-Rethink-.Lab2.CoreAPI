using CoreAPI.Core.Models;
using CoreAPI.Core.Helpers;
using CoreAPI.Core.Randomizers;

using Lab2.CoreAPI.Core.Models;

namespace Lab2.CoreAPI.Core.Randomizers;

public static class ManufacturingWorkshopRandomizer
{
    // Use Random.Shared for thread-safe random number generation (available from .NET 6+)
    private static readonly Random random = Random.Shared;

    private static (uint machinesCount, bool hasHazardousMaterials) GenerateManufacturingWorkshopFields()
    {
        uint machinesCount = RandomHelper.GenerateRandomInRange(ManufacturingWorkshop.MIN_MACHINES_NUMBER, ManufacturingWorkshop.MAX_MACHINES_NUMBER, "Machines Count");
        bool hasHazardousMaterials = random.Next(2) == 0;

        return (machinesCount, hasHazardousMaterials);
    }

    public static ManufacturingWorkshop Generate(CoordinateRange? xRange = null, CoordinateRange? yRange = null)
    {
        var workshopFields = WorkshopRandomizer.GenerateWorkshopFields(xRange, yRange);
        var manufacturingFields = GenerateManufacturingWorkshopFields();

        return new ManufacturingWorkshop(
            workshopFields.productionName, workshopFields.manager, workshopFields.workerCount, 
            workshopFields.productList, workshopFields.workshopId, workshopFields.brigades, 
            workshopFields.shifts, workshopFields.schedule, manufacturingFields.machinesCount, 
            manufacturingFields.hasHazardousMaterials, workshopFields.x, workshopFields.y        
        );
    }

    // Generate a sequence of random workshops based on the given count
    public static IEnumerable<ManufacturingWorkshop> GenerateMultiple(int count, CoordinateRange? xRange = null, CoordinateRange? yRange = null)
    {
        for (int i = 0; i < count; i++)
        {
            yield return Generate(xRange, yRange); // Lazily generates a random workshop
        }
    }
}