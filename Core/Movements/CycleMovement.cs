using CoreAPI.Core.Helpers;
using Lab2.CoreAPI.Core.Interfaces;

using System.ComponentModel.DataAnnotations;

namespace Lab2.CoreAPI.Core.Movements;

public enum MovementDirection
{
    Clockwise,
    CounterClockwise
}

public class CycleMovement : IMovementFunction
{
    private double _radius;

    [Range(0, double.MaxValue, ErrorMessage = "Radius must be greater than or equal to 0.")]
    public double Radius { 
        get => _radius;
        set => ValidatorHelper.SetValueWithValidation(this, ref _radius, nameof(Radius), value); // Validation and assignment
    } 
    public double Angle { get; set; }
    public MovementDirection Direction { get; set; }
    public double Speed { get; set; }

    public CycleMovement(double radius, double initialAngle, MovementDirection direction, double speed)
    {
        _radius = radius;

        ValidatorHelper.ValidateObject(this);

        // updating the angle, leaving it within [0, 2π]
        Angle = initialAngle % (2 * Math.PI);
        if (Angle < 0) Angle += 2 * Math.PI; // so that it doesn't go into negative values

        Direction = direction;
        Speed = speed;
    }

    public (int dx, int dy) Shift(double deltaTime)
    {
        // saving the old coordinates for the current angle
        double oldX = Radius * Math.Sin(Angle);
        double oldY = Radius * Math.Cos(Angle);

        // arc length traveled in dt
        double arcLength = Speed * deltaTime;

        // the angle corresponding to this arc (Δα = s / R)
        double deltaAngle = arcLength / Radius;

        // accounting for the direction (clockwise/counterclockwise)
        deltaAngle *= (Direction == MovementDirection.Clockwise) ? -1 : 1;

        // updating the angle, leaving it within [0, 2π]
        Angle = (Angle + deltaAngle) % (2 * Math.PI);
        if (Angle < 0) Angle += 2 * Math.PI; // so that it doesn't go into negative values

        // new coordinates
        double newX = Radius * Math.Sin(Angle);
        double newY = Radius * Math.Cos(Angle);

        // delta movement
        int dx = (int)(newX - oldX);
        int dy = (int)(newY - oldY);

        return (dx, dy);
    }
}