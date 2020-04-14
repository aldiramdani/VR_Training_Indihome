using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckListPasif : MonoBehaviour
{
    [SerializeField]public GameObject checkListObj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!sceneManager.nm_scene_sebelumnya.Contains("Awal1")){
            startHideCount();
        }
    }

    IEnumerator HideObject()
    {
        yield return new WaitForSeconds(3);
        checkListObj.SetActive(false);
    }

    public void startHideCount()
    {
        StartCoroutine(HideObject());
    }
}
