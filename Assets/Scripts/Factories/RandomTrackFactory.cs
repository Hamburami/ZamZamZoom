using UnityEngine;

// Factory Pattern: creates a random RaceTrack

public class RandomTrackFactory
{
    private readonly float _circleRadius;
    private readonly float _ovalRadiusX;
    private readonly float _ovalRadiusZ;

    public RandomTrackFactory(float circleRadius, float ovalRadiusX, float ovalRadiusZ)
    {
        _circleRadius = circleRadius;
        _ovalRadiusX = ovalRadiusX;
        _ovalRadiusZ = ovalRadiusZ;
    }

    public RaceTrack CreateTrack()
    {
        if (Random.value < 0.5f)
        {
            return new CircularTrack(_circleRadius);
        }

        return new OvalTrack(_ovalRadiusX, _ovalRadiusZ);
    }
}