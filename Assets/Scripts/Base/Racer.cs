// Encapsulation: Racer owns its own data and race state

public class Racer
{
    public string Name { get; }
    public float Speed { get; }
    public float Consistency { get; }

    private float _distanceTravelled;
    private float _speedMultiplier = 1f;
    private float _eventTimeLeft = 0f;

    public float DistanceTravelled => _distanceTravelled;
    public float CurrentSpeed => Speed * _speedMultiplier;

    public Racer(string name, float speed) : this(name, speed, 0.5f)
    {
    }

    public Racer(string name, float speed, float consistency)
    {
        Name = name;
        Speed = speed;
        Consistency = UnityEngine.Mathf.Clamp01(consistency);
    }

    public void Move(float deltaTime)
    {
        UpdateEventTimer(deltaTime);
        _distanceTravelled += CurrentSpeed * deltaTime;
    }

    public void ApplySpeedEvent(float multiplier, float duration)
    {
        _speedMultiplier = multiplier;
        _eventTimeLeft = duration;
    }

    public void ResetForNewRace()
    {
        _distanceTravelled = 0f;
        _speedMultiplier = 1f;
        _eventTimeLeft = 0f;
    }

    private void UpdateEventTimer(float deltaTime)
    {
        if (_eventTimeLeft <= 0f)
        {
            return;
        }

        _eventTimeLeft -= deltaTime;

        if (_eventTimeLeft <= 0f)
        {
            _speedMultiplier = 1f;
        }
    }
}