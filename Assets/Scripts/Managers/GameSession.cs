using UnityEngine;

// Singleton Pattern: one shared game session stores player money

public class GameSession : MonoBehaviour
{
    public static GameSession Instance { get; private set; }

    public int PlayerMoney { get; private set; } = 100;
    public string LastWinnerName { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SpendMoney(int amount)
    {
        PlayerMoney -= amount;
    }

    public void AddMoney(int amount)
    {
        PlayerMoney += amount;
    }

    public void SetLastWinner(string winnerName)
    {
        LastWinnerName = winnerName;
    }
}