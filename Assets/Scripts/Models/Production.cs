[System.Serializable]
public class Production
{
    public int id;
    public string productionName;
    public string turns;
    public ProductionTypeEnum productionType;

    public Production()
    {
    }

    public Production(int Id, string ProductionName, string Turns, ProductionTypeEnum ProductionType)
    {
        this.productionName = ProductionName;
        this.turns = Turns;
        this.productionType = ProductionType;
        this.id = Id;
    }
}