using UnityEngine;
using TMPro;

public class CurrentProductionPanel : MonoBehaviour
{
    public GameObject buildPrefab;
    public GameObject turnsLeftPrefab;
    private Colony colony;

    void Start()
    {
        colony = XmlManager.Load();

        Instantiate(buildPrefab, transform);
        Instantiate(turnsLeftPrefab, transform);

        UpdateSelectedProduction();
    }

    public void Refresh()
    {
        colony = XmlManager.Load();
        UpdateSelectedProduction();
    }

    private void UpdateSelectedProduction()
    {
        Transform buildTransform = transform.Find("Build(Clone)");
        TextMeshProUGUI build = buildTransform.GetComponent<TextMeshProUGUI>();
        build.text = colony.selectedProduction;

        Transform turnsLeftTransform = transform.Find("TurnsLeft(Clone)");
        TextMeshProUGUI turnsLeft = turnsLeftTransform.GetComponent<TextMeshProUGUI>();
        turnsLeft.text = colony.turnsLeft.ToString();
    }
}
