using System.Collections.Generic;
using System.Linq;

// Strategy Pattern: slower racers pay more if they win

public class UnderdogPayoutStrategy : IPayoutStrategy
{
    public int CalculatePayout(Bet bet, Racer winner, IReadOnlyList<Racer> racers)
    {
        if (bet.PickedRacer != winner)
        {
            return 0;
        }

        float averageSpeed = racers.Average(r => r.Speed);
        float multiplier = averageSpeed / bet.PickedRacer.Speed;

        if (multiplier < 1f)
        {
            multiplier = 1f;
        }

        return (int)(bet.Amount * 2 * multiplier);
    }
}