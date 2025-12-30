using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Colony
{
    public int population {get; set;}
    public int income {get; set;}
    public int expenses {get; set;}
    public int production {get; set;}
    public int science {get; set;} 
    public string history;
    public List<Building> buildings;
    public List<Production> productions;

    public Colony()
    {
        //population = 10;
        //income = 50;
        //expenses = 20;
        //production = 10;
        //science = 5;
        history = "By the late 22nd century, Earth was poised on the brink of a new frontier. The Western Alliance, led by Europe and the Americas, and the Eastern Compact, guided by China and its Asian partners, had spent decades preparing fleets, orbital stations, and interplanetary infrastructure. Humanity was about to enter its own Age of Discovery, a solar exploration reminiscent of the 14th-century voyages that opened the Americas—a race to claim new worlds, settle distant lands, and stake the first flags beyond Earth. The stars awaited, and the age of solar exploration was about to dawn.";
        buildings = CreateBuildings();
        productions = CreateProductions();
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

    private List<Production> CreateProductions()
    {
        List<Production> productions = new List<Production>();

        //Units
        Production battlestar = new Production("Battlestar", "100 Turns", ProductionTypeEnum.Unit);
        productions.Add(battlestar);

        Production fighter = new Production("Fighter", "20 Turns", ProductionTypeEnum.Unit);
        productions.Add(fighter);

        Production worker = new Production("Worker", "7 Turns", ProductionTypeEnum.Unit);
        productions.Add(worker);

        Production scientist = new Production("Scientist", "9 Turns", ProductionTypeEnum.Unit);
        productions.Add(scientist);

        //Buildings
        Production researchLab = new Production("Research Lab", "16 Turns", ProductionTypeEnum.Building);
        productions.Add(researchLab);

        Production spaceshipFactory = new Production("Spaceship Factory", "70 Turns", ProductionTypeEnum.Building);
        productions.Add(spaceshipFactory);

        Production greenhouse = new Production("Greenhouse", "17 Turns", ProductionTypeEnum.Building);
        productions.Add(greenhouse);

        Production mine = new Production("Mine", "19 Turns", ProductionTypeEnum.Building);
        productions.Add(mine);

        return productions;
    }
}

[System.Serializable]
public class Building
{
    public string name;
    public string abilityText;
    public YieldTypeEnum yieldType;
    public int yieldValue;

    public Building(string Name, string AbilityText, YieldTypeEnum YieldType, int YieldValue)
    {
        this.name = Name;
        this.abilityText = AbilityText;
        this.yieldType = YieldType;
        this.yieldValue = YieldValue;
    }
}

[System.Serializable]
public class Production
{
    public string productionName;
    public string turns;
    public ProductionTypeEnum productionType;

    public Production(string ProductionName, string Turns, ProductionTypeEnum ProductionType)
    {
        this.productionName = ProductionName;
        this.turns = Turns;
        this.productionType = ProductionType;
    }
}

public enum ProductionTypeEnum
{
    Unit,
    Building
}

public enum YieldTypeEnum
{
    Population,
    Credits,
    Production,
    Science,
    Food
}
