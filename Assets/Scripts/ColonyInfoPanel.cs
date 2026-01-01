using UnityEngine;
using TMPro;

public class ColonyInfoPanel : MonoBehaviour
{
    private Colony colony;

    [Header("Value Texts")]
    public TextMeshProUGUI populationValue;
    public TextMeshProUGUI incomeValue;
    public TextMeshProUGUI expensesValue;
    public TextMeshProUGUI productionValue;
    public TextMeshProUGUI scienceValue;

    void Start()
    {
        colony = XmlManager.Load();
        UpdateText();
    }

    public void UpdateText()
    {
        if (colony.buildings != null)
        {
            foreach(Building building in colony.buildings)
            {
                ApplyBuildingYieldsToCity(building, this.colony);
            }

            populationValue.text = colony.population.ToString();
            incomeValue.text = colony.income.ToString();
            expensesValue.text = colony.expenses.ToString();
            productionValue.text = colony.production.ToString();
            scienceValue.text = colony.science.ToString();
        }
    }

    public static void ApplyBuildingYieldsToCity(Building building, Colony colony)
    {
        switch (building.yieldType)
        {
            case YieldTypeEnum.Credits:
                colony.income += building.yieldValue;
                break;

            case YieldTypeEnum.Production:
                colony.production = building.yieldValue; 
                break;

            case YieldTypeEnum.Science:
                colony.science += building.yieldValue;
                break;

            default:
                //Do nothing for now
                break;
        }
    }

}
