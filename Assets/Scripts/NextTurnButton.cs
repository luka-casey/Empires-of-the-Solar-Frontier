using UnityEngine;
using TMPro;
using System;
using System.Data.Common;
using System.Linq;
using System.Collections.Generic;

public class NextTurnButton : MonoBehaviour
{
    public static int CurrentTurn;

    [Header("References")]
    public ShipOrbit ship;
    public TMP_Text turnText;

    void OnMouseDown()
    {
        CurrentTurn++;

        List<string> colonyNames = XmlManager.LoadPlanetColonyNames("Earth").colonyNames;

        foreach (string name in colonyNames)
        {
            string fileName = $"{name}.xml";
            UpdateProductionsTurns(fileName);
        }

        UpdateTurnText();
        ship.StartTurn();
    }

    public static void UpdateProductionsTurns(string colonyName)
    {
        Colony colony = XmlManager.Load(colonyName);

        if (colony.turnsLeft > -1 && !string.IsNullOrEmpty(colony.selectedProduction))
        {
            var x = colony.productions.Where(p => p.productionName == colony.selectedProduction).First();
            colony.turnsLeft = int.Parse(x.turns);
        }

        if (colony.productions != null && colony.productions.Count > 0 )
        {
            //Hard Coded for now, this allows for turns to be calcualted on turn 1, but not further calculated after something is finished (bug)
            if (colony.productions[0].turns == "100" || colony.selectedProduction != "")
            {
                UpdateProductionTurns(colony);
            }
        }

        if (colony.turnsLeft > -1 && !string.IsNullOrEmpty(colony.selectedProduction))
        {
            var x = colony.productions.Where(p => p.productionName == colony.selectedProduction).First();
            colony.turnsLeft = int.Parse(x.turns);
        }

        // This block is neccecary to update the Production Panels turns on the turn that a production is finished building.
        // It re-applies the yield of the newly finished building so its reflected in the Production Panels turns
        if(colony.turnsLeft == 0)
        {
            var productionFinishedThisTurn = colony.productions.Where(p => p.productionName == colony.selectedProduction).FirstOrDefault();

            if(productionFinishedThisTurn is not null)
            {
                colony.finishedProductions.Add(productionFinishedThisTurn);
                UpdateProductionTurns(colony);
            }
        }

        XmlManager.Save(colony, colonyName);
    }

    public static void UpdateProductionTurns(Colony colony)
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
                var currentWorkingProduction = colony.productionTotal + colony.productionBaseValue;
                int producedSoFar = production.productionMeter;
                int remainingProduction = production.requiredProduction - producedSoFar;

                production.turns = Mathf.CeilToInt((float)remainingProduction / currentWorkingProduction).ToString();
            }
        }
    }

    void UpdateTurnText()
    {
        if (turnText != null)
        {
            turnText.text = $"Turn {CurrentTurn}";
        }
    }
}
