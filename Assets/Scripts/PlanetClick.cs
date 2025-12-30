using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

public class PlanetClick2D : MonoBehaviour
{
    public string sceneToLoad;
    public float zoomDuration = 0.8f;
    public float targetZoom = 1.5f;

    bool isZooming = false;

    void Update()
    {
        if (isZooming) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Vector2 mouseWorldPos =
                Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                StartCoroutine(ZoomAndLoad());
            }
        }
    }

    IEnumerator ZoomAndLoad()
    {
        isZooming = true;

        Camera cam = Camera.main;
        float startZoom = cam.orthographicSize;
        float elapsed = 0f;

        while (elapsed < zoomDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / zoomDuration;

            cam.orthographicSize = Mathf.Lerp(startZoom, targetZoom, t);
            cam.transform.position = Vector3.Lerp(
                cam.transform.position,
                new Vector3(transform.position.x, transform.position.y, cam.transform.position.z),
                t
            );

            yield return null;
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}
