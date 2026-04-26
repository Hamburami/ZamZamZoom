using System.Collections.Generic;

// Strategy Pattern: payout rules can change without changing BettingService

public interface IPayoutStrategy
{
    int CalculatePayout(Bet bet, Racer winner, IReadOnlyList<Racer> racers);
}