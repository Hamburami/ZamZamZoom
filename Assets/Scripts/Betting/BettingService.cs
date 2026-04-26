using System.Collections.Generic;

// Facade Pattern: UI uses one simple class for betting

public class BettingService : IRaceObserver
{
    private readonly IPayoutStrategy _payoutStrategy;
    private readonly IReadOnlyList<Racer> _racers;

    private Bet _currentBet;
    private BetResult _lastResult;

    public Bet CurrentBet => _currentBet;
    public BetResult LastResult => _lastResult;

    public BettingService(IPayoutStrategy payoutStrategy, IReadOnlyList<Racer> racers)
    {
        _payoutStrategy = payoutStrategy;
        _racers = racers;
    }

    public void PlaceBet(Bet bet)
    {
        if (_currentBet != null)
        {
            throw new System.InvalidOperationException("Bet already placed");
        }

        if (GameSession.Instance != null)
        {
            GameSession.Instance.SpendMoney(bet.Amount);
        }

        _currentBet = bet;
    }

    public void OnRaceUpdated(List<Racer> standings)
    {
    }

    public void OnRaceFinished(Racer winner)
    {
        if (_currentBet == null)
        {
            return;
        }

        int payout = _payoutStrategy.CalculatePayout(_currentBet, winner, _racers);
        _lastResult = new BetResult(_currentBet, winner, payout);

        if (GameSession.Instance != null)
        {
            GameSession.Instance.AddMoney(payout);
            GameSession.Instance.SetLastWinner(winner.Name);
        }
    }
}