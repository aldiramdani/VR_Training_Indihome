using System.Collections;
using UnityEngine;
using UnityEngine.XR;
public class vrActivator : MonoBehaviour
{
    bool vrStatus;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("vrActive") != 1){
            StartCoroutine(activatorVR("Cardboard"));
        }
    }
    public IEnumerator activatorVR(string YESVR){
        XRSettings.LoadDeviceByName(YESVR);
        yield return null;
        XRSettings.enabled = true;
        PlayerPrefs.SetInt("vrActive",1);
    }
    
}
