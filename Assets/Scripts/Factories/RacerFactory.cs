// Factory Pattern creates Racer objects in one place

public class RacerFactory
{
    public Racer CreateRacer(string name, float speed)
    {
        return new Racer(name, speed);
    }
}