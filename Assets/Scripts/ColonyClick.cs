using UnityEngine;
using TMPro;

public class ColonyClick : MonoBehaviour
{
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

        populationText.text = $"Population: {colony.populationBaseValue}";
        incomeText.text = $"Income: {colony.incomeBaseValue}";
        expensesText.text = $"Expenses: {colony.expensesBaseValue}";
        productionText.text = $"Production: {colony.productionBaseValue}";
        scienceText.text = $"Science: {colony.scienceBaseValue}";
    }
}
