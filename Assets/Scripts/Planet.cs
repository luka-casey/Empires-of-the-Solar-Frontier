using UnityEngine;

public class Planet : MonoBehaviour
{
    public Transform orbit;

    void OnMouseDown()
    {
        if (ShipSelection.selectedShip == null) return;

        // Queue the planet as the next target
        ShipSelection.selectedShip.QueueMove(orbit);
    }
}
