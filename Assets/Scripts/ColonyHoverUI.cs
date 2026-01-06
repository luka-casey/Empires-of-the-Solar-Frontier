using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ColonyHoverUI : MonoBehaviour
{
    public Canvas tooltipCanvas;
    public TextMeshProUGUI populationText;
    public TextMeshProUGUI incomeText;
    public TextMeshProUGUI expensesText;
    public TextMeshProUGUI productionText;
    public TextMeshProUGUI scienceText;
    private Colony colony;
    private string colonyNameForSave;

    void Start()
    {
        ColonyIconClick colonyIconClick = GetComponent<ColonyIconClick>();
        colonyNameForSave = colonyIconClick.colonyNameForSave + ".xml";
        colony = XmlManager.Load(colonyNameForSave);
    }

    void OnMouseEnter()
    {
        // Need to make this dynamic
        if (colonyNameForSave == "Albert.xml")
        {
            Transform panelTransform = tooltipCanvas.transform.Find("Panel");
            RectTransform rectTransform = panelTransform.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(400f, -200f);
        }

        UpdateText();
        tooltipCanvas.enabled = true;
    }

    void OnMouseExit()
    {
        tooltipCanvas.enabled = false;
    }

    void UpdateText()
    {
        ColonyIconClick colonyIconClick = GetComponent<ColonyIconClick>();
        colonyNameForSave = colonyIconClick.colonyNameForSave + ".xml";
        colony = XmlManager.Load(colonyNameForSave);

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
