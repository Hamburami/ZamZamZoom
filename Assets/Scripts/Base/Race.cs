using System.Collections.Generic;
using System.Linq;

// Single Responsibility Race controls race flow and winner logic

public class Race
{
    private readonly RaceTrack _track;
    private readonly List<Racer> _racers;
    private readonly List<IRaceObserver> _observers;

    private readonly float _targetDistance;

    public bool IsFinished { get; private set; }
    public Racer Winner { get; private set; }

    public Race(RaceTrack track, List<Racer> racers, int lapCount)
    {
        _track = track;
        _racers = racers;
        _observers = new List<IRaceObserver>();
        _targetDistance = track.Length * lapCount;
    }

    public RaceTrack Track => _track;
    public IReadOnlyList<Racer> Racers => _racers;

    public void AddObserver(IRaceObserver observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
    }

    public void Update(float deltaTime)
    {
        if (IsFinished)
        {
            return;
        }

        foreach (Racer racer in _racers)
        {
            racer.Move(deltaTime);

            if (racer.DistanceTravelled >= _targetDistance)
            {
                IsFinished = true;
                Winner = racer;
                NotifyRaceUpdated();
                NotifyRaceFinished();
                return;
            }
        }

        NotifyRaceUpdated();
    }

    public List<Racer> GetStandings()
    {
        return _racers
            .OrderByDescending(r => r.DistanceTravelled)
            .ToList();
    }

    private void NotifyRaceUpdated()
    {
        var standings = GetStandings();

        foreach (IRaceObserver observer in _observers)
        {
            observer.OnRaceUpdated(standings);
        }
    }

    private void NotifyRaceFinished()
    {
        foreach (IRaceObserver observer in _observers)
        {
            observer.OnRaceFinished(Winner);
        }
    }
}