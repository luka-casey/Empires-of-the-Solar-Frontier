using UnityEngine;

public class Planet : MonoBehaviour
{
    public Transform orbit;

    void OnMouseOver()
    {
        if (!Input.GetMouseButtonDown(1))
            return;

        if (ShipSelection.selectedShip == null)
            return;

        ShipSelection.selectedShip.QueueMove(orbit);
    }
}
