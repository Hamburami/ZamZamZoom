// Encapsulation: Bet owns the picked racer and amount

public class Bet
{
    public Racer PickedRacer { get; }
    public int Amount { get; }

    public Bet(Racer pickedRacer, int amount)
    {
        PickedRacer = pickedRacer;
        Amount = amount;
    }
}