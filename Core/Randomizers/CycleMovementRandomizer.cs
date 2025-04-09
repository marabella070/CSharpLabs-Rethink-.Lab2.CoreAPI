using Lab2.CoreAPI.Core.Movements;

namespace Lab2.CoreAPI.Core.Randomizers;

public static class CycleMovementRandomizer
{
    // Use Random.Shared for thread-safe random number generation (available from .NET 6+)
    private static readonly Random random = Random.Shared;

    private static double GenerateRandomRadius((uint formWidth, uint formHeight) formSize)
    {
        uint maxRadius = (uint)Math.Min(formSize.formWidth, formSize.formHeight) / 2;
        return random.NextDouble() * maxRadius; // Generating a random radius value from 0 to maxRadius
    }
    private static double GenerateRandomAngle()
    {
        return random.NextDouble() * 2 * Math.PI; // Generating an angle in the range [0, 2π]
    }
    private static MovementDirection GenerateRandomDirection()
    {
        return (MovementDirection)random.Next(0, 2); // 0 - Clockwise, 1 - CounterClockwise
    }
    private static double GenerateRandomSpeed()
    {
        return random.NextDouble() * 200;
    }

    public static CycleMovement Generate((uint formWidth, uint formHeight) formSize)
    {
        double radius = GenerateRandomRadius(formSize);
        double angle = GenerateRandomAngle();
        MovementDirection direction = GenerateRandomDirection();
        double speed = GenerateRandomSpeed();

        return new CycleMovement(radius, angle, direction, speed);
    }

    public static IEnumerable<CycleMovement> GenerateMultiple(int count, (uint formWidth, uint formHeight) formSize)
    {
        for (int i = 0; i < count; i++)
        {
            yield return Generate(formSize);
        }
    }
}