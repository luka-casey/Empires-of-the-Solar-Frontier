using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class LoadStarMapLocations : MonoBehaviour
{
    void OnMouseDown()
    {
        this.LoadStarMapDataToScene();
    }

    public void LoadStarMapDataToScene()
    {
        StarMapSaveData starMapSaveData = StarMapXmlManager.Load();

        ApplyStarMapSaveDataToUnity(starMapSaveData);

        Turn turnObject = TurnXmlManager.Load();
        GameObject shipObj = GameObject.Find("Turn_Counter");

        TextMeshProUGUI x = shipObj.GetComponent<TextMeshProUGUI>();
        x.text = "Turn " + turnObject.turn;

        //NextTurnButton[] nextTurnButtons = Object.FindObjectsOfType<NextTurnButton>();
        //nextTurnButtons[0].cu
        NextTurnButton.CurrentTurn = turnObject.turn;
    }

    private void ApplyStarMapSaveDataToUnity(StarMapSaveData starMapSaveData)
    {
        ApplyShipsLocationSaveData(starMapSaveData.shipsLocationSaveData);

        ApplyPlanetsLocationSaveData(starMapSaveData.planetsLocationSaveData);
    }

    private void ApplyPlanetsLocationSaveData(List<PlanetLocationSaveData> planetLocationSaveDatas)
    {
        foreach (var planetData in planetLocationSaveDatas)
        {
            GameObject planetObj = GameObject.Find(planetData.planetOrbit);
            if (planetObj == null) continue;

            planetObj.transform.rotation = Quaternion.Euler(0, 0, planetData.rotationZCoordinate);
        }
    }

    private void ApplyShipsLocationSaveData(List<ShipLocationSaveData> shipsLocationSaveData)
    {
        foreach (var shipData in shipsLocationSaveData)
        {
            GameObject shipObj = GameObject.Find(shipData.shipName);
            if (shipObj == null) 
            {
                Debug.LogWarning($"[Load] Ship {shipData.shipName} not found in scene.");
                continue;
            }

            ShipOrbit shipOrbit = shipObj.GetComponent<ShipOrbit>();
            if (shipOrbit == null) 
            {
                Debug.LogWarning($"[Load] ShipOrbit component missing on {shipData.shipName}");
                continue;
            }

            // Restore position
            shipObj.transform.position = new Vector3(
                shipData.positionXCoordinate,
                shipData.positionYCoordinate,
                0f
            );

            // Restore orbits
            shipOrbit.currentOrbit = GameObject.Find(shipData.currentOrbitName)?.transform;
            shipOrbit.targetOrbit = !string.IsNullOrEmpty(shipData.targetOrbitName) ?
                                    GameObject.Find(shipData.targetOrbitName)?.transform : null;
            shipOrbit.queuedOrbit = !string.IsNullOrEmpty(shipData.queuedOrbit) ?
                                    GameObject.Find(shipData.queuedOrbit)?.transform : null;

            // Restore state
            shipOrbit.state = shipData.shipState;
            shipOrbit.turnTimer = shipData.turnTimer;
            shipOrbit.turnActive = shipData.turnActive;

            Debug.Log($"[Load] Ship {shipData.shipName} restored at {shipObj.transform.position} with state {shipOrbit.state}");
        }
    }

}

