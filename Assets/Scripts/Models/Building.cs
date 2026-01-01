[System.Serializable]
public class Building
{
    public string name;
    public string abilityText;
    public YieldTypeEnum yieldType;
    public int yieldValue;

    public Building()
    {
    }

    public Building(string Name, string AbilityText, YieldTypeEnum YieldType, int YieldValue)
    {
        this.name = Name;
        this.abilityText = AbilityText;
        this.yieldType = YieldType;
        this.yieldValue = YieldValue;
    }
}