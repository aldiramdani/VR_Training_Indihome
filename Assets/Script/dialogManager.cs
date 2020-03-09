using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class dialogManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject transistionDialog,fail_transtitionDialog;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Self" + transistionDialog.activeSelf + fail_transtitionDialog.activeSelf);
        if (transistionDialog.activeSelf==true || fail_transtitionDialog.activeSelf==true)
        {
            videoPlayer.Stop();
        }
    }
}
