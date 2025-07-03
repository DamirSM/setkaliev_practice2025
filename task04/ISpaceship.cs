namespace task04;

public interface ISpaceship
{
    void MoveForward();
    void Rotate(int angle);
    void Fire();
    int Speed { get; }
    int FirePower { get; }
}

public class Cruiser: ISpaceship
{
    public void MoveForward()
    {
        XCoordinate += Math.Round(Math.Cos(Angle * (Math.PI) / 180) * Speed, 2);
        YCoordinate += Math.Round(Math.Sin(Angle * (Math.PI) / 180) * Speed, 2);
    }
    
    public void Rotate(int angle)
    {
        Angle = angle % 360;
    }

    public void Fire()
    {
        Ammo -= 1;
    }

    public int Speed { get; }
    public int FirePower { get; }
    public double XCoordinate { get; set; }
    public double YCoordinate { get; set; }
    public int Angle { get; set; }
    public int Ammo { get; set; }

    public Cruiser()
    {
        Speed = 50;
        FirePower = 100;
        XCoordinate = 0;
        YCoordinate = 0;
        Angle = 0;
        Ammo = 10;
    }
}

public class Fighter: ISpaceship
{
    public void MoveForward()
    {
        XCoordinate += Math.Cos(Angle * (Math.PI) / 180) * Speed;
        YCoordinate += Math.Sin(Angle * (Math.PI) / 180) * Speed;
    }

    public void Rotate(int angle)
    {
        Angle = angle % 360;
    }

    public void Fire()
    {
        Ammo -= 1;
    }

    public int Speed { get; }
    public int FirePower { get; }
    public double XCoordinate { get; set; }
    public double YCoordinate { get; set; }
    public int Angle { get; set; }
    public int Ammo { get; set; }

    public Fighter()
    {
        Speed = 100;
        FirePower = 50;
        XCoordinate = 0;
        YCoordinate = 0;
        Angle = 0;
        Ammo = 10;
    }
}
