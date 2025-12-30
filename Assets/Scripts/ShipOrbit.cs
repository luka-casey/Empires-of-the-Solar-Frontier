using UnityEngine;

public class ShipOrbit : MonoBehaviour
{
    private Quaternion initialRotation;
    public Transform currentOrbit;   // Planet_Orbit
    public float orbitRadius = 1.2f;
    public float orbitSpeed = 80f;

    public float travelSpeed = 5f;

    private float angle;
    private Transform targetOrbit;

    private enum State { Orbiting, Traveling }
    private State state = State.Orbiting;

    void Update()
    {
        if (state == State.Orbiting)
        {
            Orbit();
        }
        else
        {
            Travel();
        }
    }

    void Orbit()
    {
        if (currentOrbit == null) return;

        angle += orbitSpeed * Time.deltaTime;
        float rad = angle * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(
            Mathf.Cos(rad),
            Mathf.Sin(rad),
            0f
        ) * orbitRadius;

        transform.position = currentOrbit.position + offset;
        transform.up = offset.normalized;
    }

    void Travel()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetOrbit.position,
            travelSpeed * Time.deltaTime
        );

        Vector3 dir = (targetOrbit.position - transform.position).normalized;
        transform.up = dir;

        if (Vector3.Distance(transform.position, targetOrbit.position) < orbitRadius)
        {
            currentOrbit = targetOrbit;
            targetOrbit = null;
            state = State.Orbiting;
        }
    }

    public void MoveToOrbit(Transform newOrbit)
    {
        //Debug.Log("Moving to orbit: " + newOrbit.name);

        if (newOrbit == currentOrbit)
            return;

        targetOrbit = newOrbit;

        transform.SetParent(null);

        state = State.Traveling;
    }

    void Start()
    {
        initialRotation = transform.rotation;

        // existing Start() logic
    }

    void LateUpdate()
    {
        transform.rotation = initialRotation;
    }

}
