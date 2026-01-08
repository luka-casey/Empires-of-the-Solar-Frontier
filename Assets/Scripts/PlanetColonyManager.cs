using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public class PlanetColonyManager : MonoBehaviour
{
    public GameObject colonyIconPrefab;
    public GameObject planetPanelsPrefab;

    public Transform colonyIconSpawnPointA;
    public Transform colonyIconSpawnPointB;
    public Transform colonyIconSpawnPointC;

    public TMPro.TMP_FontAsset copperplateFont;


    private List<GameObject> planetPanelsInstances;

    private GameObject planetPanelsInstance; 
    private GameObject colonyIconInstance;  
    private GameObject planetPanelsInstance1;
    private GameObject colonyIconInstance1;   

    public bool hasColony = false;

    void Start()
    {
        //Load Existing Collonies
        this.LoadColonies();        
    }

    public void LoadColonies()
    {
        List<string> colonyNames = XmlManager.LoadPlanetColonyNames("Earth").colonyNames;
        
        if (colonyNames.Count > 0)
        {
            int colonyCount = 0;

            foreach (string newColonyName in colonyNames)
            {
                colonyCount++;
                //Store for later, will set based on how many already exist
                List<Transform> colonySpawnPoints = new List<Transform>();
                colonySpawnPoints.Add(colonyIconSpawnPointA);
                colonySpawnPoints.Add(colonyIconSpawnPointB);
                colonySpawnPoints.Add(colonyIconSpawnPointC);

                //int colonyCount = XmlManager.LoadPlanetColonyNames("Earth").colonyNames.Count;
                var selectedSpawnPoint = colonySpawnPoints[colonyCount - 1];

                hasColony = true;

                // Instantiate colony icon attached to planet                                          
                GameObject colonyIconInstanceTest = Instantiate(colonyIconPrefab, selectedSpawnPoint.position, Quaternion.identity, transform);

                // Add a click listener to the icon
                var clickHandler = colonyIconInstanceTest.AddComponent<ColonyIconClick>();
                clickHandler.Init(this);
                clickHandler.colonyNameForSave = newColonyName;

                AddColonyNameText(colonyIconInstanceTest, newColonyName);

                // Instantiate panel at root and store reference
                GameObject planetPanelsInstanceTest = Instantiate(planetPanelsPrefab);
                PlanetPanelsScript planetPanelsScript = planetPanelsInstanceTest.GetComponent<PlanetPanelsScript>();
                planetPanelsScript.colonyName = newColonyName;

                // Start with panel hidden
                planetPanelsInstanceTest.SetActive(false);
            }
        }
    }

    public void OpenColonyModel()
    {
        int colonyCount = XmlManager.LoadPlanetColonyNames("Earth").colonyNames.Count;//if Planet.Colonies < 4
        if (colonyCount < 3)
        {
            FindObjectOfType<SimpleNameModalBuilder>().Open();
        }
    }

    public void InitializeNewColonyCreation(string newColonyName)
    {
        //Store for later, will set based on how many already exist
        List<Transform> colonySpawnPoints = new List<Transform>();
        colonySpawnPoints.Add(colonyIconSpawnPointA);
        colonySpawnPoints.Add(colonyIconSpawnPointB);
        colonySpawnPoints.Add(colonyIconSpawnPointC);

        int colonyCount = XmlManager.LoadPlanetColonyNames("Earth").colonyNames.Count;
        var selectedSpawnPoint = colonySpawnPoints[colonyCount];

        hasColony = true;

        // Instantiate colony icon attached to planet                                          
        GameObject colonyIconInstanceTest = Instantiate(colonyIconPrefab, selectedSpawnPoint.position, Quaternion.identity, transform);

        // Add a click listener to the icon
        var clickHandler = colonyIconInstanceTest.AddComponent<ColonyIconClick>();
        clickHandler.Init(this);
        clickHandler.colonyNameForSave = newColonyName;

        AddColonyNameText(colonyIconInstanceTest, newColonyName);

        // Instantiate panel at root and store reference
        GameObject planetPanelsInstanceTest = Instantiate(planetPanelsPrefab);
        PlanetPanelsScript planetPanelsScript = planetPanelsInstanceTest.GetComponent<PlanetPanelsScript>();
        planetPanelsScript.colonyName = newColonyName;

        // Start with panel hidden
        planetPanelsInstanceTest.SetActive(false);

        //Create XML For new Colony
        var filePath = Path.Combine(Application.persistentDataPath, $"{newColonyName}.xml");
        
        if (!File.Exists(filePath))
        {
            using (File.Create(filePath)) { }
        }

        List<string> colonyNames = XmlManager.LoadPlanetColonyNames("Earth").colonyNames;
        colonyNames.Add(newColonyName);

        SolarPlanet solarPlanet = new SolarPlanet()
        {
            planetName = "Earth",
            colonyNames = colonyNames
        };

        Colony newColony = Colony.CreateNewColonyObject();
        newColony.productions = ColonyProductionsPanel.CreateProductions();

        XmlManager.Save(newColony, $"{newColonyName}.xml");
        XmlManager.SavePlanetColonyNames(solarPlanet);
    }

    public void AddColonyNameText(GameObject colonyIconInst, string colonyName)
    {
        // Create colony name (sibling)
        GameObject colonyNameGO = new GameObject("ColonyName");

        // Same parent as the icon
        colonyNameGO.transform.SetParent(colonyIconInst.transform.parent);

        // Position it above the icon in world space
        colonyNameGO.transform.position = colonyIconInst.transform.position + new Vector3(0f, 0.25f, 0f);

        // Add TextMeshPro (3D)
        TMPro.TextMeshPro textMesh = colonyNameGO.AddComponent<TMPro.TextMeshPro>();

        // Configure text
        textMesh.text = colonyName;
        textMesh.fontSize = 1.8f;
        textMesh.alignment = TMPro.TextAlignmentOptions.Center;
        textMesh.color = Color.black;
        textMesh.font = copperplateFont;
        textMesh.fontStyle = TMPro.FontStyles.UpperCase;

        // Make sure it renders on top
        textMesh.renderer.sortingOrder = 10;
    }

    public void ToggleColonyPanels(string colonyName)
    {
        PlanetPanelsScript[] panels = FindObjectsOfType<PlanetPanelsScript>(true);

        foreach (PlanetPanelsScript panel in panels)
        {
            if (panel.colonyName == colonyName)
            {
                panel.gameObject.SetActive(!panel.gameObject.activeSelf);
                return;
            }
        }
    }

}
