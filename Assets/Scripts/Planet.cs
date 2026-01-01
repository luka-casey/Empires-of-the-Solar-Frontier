using UnityEngine;

public class Planet : MonoBehaviour
{
    public Transform orbit;

    void OnMouseDown()
    {
        if (ShipSelection.selectedShip == null)
            return;
        
        ShipSelection.selectedShip.MoveToOrbit(this.orbit);
    }
}
