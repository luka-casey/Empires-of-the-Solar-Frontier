using UnityEngine;

public class Planet : MonoBehaviour
{
    public Transform orbit;

    void OnMouseDown()
    {
        if (ShipSelection.selectedShip == null)
            return;

        Debug.Log("Moving to orbit: " + this.orbit);

        //Transform orbit = transform.parent; // Planet_Orbit
        
        ShipSelection.selectedShip.MoveToOrbit(this.orbit);
    }
}
