using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SolarPlanet
{
    public string planetName;
    public List<string> colonyNames;
    

    public SolarPlanet()
    {
    }

    public SolarPlanet(string PlanetName, List<string> ColonyNames)
    {
        this.planetName = PlanetName;
        this.colonyNames = ColonyNames;
    }
}
