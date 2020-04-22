using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipScene : MonoBehaviour
{
    public string sceneTujuanSkip;
    sceneControler sc = new sceneControler();
    // Start is called before the first frame update
    void Start()
    {
        sc.changeScene(sceneTujuanSkip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
