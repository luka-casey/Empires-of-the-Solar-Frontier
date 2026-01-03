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
        if (colony.finishedProductions != null)
        {
            foreach(Production finishedProduction in colony.finishedProductions)
            {
                ColonyInfoPanel.ApplyBuildingYieldsToCity(finishedProduction, colony);
            }

            populationText.text = $"Population: {colony.population}";
            incomeText.text = $"Income: {colony.income}";
            expensesText.text = $"Expenses: {colony.expenses}";
            productionText.text = $"Production: {colony.production}";
            scienceText.text = $"Science: {colony.science}";
        }
    }
}
