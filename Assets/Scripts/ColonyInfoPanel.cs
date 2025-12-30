using UnityEngine;
using TMPro;

public class ColonyInfoPanel : MonoBehaviour
{
    private Colony colony = new Colony();

    [Header("Value Texts")]
    public TextMeshProUGUI populationValue;
    public TextMeshProUGUI incomeValue;
    public TextMeshProUGUI expensesValue;
    public TextMeshProUGUI productionValue;
    public TextMeshProUGUI scienceValue;

    void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        foreach(Building building in colony.buildings)
        {
            ApplyBuildingYieldsToCity(building.yieldType, building.yieldValue, this.colony);
        }

        populationValue.text = colony.population.ToString();
        incomeValue.text = colony.income.ToString();
        expensesValue.text = colony.expenses.ToString();
        productionValue.text = colony.production.ToString();
        scienceValue.text = colony.science.ToString();
    }

    public static void ApplyBuildingYieldsToCity(YieldTypeEnum yieldType, int yieldValue, Colony colony)
    {
        switch (yieldType)
        {
            case YieldTypeEnum.Credits:
                colony.income += yieldValue;
                break;

            case YieldTypeEnum.Production:
                colony.production += yieldValue;
                break;

            case YieldTypeEnum.Science:
                colony.science += yieldValue;
                break;

            default:
                //Do nothing for now
                break;
        }
    }

}
