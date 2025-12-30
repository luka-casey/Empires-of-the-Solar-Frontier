using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class ColonyProductionsPanel : MonoBehaviour
{
    private Colony colony = new Colony();

    [Header("Value Texts")]
    public GameObject unitPrefab;
    public GameObject buildingPrefab;

    public GameObject prefab1;

    public TextMeshProUGUI productionNamePrefab;
    public TextMeshProUGUI turnsPrefab;


    void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
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
}