using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneControler : MonoBehaviour
{
    string currentSceneName;
    Scene m_sceneName;
    // Start is called before the first frame update
    
    private void Start() {
        
    }

    public void changeScene(string namaScene){
        SceneManager.LoadScene(namaScene);
    }

    public void statChangeScene(){
        m_sceneName = SceneManager.GetActiveScene();
        currentSceneName = m_sceneName.name;
        SceneManager.LoadScene(currentSceneName); //Ganti Nanti
    }

    public string sceneName()
    {
        m_sceneName = SceneManager.GetActiveScene();
        currentSceneName = m_sceneName.name;
        return currentSceneName;
    }
}
