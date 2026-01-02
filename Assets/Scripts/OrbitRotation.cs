using UnityEngine;

public class OrbitRotation : MonoBehaviour
{
    public float orbitSpeed = 30f;
    public GameObject clickableObject;

    private float rotateTimer = 0f;
    private bool isRotating = false;

    void Update()
    {
        DetectClick();

        if (isRotating)
            RotateFor3Second();
    }

    private void DetectClick()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

        if (hit.collider == null)
            return;

        if (hit.collider.gameObject == clickableObject)
        {
            StartRotation();
        }
    }

    private void StartRotation()
    {
        rotateTimer = 0f;
        isRotating = true;
    }

    private void RotateFor3Second()
    {
        if (rotateTimer >= 3f)
        {
            isRotating = false;

            // Log Z rotation at the end of the turn
            float zRotation = Mathf.Round(transform.eulerAngles.z * 100f) / 100f;
            Debug.Log($"[{name}] Rotation after turn: {zRotation}");

            return;
        }

        rotateTimer += Time.deltaTime;
        transform.Rotate(0f, 0f, orbitSpeed * Time.deltaTime);
    }
}
