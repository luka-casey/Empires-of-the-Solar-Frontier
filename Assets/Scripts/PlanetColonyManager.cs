using UnityEngine;

public class PlanetColonyManager : MonoBehaviour
{
    public GameObject colonyIconPrefab;
    public GameObject planetPanelsPrefab;
    public Transform colonyIconSpawnPoint;

    public GameObject colonyIconPrefab1;
    public GameObject planetPanelsPrefab1;
    public Transform colonyIconSpawnPoint1;

    private GameObject planetPanelsInstance; 
    private GameObject colonyIconInstance;  

    private GameObject planetPanelsInstance1;
    private GameObject colonyIconInstance1;   

    public bool hasColony = false;

    void Start()
    {
        this.EstablishColony();        
    }

    public void EstablishColony()
    {
        EstablishColonyVictoria();
        EstablishColonyAlbert();
    }

    public void EstablishColonyVictoria()
    {
        //if (hasColony) return;

        hasColony = true;

        // Instantiate colony icon attached to planet
        colonyIconInstance = Instantiate(colonyIconPrefab, colonyIconSpawnPoint.position, Quaternion.identity, transform);

        // Add a click listener to the icon
        var clickHandler = colonyIconInstance.AddComponent<ColonyIconClick>();
        clickHandler.Init(this);
        clickHandler.colonyNameForSave = "Victoria";

        // Instantiate panel at root and store reference
        planetPanelsInstance = Instantiate(planetPanelsPrefab);
        PlanetPanelsScript planetPanelsScript = planetPanelsInstance.GetComponent<PlanetPanelsScript>();
        planetPanelsScript.colonyName = "Victoria";

        // Start with panel hidden
        planetPanelsInstance.SetActive(false);
    }

    public void EstablishColonyAlbert()
    {
        //if (hasColony) return;

        hasColony = true;

        // Instantiate colony icon attached to planet
        colonyIconInstance1 = Instantiate(colonyIconPrefab1, colonyIconSpawnPoint1.position, Quaternion.identity, transform);

        // Add a click listener to the icon
        var clickHandler = colonyIconInstance1.AddComponent<ColonyIconClick>();
        clickHandler.Init(this);
        clickHandler.colonyNameForSave = "Albert";

        // Instantiate panel at root and store reference
        planetPanelsInstance1 = Instantiate(planetPanelsPrefab1);
        PlanetPanelsScript planetPanelsScript = planetPanelsInstance1.GetComponent<PlanetPanelsScript>();
        planetPanelsScript.colonyName = "Albert";

        // Start with panel hidden
        planetPanelsInstance1.SetActive(false);
    }

    public void ToggleColonyPanels(string colonyName)
    {
        if (colonyName == "Victoria")
        {
            if (planetPanelsInstance != null)
            {
                planetPanelsInstance.SetActive(!planetPanelsInstance.activeSelf);
            }
        }
        else if (colonyName == "Albert")
        {
            if (planetPanelsInstance1 != null)
            {
                planetPanelsInstance1.SetActive(!planetPanelsInstance1.activeSelf);
            }
        }
    }
}
