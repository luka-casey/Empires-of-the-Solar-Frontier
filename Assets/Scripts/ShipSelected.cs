using UnityEngine;

public class ShipSelection : MonoBehaviour
{
    public static ShipOrbit selectedShip;

    public float pulseSpeed = 2f;
    public float pulseAmount = 0.2f;

    private Vector3 initialScale;
    private ShipOrbit shipOrbit;

    void Awake()
    {
        shipOrbit = GetComponent<ShipOrbit>();
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (selectedShip == shipOrbit)
        {
            float scale = 1f + (Mathf.Sin(Time.time * pulseSpeed) * 0.5f + 0.5f) * pulseAmount;

            transform.localScale = initialScale * scale;
        }
        else
        {
            transform.localScale = initialScale;
        }
    }

    void OnMouseDown()
    {
        if (selectedShip == shipOrbit)
        {
            selectedShip = null;
        }
        else
        {
            selectedShip = shipOrbit;
        }
    }

    public static void DeselectCurrent()
    {
        selectedShip = null;
    }
}
