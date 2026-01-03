using UnityEngine;
using TMPro;

public class NextTurnButton : MonoBehaviour
{
    public static int CurrentTurn;

    [Header("References")]
    public ShipOrbit ship;
    public TMP_Text turnText;

    void Start()
    {
        //Turn turnObject = TurnXmlManager.Load();
        //CurrentTurn = turnObject.turn;
        //UpdateTurnText();
    }

    void OnMouseDown()
    {
        //var turnObject = TurnXmlManager.Load();

        //CurrentTurn = turnObject.turn;

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
