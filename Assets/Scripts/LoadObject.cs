using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadObject : MonoBehaviour
{
    void Start()
    {
        LoadStarMapLocations x = new LoadStarMapLocations();

        x.LoadStarMapDataToScene();
    }
}