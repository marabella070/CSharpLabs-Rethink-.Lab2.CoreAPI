using CoreAPI.Core.Interfaces;

namespace Lab2.CoreAPI.Core.Movements;

public class RandomMovement : IMovementFunction
{
    // Use Random.Shared for thread-safe random number generation (available from .NET 6+)
    private static readonly Random random = Random.Shared;
    public double Speed { get; set; }

    public RandomMovement(double speed)
    {
        Speed = speed;
    }

    public (int dx, int dy) Shift(double deltaTime)
    {
        // A new random direction every time
        double directionAngle = random.NextDouble() * 2 * Math.PI;

        // updating the angle, leaving it within [0, 2π]
        directionAngle %= (2 * Math.PI);
        if (directionAngle < 0) directionAngle += 2 * Math.PI; // so that it doesn't go into negative values

        double distance = Speed * deltaTime;
        int dx = (int)(Math.Cos(directionAngle) * distance);
        int dy = (int)(Math.Sin(directionAngle) * distance);

        return (dx, dy);
    }
}
