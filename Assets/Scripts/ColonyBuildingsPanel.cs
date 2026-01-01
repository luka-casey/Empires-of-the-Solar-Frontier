using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ColonyBuildingsPanel : MonoBehaviour
{
    private Colony colony = new Colony();

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
        if (colony.buildings == null)
        {
            colony.buildings = CreateBuildings();
        }

        foreach(Building building in colony.buildings)
        {
            // Duplicate the existing GameObject
            GameObject buildingRow = Instantiate(buildingRowPrefab, transform);

            TextMeshProUGUI buildingName = Instantiate(buildingNamePrefab, buildingRow.transform);
            TextMeshProUGUI abilityText = Instantiate(abilityTextPrefab, buildingRow.transform);

            buildingName.text = building.name;
            abilityText.text = building.abilityText;
        }
    }

    private List<Building> CreateBuildings()
    {
        List<Building> buildings = new List<Building>();

        Building researchLab = new Building("Research Lab", "Science +3", YieldTypeEnum.Science, 3);
        buildings.Add(researchLab);

        Building spaceshipFactory = new Building("Spaceship Factory", "Production +10", YieldTypeEnum.Production, 10);
        buildings.Add(spaceshipFactory);

        Building greenhouse = new Building("Green House", "Food +7", YieldTypeEnum.Food, 7);
        buildings.Add(greenhouse);

        Building mine = new Building("Mine", "Credits +8", YieldTypeEnum.Credits, 8);
        buildings.Add(mine);

        return buildings;
    }
}