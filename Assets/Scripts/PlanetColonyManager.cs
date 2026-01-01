using UnityEngine;

public class PlanetColonyManager : MonoBehaviour
{
    public GameObject colonyIconPrefab;
    public GameObject planetPanelsPrefab;
    public Transform colonyIconSpawnPoint;

    private GameObject planetPanelsInstance; // store instantiated panel
    private GameObject colonyIconInstance;   // store instantiated icon

    public bool hasColony = false;

    public void EstablishColony()
    {
        if (hasColony) return;

        hasColony = true;

        // Instantiate colony icon attached to planet
        colonyIconInstance = Instantiate(colonyIconPrefab, colonyIconSpawnPoint.position, Quaternion.identity, transform);

        // Add a click listener to the icon
        var clickHandler = colonyIconInstance.AddComponent<ColonyIconClick>();
        clickHandler.Init(this);

        // Instantiate panel at root and store reference
        planetPanelsInstance = Instantiate(planetPanelsPrefab);

        // Start with panel hidden
        planetPanelsInstance.SetActive(false);
    }

    public void ToggleColonyPanels()
    {
        if (planetPanelsInstance != null)
        {
            planetPanelsInstance.SetActive(!planetPanelsInstance.activeSelf);
        }
    }
}
