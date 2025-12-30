using UnityEngine;
using System.Collections;

public class SceneZoomIn : MonoBehaviour
{
    public float startZoom = 6f;
    public float targetZoom = 2f;
    public float zoomDuration = 0.8f;

    Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
        cam.orthographicSize = startZoom;
        StartCoroutine(ZoomIn());
    }

    IEnumerator ZoomIn()
    {
        float elapsed = 0f;

        while (elapsed < zoomDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / zoomDuration;

            cam.orthographicSize = Mathf.Lerp(startZoom, targetZoom, t);
            yield return null;
        }

        cam.orthographicSize = targetZoom;
    }
}
