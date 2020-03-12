using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckListController : MonoBehaviour
{
    public GameObject AwalContent, InternetMatiContent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        controlCheckList();
    }
    void controlCheckList()
    {
        if(sceneManager.nm_scene_sebelumnya.Contains("Awal"))
        {
            AwalContent.SetActive(true);
        }
        else
        {
            AwalContent.SetActive(false);
        }
        if (sceneManager.nm_scene_sebelumnya.Contains("InternetMati"))
        {
            InternetMatiContent.SetActive(true);
        }
        else
        {
            InternetMatiContent.SetActive(false);
        }
    }
}
