using UnityEngine;

public class OrbitRotation : MonoBehaviour
{
    public float orbitSpeed = 30f; // degrees per second

    void Update()
    {
        transform.Rotate(0f, 0f, orbitSpeed * Time.deltaTime);
    }
}
