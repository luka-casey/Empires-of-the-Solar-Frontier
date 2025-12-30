using UnityEngine;
using TMPro;

public class ColonyBuildingsPanel : MonoBehaviour
{
    private Colony colony = new Colony();

    [Header("Value Texts")]
    public GameObject buildingRowPrefab;
    public TextMeshProUGUI buildingNamePrefab;
    public TextMeshProUGUI abilityTextPrefab;

    void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        foreach(Building building in colony.buildings)
        {
            // Duplicate the existing GameObject
            GameObject buildingRow = Instantiate(buildingRowPrefab, transform);

            TextMeshProUGUI buildingName = Instantiate(buildingNamePrefab, buildingRow.transform);
            TextMeshProUGUI abilityText = Instantiate(abilityTextPrefab, buildingRow.transform);

            buildingName.text = building.name;
            abilityText.text = building.abilityText;
        }
    }
}
