using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveStarMapLocations : MonoBehaviour
{
    void OnMouseDown()
    {
        this.Save();
    }

    public void Save()
    {
        StarMapSaveData starMapSaveData = Collect();

        StarMapXmlManager.Save(starMapSaveData);

        SaveTurn();

        SaveColonyInfo();
    }

    public void SaveColonyInfo()
    {
        //Todo
    }

    public void SaveTurn()
    {
        Turn turnObject = new Turn()
        {
            turn = NextTurnButton.CurrentTurn
        };
        
        TurnXmlManager.Save(turnObject);
    }

    public StarMapSaveData Collect()
    {
        StarMapSaveData saveData = new StarMapSaveData();

        var planetsData = CollectPlanets(saveData);
        var shipsData = CollectShips(saveData);

        saveData.planetsLocationSaveData = planetsData;
        saveData.shipsLocationSaveData = shipsData;

        return saveData;
    }

    private List<PlanetLocationSaveData> CollectPlanets(StarMapSaveData saveData)
    {
        OrbitRotation[] planetOrbits = Object.FindObjectsOfType<OrbitRotation>();

        List<PlanetLocationSaveData> planetsLocationSaveData = new List<PlanetLocationSaveData>();

        foreach (var orbit in planetOrbits)
        {
            PlanetLocationSaveData planetData = new PlanetLocationSaveData
            {
                planetOrbit = orbit.name,
                rotationZCoordinate = orbit.transform.eulerAngles.z
            };

            planetsLocationSaveData.Add(planetData);

            // Debug.Log(
            //     $"[Save] Planet {planetData.planetOrbit} rotation Z: {planetData.rotationZCoordinate}"
            // );
        }

        return planetsLocationSaveData;
    }

    private List<ShipLocationSaveData> CollectShips(StarMapSaveData saveData)
    {
        ShipOrbit[] ships = Object.FindObjectsOfType<ShipOrbit>();

        List<ShipLocationSaveData> shipsLocationSaveData = new List<ShipLocationSaveData>();

        foreach (var ship in ships)
        {
            Vector3 pos = ship.transform.position;

            ShipLocationSaveData shipData = new ShipLocationSaveData
            {
                shipName = ship.name,
                positionXCoordinate = pos.x,
                positionYCoordinate = pos.y,
                currentOrbitName = ship.currentOrbit ? ship.currentOrbit.name : null,
                targetOrbitName = ship.targetOrbit ? ship.targetOrbit.name : null,
                queuedOrbit = ship.queuedOrbit ? ship.queuedOrbit.name : null,
                shipState = ship.state,
                turnTimer = ship.turnTimer,
                turnActive = ship.turnActive
            };

            shipsLocationSaveData.Add(shipData);

            // Debug.Log(
            //     $"[Save] Ship {shipData.shipName} | State: {shipData.shipState} | Orbit: {shipData.currentOrbitName}"
            // );
        }

        return shipsLocationSaveData;
    }
}


[System.Serializable]
public class StarMapSaveData
{
    public List<PlanetLocationSaveData> planetsLocationSaveData;
    public List<ShipLocationSaveData> shipsLocationSaveData;

    public StarMapSaveData()
    {
    }
}

[System.Serializable]
public class PlanetLocationSaveData
{
    public string planetOrbit;
    public float rotationZCoordinate;

    public PlanetLocationSaveData()
    {
    }
}

[System.Serializable]
public class ShipLocationSaveData
{
    public string shipName;//[Ship] Ship EndTurn Data → 
    public float positionXCoordinate;
    public float positionYCoordinate;
    public string currentOrbitName;// CurrentOrbit: Moon_Orbit, 
    public string targetOrbitName;// TargetOrbit: Moon_Orbit(Mars), 
    public string queuedOrbit;// QueuedOrbit: null, 
    public ShipState shipState; // State: Traveling, 
    public float turnTimer; // TurnTimer: 3.00, 
    public bool turnActive;// TurnActive: False

    public ShipLocationSaveData()
    {
    }
}
