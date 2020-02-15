using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scroing : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject txtScore;
    void Start()
    {
        txtScore.GetComponent<Text>().text =  PlayerPrefs.GetString("nilai");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
