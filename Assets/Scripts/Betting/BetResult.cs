// Encapsulation: BetResult owns the outcome of a bet

public class BetResult
{
    public Bet Bet { get; }
    public Racer Winner { get; }
    public bool Won { get; }
    public int Payout { get; }

    public BetResult(Bet bet, Racer winner, int payout)
    {
        Bet = bet;
        Winner = winner;
        Payout = payout;
        Won = bet.PickedRacer == winner;
    }
}