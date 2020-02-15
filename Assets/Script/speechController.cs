using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class speechController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void sceneControl(string s_Result){
        if(s_Result.Contains("h") || s_Result.Contains("H")){
            SceneManager.LoadScene("HomeScene");
        }
    }
}
