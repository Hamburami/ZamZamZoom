using System.Collections.Generic;
using NUnit.Framework;

public class BettingTests
{
    [Test]
    public void BetFactory_CreateBet_WithValidInput_ReturnsBet()
    {
        Racer racer = new Racer("Racer 1", 10f);
        BetFactory factory = new BetFactory();

        Bet bet = factory.CreateBet(racer, 25);

        Assert.AreEqual(racer, bet.PickedRacer);
        Assert.AreEqual(25, bet.Amount);
    }

    [Test]
    public void BetFactory_CreateBet_WithZeroAmount_ThrowsException()
    {
        Racer racer = new Racer("Racer 1", 10f);
        BetFactory factory = new BetFactory();

        Assert.Throws<System.ArgumentException>(() =>
        {
            factory.CreateBet(racer, 0);
        });
    }

    [Test]
    public void StandardPayoutStrategy_WinningBet_ReturnsDoubleAmount()
    {
        Racer racer = new Racer("Racer 1", 10f);
        Bet bet = new Bet(racer, 20);

        IPayoutStrategy strategy = new StandardPayoutStrategy();

        int payout = strategy.CalculatePayout(bet, racer, new List<Racer> { racer });

        Assert.AreEqual(40, payout);
    }

    [Test]
    public void StandardPayoutStrategy_LosingBet_ReturnsZero()
    {
        Racer pickedRacer = new Racer("Picked", 10f);
        Racer winner = new Racer("Winner", 12f);
        Bet bet = new Bet(pickedRacer, 20);

        IPayoutStrategy strategy = new StandardPayoutStrategy();

        int payout = strategy.CalculatePayout(bet, winner, new List<Racer> { pickedRacer, winner });

        Assert.AreEqual(0, payout);
    }

    [Test]
    public void UnderdogPayoutStrategy_SlowerWinningRacer_ReturnsMoreThanStandardPayout()
    {
        Racer underdog = new Racer("Underdog", 5f);
        Racer favorite = new Racer("Favorite", 15f);
        Bet bet = new Bet(underdog, 20);

        IPayoutStrategy strategy = new UnderdogPayoutStrategy();

        int payout = strategy.CalculatePayout(
            bet,
            underdog,
            new List<Racer> { underdog, favorite }
        );

        Assert.Greater(payout, 40);
    }

    [Test]
    public void BettingService_OnRaceFinished_StoresLastResult()
    {
        Racer racer = new Racer("Racer 1", 10f);
        Bet bet = new Bet(racer, 20);

        IPayoutStrategy strategy = new StandardPayoutStrategy();
        BettingService service = new BettingService(strategy, new List<Racer> { racer });

        service.PlaceBet(bet);
        service.OnRaceFinished(racer);

        Assert.IsNotNull(service.LastResult);
        Assert.IsTrue(service.LastResult.Won);
        Assert.AreEqual(40, service.LastResult.Payout);
    }
}