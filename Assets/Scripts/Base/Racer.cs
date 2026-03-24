// Encapsulation Racer owns its own data and movement

public class Racer
{
    public string Name { get; }
    public float Speed { get; }

    private float _distanceTravelled;

    public float DistanceTravelled => _distanceTravelled;

    public Racer(string name, float speed)
    {
        Name = name;
        Speed = speed;
    }

    public void Move(float deltaTime)
    {
        _distanceTravelled += Speed * deltaTime;
    }
}