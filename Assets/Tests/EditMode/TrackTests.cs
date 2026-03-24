using NUnit.Framework;
using UnityEngine;

public class TrackTests
{
    [Test]
    public void CircularTrack_Length_IsCorrect()
    {
        CircularTrack track = new CircularTrack(10f);

        float expected = 2f * Mathf.PI * 10f;

        Assert.AreEqual(expected, track.Length, 0.001f);
    }

    [Test]
    public void CircularTrack_GetPosition_ReturnsPointOnTrack()
    {
        CircularTrack track = new CircularTrack(10f);

        Vector3 position = track.GetPosition(0f);

        Assert.AreEqual(10f, position.x, 0.001f);
        Assert.AreEqual(0f, position.z, 0.001f);
    }
}