using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine.Video;
public class sceneControler : MonoBehaviour
{
    string currentSceneName;
    Scene m_sceneName;
    // Start is called before the first frame update

    public string newSceneName(string oldSceneName)
    {
        Regex re = new Regex(@"([a-zA-Z]+)(\d+)");
        Match result = re.Match(oldSceneName);
        string alphaPart = result.Groups[1].Value;
        int numberPart = int.Parse(result.Groups[2].Value);
        int nn_numberPart = numberPart + 1;
        string newNamaScene = alphaPart + nn_numberPart;
        return newNamaScene;
    }

    public void changeScene(string namaScene){
        SceneManager.LoadScene(namaScene);
    }

    public void statChangeScene(){
        sceneManager sm = new sceneManager();
        sm.unLoadWord();
        sm.loadKeyWord("word.txt");
        SceneManager.LoadScene("Awal1"); //Ganti Nanti
    }

    public string sceneName()
    {
        m_sceneName = SceneManager.GetActiveScene();
        currentSceneName = m_sceneName.name;
        return currentSceneName;
    }
}
