using UnityEngine;

// Inheritance CircularTrack is a type of RaceTrack

public class CircularTrack : RaceTrack
{
    private readonly float _radius;

    public CircularTrack(float radius)
    {
        _radius = radius;
    }

    public override float Length => 2f * Mathf.PI * _radius;

    public override Vector3 GetPosition(float distance)
    {
        float angle = distance / _radius;

        float x = Mathf.Cos(angle) * _radius;
        float z = Mathf.Sin(angle) * _radius;

        return new Vector3(x, 0f, z);
    }

    public override Vector3 GetForwardDirection(float distance)
    {
        float angle = distance / _radius;

        Vector3 tangent = new Vector3(-Mathf.Sin(angle), 0f, Mathf.Cos(angle));
        return tangent.normalized;
    }
}