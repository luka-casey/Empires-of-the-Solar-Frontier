using UnityEngine;

public class PlanetColonyManager : MonoBehaviour
{
    public GameObject colonyIconPrefab;
    public Transform colonyIconSpawnPoint;

    public Colony colony;
    public bool hasColony = false;

    public void EstablishColony()
    {
        //TODO: Only allows one colony per planet for now
        //Will change later
        if (hasColony) return;

        colony = new Colony();
        hasColony = true;

        Instantiate(colonyIconPrefab, colonyIconSpawnPoint.position, Quaternion.identity, transform);

        Debug.Log("Colony established!");
        Debug.Log($"Population: {colony.population}");
    }
}
