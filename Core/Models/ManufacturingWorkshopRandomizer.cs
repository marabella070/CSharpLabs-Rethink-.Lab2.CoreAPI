using CoreAPI.Core.Models;
using CoreAPI.Core.Helpers;

namespace Lab2.CoreAPI.Core.Models;

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

    public static ManufacturingWorkshop Generate()
    {
        var workshopFields = WorkshopRandomizer.GenerateWorkshopFields();
        var manufacturingFields = GenerateManufacturingWorkshopFields();

        return new ManufacturingWorkshop(
            workshopFields.productionName, workshopFields.manager, workshopFields.workerCount, 
            workshopFields.productList, workshopFields.workshopId, workshopFields.brigades, 
            workshopFields.shifts, workshopFields.schedule, manufacturingFields.machinesCount, 
            manufacturingFields.hasHazardousMaterials
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