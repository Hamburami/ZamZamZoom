using System.Collections.Generic;

// Strategy Pattern: normal winning bets pay double

public class StandardPayoutStrategy : IPayoutStrategy
{
    public int CalculatePayout(Bet bet, Racer winner, IReadOnlyList<Racer> racers)
    {
        if (bet.PickedRacer != winner)
        {
            return 0;
        }

        return bet.Amount * 2;
    }
}