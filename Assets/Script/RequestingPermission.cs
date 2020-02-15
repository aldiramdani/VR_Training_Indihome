﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
public class RequestingPermission : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject dialog = null;
    void Start()
    {
        #if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
            dialog = new GameObject();
            }
        #endif
    }

        void OnGUI ()
    {
        #if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            // The user denied permission to use the microphone.
            // Display a message explaining why you need it with Yes/No buttons.
            // If the user says yes then present the request again
            // Display a dialog here.
            return;
        }
        else if (dialog != null)
        {
            Destroy(dialog);
        }
        #endif

        // Now you can do things with the microphone
    }
}
