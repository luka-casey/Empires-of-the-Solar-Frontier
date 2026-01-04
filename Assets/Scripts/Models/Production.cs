[System.Serializable]
public class Production
{
    public int id;
    public string productionName;
    public string turns;
    public ProductionTypeEnum productionType;
    public string abilityText;
    public YieldTypeEnum yieldType;
    public int yieldValue;
    public string? imageName;

    public Production()
    {
    }

    public Production(int Id, string ProductionName, string Turns, ProductionTypeEnum ProductionType, string AbilityText, YieldTypeEnum YieldType, int YieldValue, string? ImageName)
    {
        this.productionName = ProductionName;
        this.turns = Turns;
        this.productionType = ProductionType;
        this.id = Id;
        this.abilityText = AbilityText;
        this.yieldType = YieldType;
        this.yieldValue = YieldValue;
        this.imageName = ImageName;
    }
}