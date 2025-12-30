using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class OrbitRing : MonoBehaviour
{
    public float radius = 3f;
    public int segments = 128;

    void Start()
    {
        DrawCircle();
    }

    void DrawCircle()
    {
        LineRenderer lr = GetComponent<LineRenderer>();
        lr.positionCount = segments;
        lr.loop = true;

        float angleStep = 360f / segments;

        for (int i = 0; i < segments; i++)
        {
            float angle = Mathf.Deg2Rad * angleStep * i;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            lr.SetPosition(i, new Vector3(x, y, 0f));
        }
    }
}
