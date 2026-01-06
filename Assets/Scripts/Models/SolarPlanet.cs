using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SolarPlanet
{
    public string planetName;
    public List<Colony>? planetColonies;
    

    public SolarPlanet()
    {
    }

    public SolarPlanet(string PlanetName, List<Colony>? PlanetColonies)
    {
        this.planetName = PlanetName;
        this.planetColonies = PlanetColonies;
    }
}
