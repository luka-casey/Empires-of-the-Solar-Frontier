using UnityEngine;
using TMPro;

public class ColonyHoverUI : MonoBehaviour
{
    public Canvas tooltipCanvas;
    public TextMeshProUGUI populationText;
    public TextMeshProUGUI incomeText;
    public TextMeshProUGUI expensesText;
    public TextMeshProUGUI productionText;
    public TextMeshProUGUI scienceText;
    private Colony colony;

    void Start()
    {
        colony = XmlManager.Load();
    }

    void OnMouseEnter()
    {
        UpdateText();
        tooltipCanvas.enabled = true;
    }

    void OnMouseExit()
    {
        tooltipCanvas.enabled = false;
    }

    void UpdateText()
    {
        colony = XmlManager.Load();

        if (colony.finishedProductions != null)
        {
            colony.populationTotal = 0;
            colony.incomeTotal = 0;
            colony.expensesTotal = 0;
            colony.productionTotal = 0;
            colony.scienceTotal = 0;
            
            foreach(Production finishedProduction in colony.finishedProductions)
            {
                if (finishedProduction is not null)
                {
                    ColonyInfoPanel.ApplyBuildingYieldsToCity(finishedProduction, colony);
                }
            }

            populationText.text = $"Population: {colony.populationBaseValue + colony.populationTotal}";
            incomeText.text = $"Income: {colony.incomeBaseValue + colony.incomeTotal}";
            expensesText.text = $"Expenses: {colony.expensesBaseValue + colony.expensesTotal}";
            productionText.text = $"Production: {colony.productionBaseValue + colony.productionTotal}";
            scienceText.text = $"Science: {colony.scienceBaseValue + colony.scienceTotal}";
        }
    }
}
