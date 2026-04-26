// Factory Pattern: creates valid Bet objects

public class BetFactory
{
    public Bet CreateBet(Racer pickedRacer, int amount)
    {
        if (pickedRacer == null)
        {
            throw new System.ArgumentException("Picked racer cannot be null");
        }

        if (amount <= 0)
        {
            throw new System.ArgumentException("Bet amount must be positive");
        }

        return new Bet(pickedRacer, amount);
    }
}