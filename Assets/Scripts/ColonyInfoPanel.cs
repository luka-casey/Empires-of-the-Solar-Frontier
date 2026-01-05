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
            colony.incomeTotal = 0;
            colony.productionTotal = 0;
            colony.scienceTotal = 0;
            colony.populationTotal = 0;

            foreach(Production production in colony.finishedProductions)
            {
                if (production is not null)
                {
                    ApplyBuildingYieldsToCity(production, this.colony);
                }
            }

            populationValue.text = (colony.populationBaseValue + colony.populationTotal).ToString();
            incomeValue.text = (colony.incomeBaseValue + colony.incomeTotal).ToString();
            expensesValue.text = (colony.expensesBaseValue + colony.expensesTotal).ToString();
            productionValue.text = (colony.productionBaseValue + colony.productionTotal).ToString();
            scienceValue.text = (colony.scienceBaseValue + colony.scienceTotal).ToString();

            XmlManager.Save(colony);
        }
    }

    public static void ApplyBuildingYieldsToCity(Production production, Colony colony)
    {
        switch (production.yieldType)
        {
            case YieldTypeEnum.Credits:
                colony.incomeTotal += production.yieldValue;
                break;

            case YieldTypeEnum.Production:
                colony.productionTotal = production.yieldValue; 
                break;

            case YieldTypeEnum.Science:
                colony.scienceTotal += production.yieldValue;
                break;

            case YieldTypeEnum.Population:
            case YieldTypeEnum.Food:
                colony.populationTotal += production.yieldValue;
                break;

            default:
                //Do nothing for now
                break;
        }
    }
}
