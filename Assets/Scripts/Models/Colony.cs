using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Colony
{
    public int population; 
    public int income; 
    public int expenses; 
    public int production; 
    public int science; 
    public string history;
    public List<Building>? buildings; 
    public List<Production>? productions; 
    public string selectedProduction; 
    public int turnsLeft;

    public Colony()
    {
    }
}