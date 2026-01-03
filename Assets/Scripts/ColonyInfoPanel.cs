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
        if (colony.finishedProductions != null)
        {
            foreach(Production building in colony.finishedProductions)
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

    public static void ApplyBuildingYieldsToCity(Production production, Colony colony)
    {
        switch (production.yieldType)
        {
            case YieldTypeEnum.Credits:
                colony.income += production.yieldValue;
                break;

            case YieldTypeEnum.Production:
                colony.production = production.yieldValue; 
                break;

            case YieldTypeEnum.Science:
                colony.science += production.yieldValue;
                break;

            default:
                //Do nothing for now
                break;
        }
    }

}
