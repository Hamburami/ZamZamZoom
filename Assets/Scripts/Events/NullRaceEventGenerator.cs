// Null Object Pattern: no race events happen

public class NullRaceEventGenerator : IRaceEventGenerator
{
    public void TryApplyEvent(Racer racer, float deltaTime)
    {
    }
}