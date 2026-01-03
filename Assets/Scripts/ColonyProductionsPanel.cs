using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class ColonyProductionsPanel : MonoBehaviour
{
    private Colony colony;

    [Header("Value Texts")]
    public GameObject unitPrefab;
    public GameObject buildingPrefab;

    public GameObject prefab1;

    public TextMeshProUGUI productionNamePrefab;
    public TextMeshProUGUI turnsPrefab;


    void Start()
    {
        colony = XmlManager.Load();
        UpdateText();
    }

    public void UpdateText()
    {
        if (colony.productions is null)
        {
            colony.productions = CreateProductions();
        }

        List<Production> units = new List<Production>();
        List<Production> buildings = new List<Production>();

        //Get seperate lists for units and buildings
        units = colony.productions.Where(p => p.productionType == ProductionTypeEnum.Unit).ToList();
        buildings = colony.productions.Where(p => p.productionType == ProductionTypeEnum.Building).ToList();
        
        //Create wrapper objects
        GameObject unitObject = Instantiate(unitPrefab, transform);
        GameObject buildingObject = Instantiate(buildingPrefab, transform);

        foreach(Production unit in units)
        {
            GameObject prefabObject = Instantiate(prefab1, unitObject.transform);

            TextMeshProUGUI productionNameObject = Instantiate(productionNamePrefab, prefabObject.transform);
            TextMeshProUGUI turnsObject = Instantiate(turnsPrefab, prefabObject.transform);

            productionNameObject.text = unit.productionName;
            turnsObject.text = unit.turns;
        }

        foreach(Production building in buildings)
        {
            GameObject prefabObject = Instantiate(prefab1, buildingObject.transform);

            TextMeshProUGUI productionNameObject = Instantiate(productionNamePrefab, prefabObject.transform);
            TextMeshProUGUI turnsObject = Instantiate(turnsPrefab, prefabObject.transform);

            productionNameObject.text = building.productionName;
            turnsObject.text = building.turns;
        }
    }

    public static List<Production> CreateProductions()
    {
        List<Production> productions = new List<Production>();

        //Units
        //Production battlestar = new Production(1, "Battlestar", "100 Turns", ProductionTypeEnum.Unit, "Test", YieldTypeEnum.);
        //productions.Add(battlestar);

        // Production fighter = new Production(2, "Fighter", "20 Turns", ProductionTypeEnum.Unit);
        // productions.Add(fighter);

        // Production worker = new Production(3, "Worker", "7 Turns", ProductionTypeEnum.Unit);
        // productions.Add(worker);

        // Production scientist = new Production(4, "Scientist", "9 Turns", ProductionTypeEnum.Unit);
        // productions.Add(scientist);

        //Buildings
        Production greenhouse = new Production(5, "Greenhouse", "16 Turns", ProductionTypeEnum.Building, "+5 Food", YieldTypeEnum.Food, 5);
        productions.Add(greenhouse);

        Production mine = new Production(5, "Mine", "13 Turns", ProductionTypeEnum.Building, "test", YieldTypeEnum.Food, 5);
        productions.Add(mine);

        // Production spaceshipFactory = new Production(6, "Spaceship Factory", "70 Turns", ProductionTypeEnum.Building);
        // productions.Add(spaceshipFactory);

        // Production greenhouse = new Production(7, "Greenhouse", "17 Turns", ProductionTypeEnum.Building);
        // productions.Add(greenhouse);

        // Production mine = new Production(8, "Mine", "19 Turns", ProductionTypeEnum.Building);
        // productions.Add(mine);

        return productions;
    }
}