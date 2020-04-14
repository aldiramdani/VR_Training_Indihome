using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTodo : MonoBehaviour
{
    public string namaTxt;
    private void Start()
    {
        sceneManager sm = new sceneManager();
        PlayerPrefs.SetInt("todo1", 0);
        PlayerPrefs.SetInt("todo2", 0);
        PlayerPrefs.SetInt("todo3", 0);
        PlayerPrefs.SetInt("todo4", 0);
        sm.loadKeyWord(namaTxt);
        sm.resetScore();
    }
}
