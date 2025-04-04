using CoreAPI.Core.Models;
using CoreAPI.Core.Helpers;
using Lab2.CoreAPI.Core.Movements;
using Lab2.CoreAPI.Core.Models;
using Lab2.CoreAPI.Core.Interfaces;

namespace Lab2.CoreAPI.Core.Randomizers;

public static class AssemblyWorkshopRandomizer
{
    // Use Random.Shared for thread-safe random number generation (available from .NET 6+)
    private static readonly Random random = Random.Shared;

    private static (uint assemblyLines, bool usesAutomation, int X, int Y) GenerateAssemblyWorkshopFields(uint formWidth, uint formHeight)
    {
        uint assemblyLines = RandomHelper.GenerateRandomInRange(AssemblyWorkshop.MIN_LINES_NUMBER, AssemblyWorkshop.MAX_LINES_NUMBER, "Assembly Lines");
        bool usesAutomation = random.Next(2) == 0;

        int X = (int)RandomHelper.GenerateRandomInRange(0, formWidth, "X");
        int Y = (int)RandomHelper.GenerateRandomInRange(0, formHeight, "Y");

        return (assemblyLines, usesAutomation, X, Y);
    }

    public static AssemblyWorkshop Generate(uint formWidth, uint formHeight, MovementFunctionType movementFunctionType)
    {
        var workshopFields = WorkshopRandomizer.GenerateWorkshopFields();
        var assemblyFields = GenerateAssemblyWorkshopFields(formWidth, formHeight);

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

        return new AssemblyWorkshop(
            workshopFields.productionName, workshopFields.manager, workshopFields.workerCount, 
            workshopFields.productList, workshopFields.workshopId, workshopFields.brigades, 
            workshopFields.shifts, workshopFields.schedule, assemblyFields.assemblyLines, 
            assemblyFields.usesAutomation, assemblyFields.X, assemblyFields.Y, movementFunction
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