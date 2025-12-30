using UnityEngine;
using TMPro;

public class ColonyHoverUI : MonoBehaviour
{
    public Colony colony;

    public Canvas tooltipCanvas;
    public TextMeshProUGUI populationText;
    public TextMeshProUGUI incomeText;
    public TextMeshProUGUI expensesText;
    public TextMeshProUGUI productionText;
    public TextMeshProUGUI scienceText;

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
        populationText.text = $"Population: {colony.population}";
        incomeText.text = $"Income: {colony.income}";
        expensesText.text = $"Expenses: {colony.expenses}";
        productionText.text = $"Production: {colony.production}";
        scienceText.text = $"Science: {colony.science}";
    }
}
