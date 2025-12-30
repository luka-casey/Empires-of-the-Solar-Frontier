using UnityEngine;

public class ShipSelection : MonoBehaviour
{
    public static ShipOrbit selectedShip;
    public float pulseSpeed = 2f;      // Speed of pulsing
    public float pulseAmount = 0.2f;   // Max extra scale

    private Vector3 initialScale;

    void Start()
    {
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (selectedShip == GetComponent<ShipOrbit>())
        {
            // Pulse scale from 1x to 1+pulseAmount
            float scale = 1 + (Mathf.Sin(Time.time * pulseSpeed) * 0.5f + 0.5f) * pulseAmount;
            transform.localScale = initialScale * scale;
        }
        else
        {
            // Reset scale if not selected
            transform.localScale = initialScale;
        }
    }

    void OnMouseDown()
    {
        if (selectedShip is null)
        {
            selectedShip = GetComponent<ShipOrbit>();
        }
        else
        {
            selectedShip = null;
        }
    }
}
