using UnityEngine;

// Polymorphism used the same as CircularTrack but different behavior

public class OvalTrack : RaceTrack
{
    private readonly float _radiusX;
    private readonly float _radiusZ;
    private readonly float _length;

    public OvalTrack(float radiusX, float radiusZ)
    {
        _radiusX = radiusX;
        _radiusZ = radiusZ;

        float a = radiusX;
        float b = radiusZ;

        _length = Mathf.PI * (3f * (a + b) - Mathf.Sqrt((3f * a + b) * (a + 3f * b)));
    }

    public override float Length => _length;

    public override Vector3 GetPosition(float distance)
    {
        float t = (distance / _length) * 2f * Mathf.PI;

        float x = Mathf.Cos(t) * _radiusX;
        float z = Mathf.Sin(t) * _radiusZ;

        return new Vector3(x, 0f, z);
    }

    public override Vector3 GetForwardDirection(float distance)
    {
        float t = (distance / _length) * 2f * Mathf.PI;

        Vector3 tangent = new Vector3(-Mathf.Sin(t) * _radiusX, 0f, Mathf.Cos(t) * _radiusZ);
        return tangent.normalized;
    }
}