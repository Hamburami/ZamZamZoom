using NUnit.Framework;

public class RacerTests
{
    [Test]
    public void Move_IncreasesDistanceTravelled()
    {
        Racer racer = new Racer("Test Racer", 10f);

        racer.Move(2f);

        Assert.AreEqual(20f, racer.DistanceTravelled, 0.001f);
    }
}