using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShipViewPanel : MonoBehaviour
{
    //public
    public string planetName;
    
    //private
    private List<Ship> ships;

    //GameObjects
    public GameObject shipItemPrefab;



    public void Start()
    {
        List<string> orbitingShipNames = GetOrbitingShipNames();

        foreach (string shipName in orbitingShipNames)
        {
            Ship ship = ShipXmlManager.LoadShipData(shipName);
            ships.Add(ship);            
        }

        this.AssignShipDataToShipPanel();
    }

    private void AssignShipDataToShipPanel()
    {
        this.SetLeftPanelShipList();
    }

    private void SetLeftPanelShipList()
    {
        GameObject shipNameSelectorContainer = GameObject.Find("ShipNameSelectorContainer");

        GameObject instance = Instantiate(shipItemPrefab, shipNameSelectorContainer.transform, false);
        //TextMeshProUGUI component = instance.GetComponent<TextMeshProUGUI>();
        //component.text = 
    }

    private List<string> GetOrbitingShipNames()
    {
        List<string> orbitingShipNames = new();

        StarMapSaveData starMapData = StarMapXmlManager.Load();

        foreach(ShipLocationSaveData shipLocationSaveData in starMapData.shipsLocationSaveData)
        {
            if (CheckShipOrbitingPlanet(shipLocationSaveData, planetName))
            {
                orbitingShipNames.Add(shipLocationSaveData.shipName);
            }
        }

        return orbitingShipNames;
    }

    public static bool CheckShipOrbitingPlanet(ShipLocationSaveData shipLocationSaveData, string planetName)
    {
        switch (planetName)
        {
            case "Earth":
                if (shipLocationSaveData.currentOrbitName == "Moon_Orbit")
                    return true;
                break;

            case "Mars":
                if (shipLocationSaveData.currentOrbitName == "Moon_Orbit(Mars)")
                    return true;
                break;

            default:
                Debug.LogError($"Please add {planetName} to switch Statement in ShipViewPanel.CheckShipOrbitingPlanet");
                break;
        }

        return false;
    }

}