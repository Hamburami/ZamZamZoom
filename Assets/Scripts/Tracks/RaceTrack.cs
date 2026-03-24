using UnityEngine;

// Abstraction Race uses base track type without knowing shape

public abstract class RaceTrack
{
    public abstract float Length { get; }

    public abstract Vector3 GetPosition(float distance);
    public abstract Vector3 GetForwardDirection(float distance);
}