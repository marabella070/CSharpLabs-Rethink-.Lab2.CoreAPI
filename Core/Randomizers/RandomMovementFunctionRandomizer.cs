using CoreAPI.Core.Interfaces;
using CoreAPI.Core.Movements;

namespace Lab2.CoreAPI.Core.Randomizers;

public static class RandomMovementFunctionRandomizer
{
    // Use Random.Shared for thread-safe random number generation (available from .NET 6+)
    private static readonly Random random = Random.Shared;

    public static IMovementFunction Generate((uint formWidth, uint formHeight) formSize)
    {
        MovementFunctionType movementFunctionType = GetRandomMovementFunctionType();

        IMovementFunction movementFunction = new EmptyMovement();
        switch (movementFunctionType)
        {
            case MovementFunctionType.CycleMovement:
                movementFunction = CycleMovementRandomizer.Generate(formSize);
                break;
            case MovementFunctionType.RandomMovement:
                movementFunction = RandomMovementRandomizer.Generate();
                break;
        }
        return movementFunction;
    }

    private static MovementFunctionType GetRandomMovementFunctionType()
    {
        // Creating an array of types from which we will randomly select (excluding Empty Movement)
        Array values = Enum.GetValues(typeof(MovementFunctionType));
        var validMovementTypes = values.Cast<MovementFunctionType>()
                                      .Where(x => x != MovementFunctionType.EmptyMovement)
                                      .ToArray();

        // Selecting a random value from the array
        return validMovementTypes[random.Next(validMovementTypes.Length)];
    }
}
