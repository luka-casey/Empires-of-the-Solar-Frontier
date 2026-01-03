using UnityEngine;

public class ShipOrbit : MonoBehaviour
{
    [Header("Orbit")]
    public Transform currentOrbit;
    public float orbitRadius = 1.2f;
    public float orbitSpeed = 8f;

    [Header("Travel")]
    public float travelSpeed = 5f;

    [Header("Turn")]
    public float maxTurnTime = 3f;

    private float angle;

    public Transform queuedOrbit;
    public Transform targetOrbit;

    public float turnTimer;
    public bool turnActive;

    public ShipState state = ShipState.Orbiting;

    private Quaternion initialRotation;

    // ───────── INITIALISATION ─────────

    void Start()
    {
        initialRotation = transform.rotation;

        // ✅ Place ship correctly at game start
        PlaceOnOrbit();

        Debug.Log($"[{name}] Start() → Initial orbit = {currentOrbit?.name}");
    }

    // ───────── UPDATE LOOP ─────────

    void Update()
    {
        // ❌ Nothing moves unless a turn is active
        if (!turnActive)
            return;

        // Movement only happens during the turn
        if (state == ShipState.Orbiting)
        {
            Orbit();
        }
        else if (state == ShipState.Traveling)
        {
            Travel();
        }

        turnTimer += Time.deltaTime;

        if (turnTimer >= maxTurnTime)
            EndTurn();
    }

    // ───────── TURN CONTROL ─────────

    public void StartTurn()
    {
        Debug.Log($"[{name}] StartTurn()");

        turnTimer = 0f;
        turnActive = true;

        if (targetOrbit != null)
        {
            state = ShipState.Traveling;
            Debug.Log($"[{name}] Resuming travel → {targetOrbit.name}");
        }
        else if (queuedOrbit != null)
        {
            targetOrbit = queuedOrbit;
            queuedOrbit = null;
            state = ShipState.Traveling;

            Debug.Log($"[{name}] Promoted queued orbit → {targetOrbit.name}");
        }
        else
        {
            state = ShipState.Orbiting;
            Debug.Log($"[{name}] No queued move or travel this turn");
        }
    }

    private void EndTurn()
    {
        Debug.Log($"[{name}] EndTurn()");
        turnActive = false;

        Vector2 shipPosition = new Vector2(transform.position.x, transform.position.y);

        // Debug.Log(
        //     $"[{name}] Ship EndTurn Data → " +
        //     $"Position: {shipPosition}, " +
        //     $"CurrentOrbit: {currentOrbit?.name ?? "null"}, " +
        //     $"TargetOrbit: {targetOrbit?.name ?? "null"}, " +
        //     $"QueuedOrbit: {queuedOrbit?.name ?? "null"}, " +
        //     $"State: {state}, " +
        //     $"TurnTimer: {turnTimer:F2}, " +
        //     $"TurnActive: {turnActive}"
        // );

        if (state == ShipState.Orbiting && targetOrbit == null)
        {
            Debug.Log($"[{name}] Ship now locked into orbit around {currentOrbit.name}");
        }

        if (ShipSelection.selectedShip == this)
            ShipSelection.DeselectCurrent();
    }

    // ───────── MOVEMENT ─────────

    void PlaceOnOrbit()
    {
        if (currentOrbit == null)
            return;

        // Deterministic later → saved angle
        angle = Random.Range(0f, 360f);

        float rad = angle * Mathf.Deg2Rad;

        Vector3 offset = new Vector3(
            Mathf.Cos(rad),
            Mathf.Sin(rad),
            0f
        ) * orbitRadius;

        transform.position = currentOrbit.position + offset;
        transform.up = offset.normalized;
    }

    void Orbit()
    {
        if (currentOrbit == null)
            return;

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
        if (targetOrbit == null)
            return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetOrbit.position,
            travelSpeed * Time.deltaTime
        );

        Vector3 dir = (targetOrbit.position - transform.position).normalized;
        transform.up = dir;

        float dist = Vector3.Distance(transform.position, targetOrbit.position);

        if (dist <= orbitRadius)
        {
            Debug.Log($"[{name}] Reached {targetOrbit.name}");

            currentOrbit = targetOrbit;
            targetOrbit = null;
            state = ShipState.Orbiting;
        }
    }

    // ───────── COMMAND ─────────

    public void QueueMove(Transform orbit)
    {
        Debug.Log($"[{name}] QueueMove({orbit.name})");

        if (orbit == currentOrbit)
            return;

        queuedOrbit = orbit;
    }

    // ───────── VISUAL LOCK ─────────

    void LateUpdate()
    {
        transform.rotation = initialRotation;
    }
}
