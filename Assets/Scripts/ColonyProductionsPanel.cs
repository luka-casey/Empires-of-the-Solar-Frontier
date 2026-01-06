using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using System.Data.Common;

public class ColonyProductionsPanel : MonoBehaviour
{
    private Colony colony;
    private string colonyNameForSave;

    [Header("Value Texts")]
    public GameObject unitPrefab;
    public GameObject buildingPrefab;

    public GameObject prefab1;

    public TextMeshProUGUI productionNamePrefab;
    public TextMeshProUGUI turnsPrefab;


    void Start()
    {
        PlanetPanelsScript planetPanel = GetComponentInParent<PlanetPanelsScript>();
        colonyNameForSave = planetPanel.colonyName + ".xml";
        colony = XmlManager.Load(colonyNameForSave);

        if (colony.turnsLeft == -33)
        {
            NextTurnButton.UpdateProductionsTurns(colonyNameForSave);
        }

        UpdateText();
    }

    public void UpdateText()
    {
        PlanetPanelsScript planetPanel = GetComponentInParent<PlanetPanelsScript>();
        colonyNameForSave = planetPanel.colonyName + ".xml";

        colony = XmlManager.Load(colonyNameForSave);

        if (colony.productions is null)
        {
            colony.productions = CreateProductions();
        }

        List<Production> units = new List<Production>();
        List<Production> buildings = new List<Production>();

        var validProdoctions = new List<Production>();

        //makes sure Productions list does not show a completed production
        foreach (Production production in colony.productions)
        {
            if (colony.turnsLeft == 0 && colony.selectedProduction == production.productionName)
            {
                // Dont add if we just finished the select product
            }
            else
            {
                //For all finished buildings that are already there
                if (!colony.finishedProductions.Any(fp => fp.productionName == production.productionName))
                {
                    validProdoctions.Add(production);
                }
            }
        }

        units = validProdoctions
            .Where(p => p.productionType == ProductionTypeEnum.Unit) 
            .ToList();

        buildings = validProdoctions
            .Where(p => p.productionType == ProductionTypeEnum.Building) 
            .ToList();

        //refersh Productions Panel
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        
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

    public static void UpdateProductionMeter(Colony colony, Production specificProduction)
    {
        colony.incomeTotal = 0;
        colony.productionTotal = 0;
        colony.scienceTotal = 0;
        colony.populationTotal = 0;

        foreach(Production production in colony.finishedProductions)
        {
            if (production is not null)
            {
                ColonyInfoPanel.ApplyBuildingYieldsToCity(production, colony);
            }
        }

        int colonyTotalProductionPerTurn = colony.productionBaseValue + colony.productionTotal;

        if (colony.selectedProduction == specificProduction.productionName && colony.turnsLeft != 0)
        {
            specificProduction.productionMeter += colonyTotalProductionPerTurn;
        }
        else if (colony.selectedProduction == "" && specificProduction.productionName != "" && colony.turnsLeft == 0)
        {
            specificProduction.productionMeter += colonyTotalProductionPerTurn;
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
        Production greenhouse = new Production(1, "Greenhouse", "3", ProductionTypeEnum.Building, "+6 Population", YieldTypeEnum.Population, 6, "Greenhouse", 22, 0);
        productions.Add(greenhouse);

        Production mine = new Production(2, "Mine", "3", ProductionTypeEnum.Building, "+2 Production", YieldTypeEnum.Production, 2, "Mine", 30, 0);
        productions.Add(mine);

        Production laboratory = new Production(3, "Laboratory", "3", ProductionTypeEnum.Building, "+10 Science", YieldTypeEnum.Science, 10, "Lab", 26, 0);
        productions.Add(laboratory);

        // Production spaceshipFactory = new Production(6, "Spaceship Factory", "70 Turns", ProductionTypeEnum.Building);
        // productions.Add(spaceshipFactory);

        // Production greenhouse = new Production(7, "Greenhouse", "17 Turns", ProductionTypeEnum.Building);
        // productions.Add(greenhouse);

        // Production mine = new Production(8, "Mine", "19 Turns", ProductionTypeEnum.Building);
        // productions.Add(mine);

        return productions;
    }
}