using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class UIHoverHighlight : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerClickHandler
{
    private Colony colony;
    private Image image;
    private Color originalColor;

    private TextMeshProUGUI productionName;
    private TextMeshProUGUI turnsLeft;

    private CurrentProductionPanel currentProductionPanel;

    private Color hoverColor = Color.grey;
    private Color selectedColor = Color.grey;
    private Color defaultTextColor = Color.white;

    void Awake()
    {
        image = GetComponent<Image>();
        //if (image == null)
            //Debug.LogError("UIHoverHighlight requires an Image component");

        originalColor = image.color;
    }

    void Start()
    {
        colony = XmlManager.Load();

        // Production name
        Transform label = transform.Find("Label(Clone)");
        if (label != null)
        {
            productionName = label.GetComponent<TextMeshProUGUI>();
            //if (productionName == null)
                //Debug.LogError("Label(Clone) is missing TextMeshProUGUI");
        }

        // Turns left
        Transform value = transform.Find("Value(Clone)");
        if (value != null)
        {
            turnsLeft = value.GetComponent<TextMeshProUGUI>();
            //if (turnsLeft == null)
                //Debug.LogError("Value(Clone) is missing TextMeshProUGUI");
        }

        currentProductionPanel = FindObjectOfType<CurrentProductionPanel>();
        //if (currentProductionPanel == null)
            //Debug.LogError("CurrentProductionPanel not found");

        RefreshSelection();
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

        RefreshSelection();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        colony = XmlManager.Load();

        if (productionName == null || turnsLeft == null)
            return;

        if (productionName.text == colony.selectedProduction)
        {
            colony.selectedProduction = "";
            colony.turnsLeft = 0;

            Sprite loadedSprite = Resources.Load<Sprite>($"imageAssets/SelectProduction");
            GameObject imageTransform = GameObject.Find("Image"); 
            Image image = imageTransform.GetComponent<Image>();
            image.sprite = loadedSprite;
            image.color = Color.cyan;
        }
        else
        {
            colony.selectedProduction = productionName.text;
            colony.turnsLeft = int.Parse(turnsLeft.text);
        }

        XmlManager.Save(colony);
        currentProductionPanel.Refresh();

        // 🔁 Force ALL items to refresh
        foreach (UIHoverHighlight item in FindObjectsOfType<UIHoverHighlight>())
        {
            item.RefreshSelection();
        }
    }

    public void RefreshSelection()
    {
        colony = XmlManager.Load();
        UpdateProductionTextColors();
    }

    private void UpdateProductionTextColors()
    {
        bool isSelected = productionName != null &&
                          productionName.text == colony.selectedProduction;

        if (productionName != null)
            productionName.color = isSelected ? selectedColor : defaultTextColor;

        if (turnsLeft != null)
            turnsLeft.color = isSelected ? selectedColor : defaultTextColor;
    }
}
