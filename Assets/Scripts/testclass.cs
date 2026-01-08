using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SimpleNameModalBuilder : MonoBehaviour
{
    public TMP_FontAsset font;

    GameObject modalRoot;
    TMP_InputField inputField;

    // void Start()
    // {
    //     BuildModal();
    //     modalRoot.SetActive(true);
    // }

    public void Open()
    {
        BuildModal();
        modalRoot.SetActive(true);

        modalRoot.SetActive(true);
        inputField.text = "";
        inputField.ActivateInputField();
    }

    void BuildModal()
    {
        // ===== Canvas =====
        Canvas canvas = FindObjectOfType<Canvas>();
        //if (canvas == null)
        //{
            GameObject canvasGO = new GameObject("Canvas");
            canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasGO.AddComponent<CanvasScaler>();
            canvasGO.AddComponent<GraphicRaycaster>();
        //}

        // ===== Modal Root =====
        modalRoot = new GameObject("SimpleNameModal");
        modalRoot.transform.SetParent(canvas.transform, false);

        // ===== Background =====
        GameObject bg = CreateUIObject("Background", modalRoot.transform);
        Image bgImage = bg.AddComponent<Image>();
        bgImage.color = new Color(0, 0, 0, 0.5f);

        StretchRect(bg);

        // ===== Panel =====
        GameObject panel = CreateUIObject("Panel", modalRoot.transform);
        Image panelImage = panel.AddComponent<Image>();
        panelImage.color = new Color(0.15f, 0.15f, 0.15f, 1f);

        RectTransform panelRT = panel.GetComponent<RectTransform>();
        panelRT.sizeDelta = new Vector2(400, 220);
        panelRT.anchorMin = panelRT.anchorMax = new Vector2(0.5f, 0.5f);
        panelRT.anchoredPosition = Vector2.zero;

        // ===== Question Text =====
        TMP_Text question = CreateTMPText(
            "Enter a name",
            panel.transform,
            new Vector2(0, 60)
        );

        // ===== Input Field =====
        inputField = CreateInputField(panel.transform, new Vector2(0, 10));

        // ===== Submit Button =====
        CreateButton(panel.transform, "Submit", new Vector2(0, -60), Submit);
    }

    void Submit()
    {
        Debug.Log("Entered name: " + inputField.text);
        modalRoot.SetActive(false);

        FindObjectOfType<PlanetColonyManager>().InitializeNewColonyCreation(inputField.text);
    }

    // ---------- Helpers ----------

    GameObject CreateUIObject(string name, Transform parent)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(parent, false);
        go.AddComponent<RectTransform>();
        return go;
    }

    void StretchRect(GameObject go)
    {
        RectTransform rt = go.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = Vector2.zero;
        rt.offsetMax = Vector2.zero;
    }

    TMP_Text CreateTMPText(string text, Transform parent, Vector2 pos)
    {
        GameObject go = CreateUIObject("QuestionText", parent);
        TMP_Text tmp = go.AddComponent<TextMeshProUGUI>();
        tmp.font = font;
        tmp.text = text;
        tmp.fontSize = 24;
        tmp.alignment = TextAlignmentOptions.Center;

        RectTransform rt = go.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(360, 40);
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = pos;

        return tmp;
    }

    TMP_InputField CreateInputField(Transform parent, Vector2 pos)
    {
        GameObject go = CreateUIObject("InputField", parent);
        Image bg = go.AddComponent<Image>();
        bg.color = Color.white;

        TMP_InputField input = go.AddComponent<TMP_InputField>();

        RectTransform rt = go.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(300, 40);
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = pos;

        // Text
        TMP_Text text = CreateTMPChild("Text", go.transform);
        input.textComponent = text;

        // Placeholder
        TMP_Text placeholder = CreateTMPChild("Placeholder", go.transform);
        placeholder.text = "Name...";
        placeholder.color = new Color(0.5f, 0.5f, 0.5f);
        input.placeholder = placeholder;

        return input;
    }

    TMP_Text CreateTMPChild(string name, Transform parent)
    {
        GameObject go = new GameObject(name);
        go.transform.SetParent(parent, false);
        TMP_Text tmp = go.AddComponent<TextMeshProUGUI>();
        tmp.font = font;
        tmp.fontSize = 18;
        tmp.alignment = TextAlignmentOptions.Left;

        RectTransform rt = go.GetComponent<RectTransform>();
        rt.anchorMin = Vector2.zero;
        rt.anchorMax = Vector2.one;
        rt.offsetMin = new Vector2(10, 6);
        rt.offsetMax = new Vector2(-10, -6);

        return tmp;
    }

    void CreateButton(Transform parent, string label, Vector2 pos, UnityEngine.Events.UnityAction action)
    {
        GameObject go = CreateUIObject("Button", parent);
        Image img = go.AddComponent<Image>();
        img.color = new Color(0.2f, 0.6f, 1f);

        Button btn = go.AddComponent<Button>();
        btn.onClick.AddListener(action);

        RectTransform rt = go.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(160, 40);
        rt.anchorMin = rt.anchorMax = new Vector2(0.5f, 0.5f);
        rt.anchoredPosition = pos;

        TMP_Text text = CreateTMPText(label, go.transform, Vector2.zero);
        text.alignment = TextAlignmentOptions.Center;
    }
}
