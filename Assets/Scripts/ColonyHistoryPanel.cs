using UnityEngine;
using TMPro;

public class ColonyHistoryPanel : MonoBehaviour
{
    private Colony colony = new Colony();

    [Header("Value Texts")]
    public TextMeshProUGUI historyValue;

    void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        historyValue.text = colony.history.ToString();
    }
}
