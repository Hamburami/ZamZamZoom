// Strategy Pattern: race event rules can be swapped

public interface IRaceEventGenerator
{
    void TryApplyEvent(Racer racer, float deltaTime);
}