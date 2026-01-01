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
        colony.history = "By the late 22nd century, Earth was poised on the brink of a new frontier. The Western Alliance, led by Europe and the Americas, and the Eastern Compact, guided by China and its Asian partners, had spent decades preparing fleets, orbital stations, and interplanetary infrastructure. Humanity was about to enter its own Age of Discovery, a solar exploration reminiscent of the 14th-century voyages that opened the Americas—a race to claim new worlds, settle distant lands, and stake the first flags beyond Earth. The stars awaited, and the age of solar exploration was about to dawn.";
        
        historyValue.text = colony.history.ToString();
    }
}
