[System.Serializable]
public class Ship
{
    public string shipName;
    public ShipType shipType;

    public Ship()
    {
    }

    public Ship(string ShipName, ShipType ShipType)
    {
        this.shipName = ShipName;
        this.shipType = ShipType;
    }
}

public enum ShipType
{
    BattleStar,
    Fighter
}