using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class videoManager : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    Button btn_play;
    Scene m_sceneName;
    string currentScene;
    public double time;
    public float timeShow;
    public double currentTime;
    int tutorialStatus;
    public GameObject gOCanvas,cTutorial;
    // Start is called before the first frame update
    void Start()
    {
        m_sceneName = SceneManager.GetActiveScene();
        currentScene = m_sceneName.name;
        time = gameObject.GetComponent<VideoPlayer>().clip.length;
        videoPlayer = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        skenarioTunggu(currentScene);
        if(currentTime > time - 0.1)
        {
            gOCanvas.SetActive(true);
        }
    }


    public void Play(){
        videoPlayer.Play();
    }
     public void Pause()
    {
        videoPlayer.Pause();
    }

    public void Stop()
    {
        videoPlayer.Stop();
    }

    private void skenarioTunggu(string sceneSekarang)
    {
        currentTime = videoPlayer.time;
        double timeChange = time - 0.1;
        if(currentTime+1 >= timeChange && sceneSekarang != "SceneTunggu" && sceneSekarang != "TestScene4")
        {
            SceneManager.LoadScene("SceneTunggu");
        }
    }

    private void VideoPlayer_prepareCompleted(VideoPlayer source)
    {
        Play();
    }
}
