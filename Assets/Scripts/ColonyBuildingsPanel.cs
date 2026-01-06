using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ColonyBuildingsPanel : MonoBehaviour
{
    private Colony colony;
    private string colonyNameForSave;

    [Header("Value Texts")]
    public GameObject buildingRowPrefab;
    public TextMeshProUGUI buildingNamePrefab;
    public TextMeshProUGUI abilityTextPrefab;

    void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        PlanetPanelsScript planetPanel = GetComponentInParent<PlanetPanelsScript>();
        colonyNameForSave = planetPanel.colonyName + ".xml";
        colony = XmlManager.Load(colonyNameForSave);

        if (colony.finishedProductions is not null)
        {
            colony.incomeTotal = 0;
            colony.productionTotal = 0;
            colony.scienceTotal = 0;
            colony.populationTotal = 0;

            foreach (Production production in colony.finishedProductions)
            {
                if (production is not null)
                {
                    // Duplicate the existing GameObject
                    GameObject buildingRow = Instantiate(buildingRowPrefab, transform);

                    TextMeshProUGUI buildingName = Instantiate(buildingNamePrefab, buildingRow.transform);
                    TextMeshProUGUI abilityText = Instantiate(abilityTextPrefab, buildingRow.transform);

                    buildingName.text = production.productionName;
                    abilityText.text = production.abilityText;

                    ColonyInfoPanel.ApplyBuildingYieldsToCity(production, this.colony);
                }
            }

            ColonyInfoPanel[] colonyInfoPanels = Object.FindObjectsOfType<ColonyInfoPanel>();

            colonyInfoPanels[0].populationValue.text = (colony.populationBaseValue + colony.populationTotal).ToString();
            colonyInfoPanels[0].incomeValue.text = (colony.incomeBaseValue + colony.incomeTotal).ToString();
            colonyInfoPanels[0].expensesValue.text = (colony.expensesBaseValue + colony.expensesTotal).ToString();
            colonyInfoPanels[0].productionValue.text = (colony.productionBaseValue + colony.productionTotal).ToString();
            colonyInfoPanels[0].scienceValue.text = (colony.scienceBaseValue + colony.scienceTotal).ToString();
        }
    }

    // private List<Building> CreateBuildings()
    // {
    //     List<Building> buildings = new List<Building>();

    //     Building researchLab = new Building("Research Lab", "Science +3", YieldTypeEnum.Science, 3);
    //     buildings.Add(researchLab);

    //     Building spaceshipFactory = new Building("Spaceship Factory", "Production +10", YieldTypeEnum.Production, 10);
    //     buildings.Add(spaceshipFactory);

    //     Building greenhouse = new Building("Green House", "Food +7", YieldTypeEnum.Food, 7);
    //     buildings.Add(greenhouse);

    //     Building mine = new Building("Mine", "Credits +8", YieldTypeEnum.Credits, 8);
    //     buildings.Add(mine);

    //     return buildings;
    // }
}