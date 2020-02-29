using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class dialogTransition : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    // Start is called before the first frame update
    public GameObject nextSceneCanvas;
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Self Active" + nextSceneCanvas.activeInHierarchy);
        if (nextSceneCanvas.activeSelf)
        {
            videoPlayer.Stop();
        }
    }
}
