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

        Colony colony = XmlManager.Load();
        colony.turnsLeft = colony.turnsLeft - 1;
        XmlManager.Save(colony);

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
