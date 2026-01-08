using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Colony
{
    public int populationBaseValue; 
    public int incomeBaseValue; 
    public int expensesBaseValue; 
    public int productionBaseValue; 
    public int scienceBaseValue; 

    public int populationTotal; 
    public int incomeTotal; 
    public int expensesTotal; 
    public int productionTotal; 
    public int scienceTotal; 

    public string history;
    public List<Production>? finishedProductions; 
    public List<Production>? productions; 
    public string selectedProduction; 
    public int turnsLeft;

    public Colony()
    {
    }

    public static Colony CreateNewColonyObject()
    {
        return new Colony()
        {
            populationBaseValue = 3,
            incomeBaseValue = 3,
            expensesBaseValue = 3,
            productionBaseValue = 3,
            scienceBaseValue = 3,
            turnsLeft = -33 //specialNumber for logic
        };
    }
}