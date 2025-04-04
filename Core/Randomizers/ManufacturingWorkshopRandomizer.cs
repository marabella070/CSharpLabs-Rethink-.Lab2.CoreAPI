using CoreAPI.Core.Models;
using CoreAPI.Core.Helpers;

using Lab2.CoreAPI.Core.Models;
using Lab2.CoreAPI.Core.Interfaces;
using Lab2.CoreAPI.Core.Movements;

namespace Lab2.CoreAPI.Core.Randomizers;

public static class ManufacturingWorkshopRandomizer
{
    // Use Random.Shared for thread-safe random number generation (available from .NET 6+)
    private static readonly Random random = Random.Shared;

    private static (uint machinesCount, bool hasHazardousMaterials, int X, int Y) GenerateManufacturingWorkshopFields(uint formWidth, uint formHeight)
    {
        uint machinesCount = RandomHelper.GenerateRandomInRange(ManufacturingWorkshop.MIN_MACHINES_NUMBER, ManufacturingWorkshop.MAX_MACHINES_NUMBER, "Machines Count");
        bool hasHazardousMaterials = random.Next(2) == 0;

        int X = (int)RandomHelper.GenerateRandomInRange(0, formWidth, "X");
        int Y = (int)RandomHelper.GenerateRandomInRange(0, formHeight, "Y");

        return (machinesCount, hasHazardousMaterials, X, Y);
    }

    public static ManufacturingWorkshop Generate(uint formWidth, uint formHeight, MovementFunctionType movementFunctionType)
    {
        var workshopFields = WorkshopRandomizer.GenerateWorkshopFields();
        var manufacturingFields = GenerateManufacturingWorkshopFields(formWidth, formHeight);

        IMovementFunction movementFunction = new EmptyMovement();
        switch (movementFunctionType)
        {
            case MovementFunctionType.CycleMovement:
                movementFunction = CycleMovementRandomizer.Generate(formWidth, formHeight);
                break;
            case MovementFunctionType.RandomMovement:
                movementFunction = RandomMovementRandomizer.Generate();
                break;
            default:
                throw new ArgumentException("Unknown movement function type.");
        }

        return new ManufacturingWorkshop(
            workshopFields.productionName, workshopFields.manager, workshopFields.workerCount, 
            workshopFields.productList, workshopFields.workshopId, workshopFields.brigades, 
            workshopFields.shifts, workshopFields.schedule, manufacturingFields.machinesCount, 
            manufacturingFields.hasHazardousMaterials, manufacturingFields.X, manufacturingFields.Y, movementFunction
        );
    }

    // Generate a sequence of random workshops based on the given count
    public static IEnumerable<Workshop> GenerateMultiple(int count, uint formWidth, uint formHeight, MovementFunctionType movementFunctionType)
    {
        for (int i = 0; i < count; i++)
        {
            yield return Generate(formWidth, formHeight, movementFunctionType); // Lazily generates a random workshop
        }
    }
}