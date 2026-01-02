using UnityEngine;
using TMPro;

public class NextTurnButton : MonoBehaviour
{
    public static int CurrentTurn = 1;

    [Header("References")]
    public ShipOrbit ship;
    public TMP_Text turnText;

    void Start()
    {
        UpdateTurnText();
    }

    void OnMouseDown()
    {
        CurrentTurn++;

        Debug.Log($"=== TURN {CurrentTurn} ===");

        UpdateTurnText();
        ship.StartTurn();
    }

    void UpdateTurnText()
    {
        if (turnText != null)
        {
            turnText.text = $"Turn {CurrentTurn}";
        }
    }
}
