using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class imageScript : MonoBehaviour
{
    public Image hideImages;
    public string nextScene;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HideObject(nextScene));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator HideObject(string nextScene)
    {
        yield return new WaitForSeconds(8);
        hideImages.enabled = false;
        SceneManager.LoadScene(nextScene);
    }

}
