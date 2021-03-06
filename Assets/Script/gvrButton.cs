﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class gvrButton : MonoBehaviour
{
    public Image imgCricle;
    public UnityEvent gvrClick;
    public float totalTime = 2;
    bool gvrStatus;
    public float gvrTimer;

    // Update is called once per frame
    void Update()
    {
        if(gvrStatus){
            gvrTimer += Time.deltaTime;
            imgCricle.fillAmount = gvrTimer / totalTime;
        }
        if(gvrTimer > totalTime){
            GvrOff();
            gvrClick.Invoke();
        }
        
    }

    public void GvrOn(){
        gvrStatus = true;
    }

    public void GvrOff(){
        gvrStatus = false;
        gvrTimer = 0;
        imgCricle.fillAmount = 0;
    }
}
