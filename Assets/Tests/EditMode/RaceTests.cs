using System.Collections.Generic;
using NUnit.Framework;

public class RaceTests
{
    [Test]
    public void Update_FinishesRace_WhenRacerReachesTargetDistance()
    {
        RaceTrack track = new CircularTrack(10f);
        Racer racer = new Racer("Fast", 100f);
        List<Racer> racers = new List<Racer> { racer };

        Race race = new Race(track, racers, 1);

        race.Update(1f);

        Assert.IsTrue(race.IsFinished);
        Assert.AreEqual(racer, race.Winner);
    }

    [Test]
    public void GetStandings_ReturnsRacersSortedByDistance()
    {
        Racer racer1 = new Racer("Slow", 5f);
        Racer racer2 = new Racer("Fast", 10f);

        RaceTrack track = new CircularTrack(10f);
        Race race = new Race(track, new List<Racer> { racer1, racer2 }, 5);

        race.Update(1f);

        List<Racer> standings = race.GetStandings();

        Assert.AreEqual("Fast", standings[0].Name);
        Assert.AreEqual("Slow", standings[1].Name);
    }

    [Test]
    public void Update_DoesNotChangeWinner_AfterRaceFinishes()
    {
        RaceTrack track = new CircularTrack(10f);
        Racer racer1 = new Racer("Fast", 100f);
        Racer racer2 = new Racer("Slow", 1f);

        Race race = new Race(track, new List<Racer> { racer1, racer2 }, 1);

        race.Update(1f);
        Racer firstWinner = race.Winner;

        race.Update(1f);

        Assert.AreEqual(firstWinner, race.Winner);
    }
}