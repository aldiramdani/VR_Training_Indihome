using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class NoSkipVideo : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    Scene m_sceneName;
    string currentScene;
    public double time;
    public double currentTime;
    public string nextScene;
    sceneManager sc = new sceneManager();
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
        currentTime = videoPlayer.time;
        if (currentTime >= time - 0.3)
        {
            sc.changeScene(nextScene);
        }
    }
}
