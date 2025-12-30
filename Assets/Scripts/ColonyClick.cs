using UnityEngine;
using TMPro;

public class ColonyClick : MonoBehaviour
{
    //private Colony colony = new Colony();

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
        Colony colony = new Colony();

        populationText.text = $"Population: {colony.population}";
        incomeText.text = $"Income: {colony.income}";
        expensesText.text = $"Expenses: {colony.expenses}";
        productionText.text = $"Production: {colony.production}";
        scienceText.text = $"Science: {colony.science}";
    }
}
