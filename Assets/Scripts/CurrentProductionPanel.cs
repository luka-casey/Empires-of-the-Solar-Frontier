using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class CurrentProductionPanel : MonoBehaviour
{
    public GameObject buildPrefab;
    public GameObject turnsLeftPrefab;
    private Colony colony;
    private string colonyNameForSave;
    private List<Production> productions;

    void Start()
    {
        PlanetPanelsScript planetPanel = GetComponentInParent<PlanetPanelsScript>();
        colonyNameForSave = planetPanel.colonyName + ".xml";
        colony = XmlManager.Load(colonyNameForSave);

        Instantiate(buildPrefab, transform);
        Instantiate(turnsLeftPrefab, transform);

        UpdateSelectedProduction();
    }

    public void Refresh()
    {
        PlanetPanelsScript planetPanel = GetComponentInParent<PlanetPanelsScript>();
        colonyNameForSave = planetPanel.colonyName + ".xml";
        colony = XmlManager.Load(colonyNameForSave);

        UpdateSelectedProduction();
    }

    private void UpdateSelectedProduction()
    {
        PlanetPanelsScript planetPanel = GetComponentInParent<PlanetPanelsScript>();
        colonyNameForSave = planetPanel.colonyName + ".xml";
        colony = XmlManager.Load(colonyNameForSave);

        Transform buildTransform = transform.Find("Build(Clone)");
        TextMeshProUGUI build = buildTransform.GetComponent<TextMeshProUGUI>();
        build.text = colony.selectedProduction;

        Transform turnsLeftTransform = transform.Find("TurnsLeft(Clone)");
        TextMeshProUGUI turnsLeft = turnsLeftTransform.GetComponent<TextMeshProUGUI>();
        turnsLeft.text = colony.turnsLeft.ToString();

        if (colony.selectedProduction is not null && colony.selectedProduction != "")
        {
            var temp = ColonyProductionsPanel.CreateProductions();
            Production? currentProduction = temp.FirstOrDefault(p => p.productionName == colony.selectedProduction);
            Sprite loadedSprite = Resources.Load<Sprite>($"imageAssets/{currentProduction.imageName}");

            Transform imageTransform = transform.Find("Image"); 
            Image image = imageTransform.GetComponent<Image>();
            image.sprite = loadedSprite;
            image.color = Color.deepSkyBlue;
        }

        if (colony.turnsLeft == 0 || colony.turnsLeft < 0)
        {
            turnsLeft.text = "";

            Sprite loadedSprite = Resources.Load<Sprite>($"imageAssets/SelectProduction");
            GameObject imageTransform = GameObject.Find("Image"); 
            Image image = imageTransform.GetComponent<Image>();
            image.sprite = loadedSprite;
            image.color = Color.deepSkyBlue; //Color.cyan; 
        }

        if (colony.turnsLeft == 0 && colony.selectedProduction != "")
        {
            build.text = "";
            turnsLeft.text = "";

            productions = ColonyProductionsPanel.CreateProductions();

            Production? currentProduction = productions.FirstOrDefault(p => p.productionName == colony.selectedProduction);

            if (currentProduction is null)
            {
                //Debug.LogError("Cannot find currentproduction in production list (add it to CreateProductions())");
            }

            if (!colony.finishedProductions.Any(fp => fp.productionName == currentProduction.productionName))
            {
                colony.finishedProductions.Add(currentProduction);
            }
            colony.selectedProduction = "";

            XmlManager.Save(colony, colonyNameForSave);
        }
    }
}
