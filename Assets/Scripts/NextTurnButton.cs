using UnityEngine;
using TMPro;
using System;
using System.Data.Common;
using System.Linq;

public class NextTurnButton : MonoBehaviour
{
    public static int CurrentTurn;

    [Header("References")]
    public ShipOrbit ship;
    public TMP_Text turnText;

    void OnMouseDown()
    {
        CurrentTurn++;

        UpdateProductionsTurns();

        UpdateTurnText();
        ship.StartTurn();
    }

    public static void UpdateProductionsTurns()
    {
        Colony colony = XmlManager.Load();

        if (colony.turnsLeft > -1 && !string.IsNullOrEmpty(colony.selectedProduction))
        {
            var x = colony.productions.Where(p => p.productionName == colony.selectedProduction).First();
            colony.turnsLeft = int.Parse(x.turns);
        }

        if (colony.productions != null && colony.productions.Count > 0)
        {
            foreach (Production production in colony.productions)
            {
                ColonyProductionsPanel.UpdateProductionMeter(colony, production);

                int productionPerTurn = colony.productionTotal + colony.productionBaseValue;

                if (productionPerTurn > production.requiredProduction)
                {
                    production.turns = "0";
                    colony.turnsLeft = 0;
                }
                else
                {
                    var currentWorkingProduction = colony.productionTotal + colony.productionBaseValue; //production.productionMeter == 0 ? colony.productionTotal : production.productionMeter;
                    int producedSoFar = production.productionMeter;
                    int remainingProduction = production.requiredProduction - producedSoFar;

                    production.turns = Mathf.CeilToInt((float)remainingProduction / currentWorkingProduction).ToString();
                }
            }
        }

        if (colony.turnsLeft > -1 && !string.IsNullOrEmpty(colony.selectedProduction))
        {
            var x = colony.productions.Where(p => p.productionName == colony.selectedProduction).First();
            colony.turnsLeft = int.Parse(x.turns);
        }

        //XmlManager.Save(colony);

        // This block is neccecary to update the Production Panels turns on the turn that a production is finished building.
        // It re-applies the yield of the newly finished building so its reflected in the Production Panels turns
        if(colony.turnsLeft == 0)
        {
            var productionFinishedThisTurn = colony.productions.Where(p => p.productionName == colony.selectedProduction).First();
            colony.finishedProductions.Add(productionFinishedThisTurn);

            foreach (Production production in colony.productions)
            {
                ColonyProductionsPanel.UpdateProductionMeter(colony, production);

                int productionPerTurn = colony.productionTotal + colony.productionBaseValue;

                if (productionPerTurn > production.requiredProduction)
                {
                    production.turns = "0";
                    colony.turnsLeft = 0;
                }
                else
                {
                    var currentWorkingProduction = colony.productionTotal + colony.productionBaseValue; //production.productionMeter == 0 ? colony.productionTotal : production.productionMeter;
                    int producedSoFar = production.productionMeter;
                    int remainingProduction = production.requiredProduction - producedSoFar;

                    production.turns = Mathf.CeilToInt((float)remainingProduction / currentWorkingProduction).ToString();
                }
            }
        }

        XmlManager.Save(colony);

        //Debug.Log($"=== TURN {CurrentTurn} ===");
    }

    void UpdateTurnText()
    {
        if (turnText != null)
        {
            turnText.text = $"Turn {CurrentTurn}";
        }
    }
}
