using UnityEngine;

// Separation of Concerns visual only, reads from model

public class CarView : MonoBehaviour
{
    private Racer _racer;
    private RaceTrack _track;

    public void Initialize(Racer racer, RaceTrack track)
    {
        _racer = racer;
        _track = track;
    }

    private void Update()
    {
        if (_racer == null || _track == null)
        {
            return;
        }

        transform.position = _track.GetPosition(_racer.DistanceTravelled);

        Vector3 dir = _track.GetForwardDirection(_racer.DistanceTravelled);
        if (dir.sqrMagnitude > 0.0001f)
        {
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}