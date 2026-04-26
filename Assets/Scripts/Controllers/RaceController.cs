using System.Collections.Generic;
using UnityEngine;

// Dependency Injection: builds objects and passes them into Race and BettingService

public class RaceController : MonoBehaviour
{
    [SerializeField] private GameObject carPrefab;
    [SerializeField] private LeaderboardUI leaderboardUI;
    [SerializeField] private BettingUI bettingUI;

    [SerializeField] private int racerCount = 5;
    [SerializeField] private int lapCount = 3;
    [SerializeField] private bool useUnderdogPayout = false;

    [SerializeField] private float circleRadius = 20f;
    [SerializeField] private float ovalRadiusX = 24f;
    [SerializeField] private float ovalRadiusZ = 16f;

    [SerializeField] private float minSpeed = 5f;
    [SerializeField] private float maxSpeed = 10f;
    [SerializeField] private float minConsistency = 0.2f;
    [SerializeField] private float maxConsistency = 1f;

    private Race _race;
    private RacerFactory _racerFactory;
    private BettingService _bettingService;
    private readonly List<GameObject> _spawnedCars = new List<GameObject>();

    private bool _raceEndLogged;

    private void Start()
    {
        StartNewRace();
    }

    private void Update()
    {
        if (_race == null || _race.IsFinished)
        {
            return;
        }

        _race.Update(Time.deltaTime);

        if (_race.IsFinished && !_raceEndLogged)
        {
            Debug.Log("Winner: " + _race.Winner.Name);
            _raceEndLogged = true;
        }
    }

    public void PlayAnotherRace()
    {
        StartNewRace();
    }

    private void StartNewRace()
    {
        ClearOldRace();

        _racerFactory = new RacerFactory();

        RandomTrackFactory trackFactory = new RandomTrackFactory(circleRadius, ovalRadiusX, ovalRadiusZ);
        RaceTrack track = trackFactory.CreateTrack();

        List<Racer> racers = CreateRacers();

        IRaceEventGenerator eventGenerator = new ConsistencyRaceEventGenerator();
        _race = new Race(track, racers, lapCount, eventGenerator);

        if (leaderboardUI != null)
        {
            leaderboardUI.Clear();
            _race.AddObserver(leaderboardUI);
        }

        IPayoutStrategy payoutStrategy = useUnderdogPayout
            ? new UnderdogPayoutStrategy()
            : new StandardPayoutStrategy();

        _bettingService = new BettingService(payoutStrategy, racers);
        _race.AddObserver(_bettingService);

        if (bettingUI != null)
        {
            bettingUI.Initialize(_bettingService, new BetFactory(), racers);
        }

        SpawnCars(track, racers);
        _raceEndLogged = false;
    }

    private List<Racer> CreateRacers()
    {
        List<Racer> racers = new List<Racer>();

        for (int i = 0; i < racerCount; i++)
        {
            float speed = Random.Range(minSpeed, maxSpeed);
            float consistency = Random.Range(minConsistency, maxConsistency);

            Racer racer = _racerFactory.CreateRacer("Racer " + (i + 1), speed, consistency);
            racers.Add(racer);
        }

        return racers;
    }

    private void SpawnCars(RaceTrack track, List<Racer> racers)
    {
        foreach (Racer racer in racers)
        {
            GameObject obj = Instantiate(carPrefab);
            obj.GetComponent<CarView>().Initialize(racer, track);
            _spawnedCars.Add(obj);
        }
    }

    private void ClearOldRace()
    {
        foreach (GameObject car in _spawnedCars)
        {
            if (car != null)
            {
                Destroy(car);
            }
        }

        _spawnedCars.Clear();

        if (leaderboardUI != null)
        {
            leaderboardUI.Clear();
        }

        if (bettingUI != null)
        {
            bettingUI.Clear();
        }

        _race = null;
        _bettingService = null;
    }
}