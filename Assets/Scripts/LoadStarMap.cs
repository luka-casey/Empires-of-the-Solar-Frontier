using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadStatMap : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}