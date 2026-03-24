using System.Collections.Generic;
using UnityEngine;

// Dependency Injection builds objects and passes them into Race

public class RaceController : MonoBehaviour
{
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private LeaderboardUI leaderboardUI;

    [SerializeField] private int racerCount = 5;
    [SerializeField] private int lapCount = 3;
    [SerializeField] private bool useOvalTrack = false;

    [SerializeField] private float circleRadius = 20f;
    [SerializeField] private float ovalRadiusX = 24f;
    [SerializeField] private float ovalRadiusZ = 16f;

    [SerializeField] private float minSpeed = 5f;
    [SerializeField] private float maxSpeed = 10f;

    private Race _race;
    private RacerFactory _factory;

    private void Start()
    {
        _factory = new RacerFactory();

        RaceTrack track = useOvalTrack
            ? new OvalTrack(ovalRadiusX, ovalRadiusZ)
            : new CircularTrack(circleRadius);

        List<Racer> racers = new List<Racer>();

        for (int i = 0; i < racerCount; i++)
        {
            float speed = Random.Range(minSpeed, maxSpeed);
            racers.Add(_factory.CreateRacer($"Racer {i + 1}", speed));
        }

        _race = new Race(track, racers, lapCount);

        if (leaderboardUI != null)
        {
            _race.AddObserver(leaderboardUI);
        }

        foreach (Racer r in racers)
        {
            GameObject obj = Instantiate(carPrefab);
            obj.GetComponent<CarView>().Initialize(r, track);
        }
    }

    private void Update()
    {
        if (_race == null) return;

        _race.Update(Time.deltaTime);

        if (_race.IsFinished)
        {
            Debug.Log("Winner: " + _race.Winner.Name);
            enabled = false;
        }
    }
}