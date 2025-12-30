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
        populationValue.text = colony.population.ToString();
        incomeValue.text = colony.income.ToString();
        expensesValue.text = colony.expenses.ToString();
        productionValue.text = colony.production.ToString();
        scienceValue.text = colony.science.ToString();
    }
}
