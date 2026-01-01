using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIHoverHighlight : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Colony colony;
    private Image image;
    private Color originalColor;
    private TextMeshProUGUI productionName;
    private TextMeshProUGUI turnsLeft;
    private CurrentProductionPanel productionPanel;
    [SerializeField] private Color hoverColor = Color.cyan;

    void Awake()
    {
        image = GetComponent<Image>();
        if (image == null)
            Debug.LogError("UIHoverHighlight requires an Image component");

        originalColor = image.color;
    }

    void Start()
    {
        colony = XmlManager.Load();

        Transform label = transform.Find("Label(Clone)");
        if (label != null)
        {
            productionName = label.GetComponent<TextMeshProUGUI>();

            if (productionName == null)
                Debug.LogError("No TextMeshProUGUI child named 'Label(Clone)' found under " + gameObject.name);
        }

        Transform value = transform.Find("Value(Clone)");
        if (value != null)
        {
            turnsLeft = value.GetComponent<TextMeshProUGUI>();

            if (turnsLeft == null)
                Debug.LogError("No TextMeshProUGUI child named 'TurnsLeft(Clone)' found under " + gameObject.name);
        }

        productionPanel = FindObjectOfType<CurrentProductionPanel>();

        if (productionPanel == null)
            Debug.LogError("CurrentProductionPanel not found in scene");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (image != null)
            image.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (image != null)
            image.color = originalColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        colony.selectedProduction = productionName.text;
        colony.turnsLeft = int.Parse(turnsLeft.text);

        XmlManager.Save(colony);
        productionPanel.Refresh();
    }
}