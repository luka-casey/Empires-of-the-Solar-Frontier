using UnityEngine;
using System.Collections;

public class ShipPlanetView : MonoBehaviour
{
    [SerializeField] private GameObject earth;

    private Coroutine earthRoutine;
    private CoroutineRunner runner;

    private readonly Vector3 openPosition = new Vector3(5.25f, -2.75f, 0f);
    private readonly Vector3 openScale = new Vector3(0.21f, 0.21f, 1f);

    private readonly Vector3 closedPosition = Vector3.zero;
    private readonly Vector3 closedScale = new Vector3(0.61f, 0.6f, 1f);

    public float transitionDuration = 0.6f;

    void Awake()
    {
        if (earth == null)
            earth = GameObject.Find("Earth");

        runner = earth.GetComponent<CoroutineRunner>();
        if (runner == null)
            runner = earth.AddComponent<CoroutineRunner>();
    }

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void Open()
    {
        bool opening = !gameObject.activeSelf;

        Vector3 targetPos = opening ? openPosition : closedPosition;
        Vector3 targetScale = opening ? openScale : closedScale;

        // Stop previous animation safely
        if (earthRoutine != null)
            runner.StopCoroutine(earthRoutine);

        // Start coroutine on EARTH (always active)
        earthRoutine = runner.StartCoroutine(
            SmoothEarthTransform(
                earth.transform,
                targetPos,
                targetScale,
                transitionDuration
            )
        );

        // Toggle panel AFTER starting animation
        gameObject.SetActive(opening);
    }

    IEnumerator SmoothEarthTransform(
        Transform target,
        Vector3 targetPos,
        Vector3 targetScale,
        float duration
    )
    {
        Vector3 startPos = target.position;
        Vector3 startScale = target.localScale;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            t = Mathf.SmoothStep(0f, 1f, t);

            target.position = Vector3.Lerp(startPos, targetPos, t);
            target.localScale = Vector3.Lerp(startScale, targetScale, t);

            yield return null;
        }

        target.position = targetPos;
        target.localScale = targetScale;
    }
}
