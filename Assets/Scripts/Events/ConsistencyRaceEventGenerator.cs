using UnityEngine;

// Strategy Pattern: consistency changes random event chances

public class ConsistencyRaceEventGenerator : IRaceEventGenerator
{
    private const float BaseEventChancePerSecond = 0.08f;
    private const float GoodEventMultiplier = 1.25f;
    private const float BadEventMultiplier = 0.75f;
    private const float EventDuration = 1.5f;

    public void TryApplyEvent(Racer racer, float deltaTime)
    {
        float eventChance = BaseEventChancePerSecond * deltaTime;

        if (Random.value > eventChance)
        {
            return;
        }

        float goodChance = racer.Consistency;

        if (Random.value <= goodChance)
        {
            racer.ApplySpeedEvent(GoodEventMultiplier, EventDuration);
        }
        else
        {
            racer.ApplySpeedEvent(BadEventMultiplier, EventDuration);
        }
    }
}