using Lab2.CoreAPI.Core.Movements;

namespace Lab2.CoreAPI.Core.Randomizers;

public static class RandomMovementRandomizer
{
    // Use Random.Shared for thread-safe random number generation (available from .NET 6+)
    private static readonly Random random = Random.Shared;
    private static double GenerateRandomSpeed(double minSpeed, double maxSpeed)
    {
        return random.NextDouble() * (maxSpeed - minSpeed) + minSpeed;
    }

    public static RandomMovement Generate()
    {
        double speed = GenerateRandomSpeed(30, 120);
        return new RandomMovement(speed);
    }
    public static IEnumerable<RandomMovement> GenerateMultiple(int count)
    {
        for (int i = 0; i < count; i++)
        {
            yield return Generate();
        }
    }
}