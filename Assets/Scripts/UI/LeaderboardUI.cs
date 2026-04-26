using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

// Observer Pattern: UI updates from Race notifications

public class LeaderboardUI : MonoBehaviour, IRaceObserver
{
    [SerializeField] private TMP_Text standingsText;
    [SerializeField] private TMP_Text winnerText;

    public void OnRaceUpdated(List<Racer> standings)
    {
        if (standingsText == null) return;

        StringBuilder sb = new StringBuilder();
        sb.AppendLine("Standings");

        for (int i = 0; i < standings.Count; i++)
        {
            sb.AppendLine($"{i + 1}. {standings[i].Name} {standings[i].DistanceTravelled:F1}");
        }

        standingsText.text = sb.ToString();
    }

    public void OnRaceFinished(Racer winner)
    {
        if (winnerText == null) return;

        winnerText.text = $"Winner: {winner.Name}";
    }

    public void Clear()
    {
        if (standingsText != null)
        {
            standingsText.text = "Standings";
        }

        if (winnerText != null)
        {
            winnerText.text = "";
        }
    }
}