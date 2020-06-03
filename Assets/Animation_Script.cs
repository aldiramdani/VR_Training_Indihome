﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_Script : MonoBehaviour
{
    public Animator anim;
    sceneManager sc = new sceneManager();
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Input.GetButton("A")
        if (Input.GetKeyDown("space"))
        {
            anim.Play("newTabletAnimation");
           
        }
    }

    public void changeScene(string namaScene)
    {
        sc.changeScene(namaScene);
    }
}
