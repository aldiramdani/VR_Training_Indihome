
using UnityEngine;

public class internetChecker : MonoBehaviour
{
    private const bool allowCarrierDataNetwork = false;
    private const string pingAddress = "8.8.8.8";
    private const float waitingTime = 1.0f;
    public bool internetConnectBool;
    private Ping ping;
    private float pingStartTime;
    sceneManager sm = new sceneManager();
    public void Start(){
        internetCheck();
    }

    //Fungsi Check Apakah Tersedia Internet / Tidak
    public void internetCheck(){
        Invoke("InternetCheck",5f);
        bool internetPossibilyAvailable;
        switch(Application.internetReachability){
            case NetworkReachability.ReachableViaLocalAreaNetwork:
                internetPossibilyAvailable = true;
                break;
            case NetworkReachability.ReachableViaCarrierDataNetwork:
                internetPossibilyAvailable = true;
                break;
            default:  
                internetPossibilyAvailable = false;
                break;
        }
        if(!internetPossibilyAvailable){
            internetIsNotAvailable();
            return;
        }
        ping = new Ping(pingAddress);
        pingStartTime = Time.time;
    }

    //Function Memulai Apps dan Check Internet
    public void startApp(){
        PlayerPrefs.SetInt("vrActive",0);
        if(!internetConnectBool){
            SSTools.ShowMessage("Tidak Ada Koneksi Internet!",SSTools.Position.bottom,SSTools.Time.threeSecond);
        }else{
            //Koding Pindah Scene
            sm.changeScene("HomeScene");
        }
    }

    public void Update(){
        checkPing();
    }

    //Fungsi CheckPing
    public void checkPing(){
        if(ping != null){
            bool stopCheck = true;
            if(ping.isDone){
                internetAvailable();
            }
            else if(Time.time - pingStartTime < waitingTime){
                stopCheck = false;
            }
            if(stopCheck){
                ping = null;
            }
        }
    }

    public void internetIsNotAvailable(){
        internetConnectBool = false;
    }

    public void internetAvailable(){
        internetConnectBool = true;
    }

}