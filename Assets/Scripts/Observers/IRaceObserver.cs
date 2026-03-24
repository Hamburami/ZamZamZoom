using System.Collections.Generic;

// Observer Pattern allows listeners to react to race changes

public interface IRaceObserver
{
    void OnRaceUpdated(List<Racer> standings);
    void OnRaceFinished(Racer winner);
}