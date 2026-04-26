using System.Collections.Generic;
using TMPro;
using UnityEngine;

// Facade Pattern: UI talks to BettingService instead of betting internals

public class BettingUI : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown racerDropdown;
    [SerializeField] private TMP_InputField amountInput;
    [SerializeField] private TMP_Text betStatusText;
    [SerializeField] private TMP_Text moneyText;

    private BettingService _bettingService;
    private BetFactory _betFactory;
    private List<Racer> _racers;

    public void Clear()
    {
        _bettingService = null;
        _betFactory = null;
        _racers = null;

        if (betStatusText != null)
        {
            betStatusText.text = "Pick a racer and place a bet";
        }

        if (amountInput != null)
        {
            amountInput.text = "";
        }

        UpdateMoneyText();
    }
    public void Initialize(BettingService bettingService, BetFactory betFactory, List<Racer> racers)
    {
        _bettingService = bettingService;
        _betFactory = betFactory;
        _racers = racers;

        racerDropdown.ClearOptions();

        List<string> names = new List<string>();

        foreach (Racer racer in racers)
        {
            names.Add(racer.Name);
        }

        racerDropdown.AddOptions(names);
        betStatusText.text = "Pick a racer and place a bet";

        UpdateMoneyText();
    }

    public void PlaceBetFromUI()
    {
        if (_bettingService == null || _racers == null)
        {
            return;
        }

        int amount;

        if (!int.TryParse(amountInput.text, out amount))
        {
            betStatusText.text = "Enter a valid amount";
            return;
        }

        Racer pickedRacer = _racers[racerDropdown.value];

        try
        {
            Bet bet = _betFactory.CreateBet(pickedRacer, amount);
            _bettingService.PlaceBet(bet);
            betStatusText.text = "Bet placed on " + pickedRacer.Name;
            UpdateMoneyText();
        }
        catch (System.Exception error)
        {
            betStatusText.text = error.Message;
        }
    }

    private void Update()
    {
        if (_bettingService == null || _bettingService.LastResult == null)
        {
            return;
        }

        BetResult result = _bettingService.LastResult;

        if (result.Won)
        {
            betStatusText.text = "Bet won. Payout " + result.Payout;
        }
        else
        {
            betStatusText.text = "Bet lost. Winner was " + result.Winner.Name;
        }

        UpdateMoneyText();
    }

    private void UpdateMoneyText()
    {
        if (moneyText == null || GameSession.Instance == null)
        {
            return;
        }

        moneyText.text = "Money: " + GameSession.Instance.PlayerMoney;
    }
}