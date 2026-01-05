using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class testclass : MonoBehaviour
{


    public void LogSomething()
    {
        //Debug.Log("I CLicked!!!");
    }


    void OnMouseDown()
    {
        LogSomething();
    }
}
