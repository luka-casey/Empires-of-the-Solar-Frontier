using UnityEngine;

public class ColonyIconClick : MonoBehaviour
{
    private PlanetColonyManager manager;
    public string colonyNameForSave;

    public void Init(PlanetColonyManager manager)
    {
        this.manager = manager;
    }

    void OnMouseDown()
    {
        // Toggle the panel when the icon is clicked
        manager.ToggleColonyPanels(colonyNameForSave);
    }
}
