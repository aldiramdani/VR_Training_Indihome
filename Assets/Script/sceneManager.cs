using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Linq;
using System;
public class sceneManager : MonoBehaviour
{
    Scene m_sceneName; //Variable Scene
    string currentScene; //Variable dapetin nama scene ubah jd string
    double pnDouble, nDouble; //convert nilai ke double
    public Text debug_txt,txt_status,txt_status_fail; //tampilan text debug dan test
    public static string hasilSpeech; //variable tampung hasil speech dari lib
    static List<Words> word = new List<Words>(); //variable list nampung word.txt
    List<Words> nWord = new List<Words>();
    public GameObject imgCheckmark0,imgCheckmark1, imgCheckmark2, imgCheckmark3, 
        imgCheckmark4, imgCheckmark5, imgCheckmark6, imgCheckmark7, imgCheckmark8, 
        imgCheckmark9; //image check list
    [SerializeField] public GameObject nextSceneCanvas, nextSceneFailCanvas;
    public GameObject fCanvas;
    public static string session_mode;
    public static string nextScene, nm_scene_sebelumnya;
    public string benar_salah, stoDo,skipNextScene;
    public bool isSkipOk;
    sceneControler sc = new sceneControler();
    // Start is called before the first frame update
    void Start()
    {
        hasilSpeech = "";
        nextScene = "";
        benar_salah = "salah";
        m_sceneName = SceneManager.GetActiveScene();
        currentScene = m_sceneName.name;
        if (!currentScene.Contains("Tunggu"))
        {
            nm_scene_sebelumnya = currentScene;
        }
        if (currentScene == "Awal1")
        {
            PlayerPrefs.SetString("nilai", "0");
            resetScore();
        }else if(currentScene == "Akhir1" || currentScene =="Jaringan1")
        {
            resetScore();
        }
        if (currentScene == "HomeScene")
        {
            unLoadWord();
        }
        loadNWord();
        toDoController();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentScene.Contains("Tunggu"))
        {
            testSpeak();
            debug_txt.text = currentScene;
        }
        
    }

    void hideSeekCanvas()
    {
        if (nextSceneCanvas.activeSelf)
        {
            nextSceneFailCanvas.SetActive(false);
        }
    }

    void loadNWord()
    {
        for(int i=0;i < word.Count; i++)
        {
            if (word[i].skenarioTujuan.Contains(sc.newSceneName(nm_scene_sebelumnya)) || word[i].sceneSebelumnya.Contains(nm_scene_sebelumnya))
            {
                nWord.Add(word[i]);
            }
        }
    }

    public void changeScene(string namaScene){
        SceneManager.LoadScene(namaScene);
    }

    void testSpeak(){
        if (currentScene.Contains("Tunggu")){
            if(Input.GetButton("A")){
                SpeakNow.startSpeech(LanguageUtil.INDONESIAN);
            }
            else if (Input.GetButtonDown("B"))
            {
                sceneControl(hasilSpeech);
                hasilSpeech = "";
            }
        }
        hasilSpeech += SpeakNow.speechResult().ToLower().Replace("no match", " ");
    }

    public void sceneControl(string s_Result){
        getScore();
        var i = 0;
        foreach(Words x in nWord)
        {
            if (s_Result.Contains(x.kataKunci.ToString()))
            {
                
                benar_salah = "benar";
                txt_status.text = "Kamu Berhasil !";
                txt_status.color = Color.green;
                if(x.isWajib != "1")
                {
                    benar_salah = "benar";
                    nextScene = x.skenarioTujuan;
                    speechManager(i);
                    break;
                }
                else if(x.isWajib == "1")
                {
                    if (s_Result.Contains(x.kataWajib))
                    {
                        benar_salah = "benar";
                        nextScene = nWord[i].skenarioTujuan;
                        speechManager(i);
                        break;
                    }
                    else if (!s_Result.Contains(x.kataWajib))
                    {
                        benar_salah = "benar";
                        failDialogBoxMode();
                        break;
                    }

                }
            }
            else if (benar_salah == "salah" && !s_Result.Contains(x.kataKunci.ToString()) && s_Result != "")
            {
                s_Result = "";
                //fCanvas.SetActive(true);
                hasilSpeech = "";
                failDialogBoxMode();
            }
            i++;
        }
    }


    private void speechManager(int pos){
        //SceneManager.LoadScene(word[pos].skenarioTujuan);
        stoDo = nWord[pos].toDo;
        nDouble = nWord[pos].nilai;
        nextScene = nWord[pos].skenarioTujuan;
        dialogBoxMode(nWord[pos].kataSaran,nWord[pos].skenarioTujuan);
        SpeakNow.reset();
        hasilSpeech = "";
    }

    public void inserToTodo()
    {
        pnDouble = double.Parse(PlayerPrefs.GetString("nilai"));
        double nilai = nDouble + pnDouble;
        PlayerPrefs.SetInt(stoDo,1);
        PlayerPrefs.SetString("nilai", nilai.ToString());
    }


    public string failKata(string namaScene)
    {
        string kataFail="Kosong Keneh Cuy";
        foreach(Words x in nWord)
        {
            if (x.skenarioTujuan.Contains(namaScene))
            {
                kataFail = x.kataSaran;
                return x.kataSaran;
            }
        }
        return kataFail;
    }

    public void failDelete()
    {
        for (int i = 0; i < nWord.Count; i++)
        {
            nWord.Find(x => x.skenarioTujuan.Contains(nextScene));
            nWord.Remove(new Words { skenarioTujuan = nextScene });
        }
    }

    void dialogBoxMode(string kataKunci,string namaSceneSelanjutnya)
    {
        nextSceneCanvas.SetActive(true);
        StartCoroutine(HideObject(namaSceneSelanjutnya));
        inserToTodo();
    }
    IEnumerator HideObject(string nextScene)
    {
        yield return new WaitForSeconds(3);
        nextSceneCanvas.SetActive(false);
        nextSceneFailCanvas.SetActive(false);
        SceneManager.LoadScene(nextScene);
    }

    

   public void failDialogBoxMode()
    {
        txt_status_fail.text = "Kamu Salah Mengucapkan Kata!, Sebaik nya kamu mengucapkan";
        txt_status_fail.color = Color.red;
        hasilSpeech = ""; 
        SpeakNow.reset();
        if (isSkipOk)
        {
            nextScene = skipNextScene;
        }
        else
        {
            nextScene = sc.newSceneName(nm_scene_sebelumnya);
        }

        nextSceneFailCanvas.SetActive(true);
        StartCoroutine(HideObject(nextScene));
    }


    public void f_nextScene()
    {
        failDelete();
        SceneManager.LoadScene(nextScene);
    }


    public void loadKeyWord(string nameFile){
        try{
            string path= "jar:file://" + Application.dataPath + "!/assets/"+nameFile;
            WWW wwfile = new WWW(path);
            while(!wwfile.isDone){}
            var filepath = string.Format("{0}/{1}",Application.persistentDataPath,nameFile);
            File.WriteAllBytes(filepath,wwfile.bytes); 
            
            string[] lines = File.ReadAllLines(filepath);

            foreach(string line in lines){
                if(line.StartsWith("--")|| line == String.Empty)continue;
                
                var parts = line.Split(new char[] {'|'});
                word.Add(new Words(){
                    kataKunci = parts[0],
                    isWajib = parts[1],
                    kataWajib = parts[2],
                    nilai = double.Parse(parts[3]),
                    skenarioTujuan = parts[4],
                    toDo = parts[5],
                    kataSaran = parts[6],
                    sceneSebelumnya = parts[7]
                });
            }
        }catch(Exception ex)
        {
            throw ex;
        }
    }

    public void resetScore()
    {
        PlayerPrefs.SetInt("todo1", 0);
        PlayerPrefs.SetInt("todo2", 0);
        PlayerPrefs.SetInt("todo3", 0);
        PlayerPrefs.SetInt("todo4", 0);
        PlayerPrefs.SetInt("todo5", 0);
        PlayerPrefs.SetInt("todo6", 0);
    }
    
    public void addDb()
    {
        DBTest dBTest = new DBTest();
        dBTest.addtoDB();
    }
    public void unLoadWord()
    {
        word.Clear();
        nWord.Clear();
    }
    public void getScore()
    {
        PlayerPrefs.GetInt("todo1");
        PlayerPrefs.GetInt("todo2");
        PlayerPrefs.GetInt("todo3");
        PlayerPrefs.GetInt("todo4");
        PlayerPrefs.GetInt("todo5");
        PlayerPrefs.GetInt("todo6");
        PlayerPrefs.GetInt("nilai");
    }

    public void toDoController()
    {

        if (PlayerPrefs.GetInt("todo1") != 1)
        {
            imgCheckmark0.SetActive(false);
        }
        else
        {
            imgCheckmark0.SetActive(true);
        }
        if (PlayerPrefs.GetInt("todo2") != 1)
        {
            imgCheckmark1.SetActive(false);
        }
        else
        {
            imgCheckmark1.SetActive(true);
        }
        if (PlayerPrefs.GetInt("todo3") != 1)
        {
            imgCheckmark2.SetActive(false);
        }
        else
        {
            imgCheckmark2.SetActive(true);
        }
        if (PlayerPrefs.GetInt("todo4") != 1)
        {
            imgCheckmark3.SetActive(false);
        }
        else
        {
            imgCheckmark3.SetActive(true);
        }
        if (PlayerPrefs.GetInt("todo5") != 1)
        {
            imgCheckmark4.SetActive(false);
        }
        else
        {
            imgCheckmark4.SetActive(true);
        }
        if (PlayerPrefs.GetInt("todo6") != 1)
        {
            imgCheckmark5.SetActive(false);
        }
        else
        {
            imgCheckmark5.SetActive(true);
        }
        if (PlayerPrefs.GetInt("todo7") != 1)
        {
            imgCheckmark6.SetActive(false);
        }
        else
        {
            imgCheckmark6.SetActive(true);
        }
        if (PlayerPrefs.GetInt("todo8") != 1)
        {
            imgCheckmark7.SetActive(false);
        }
        else
        {
            imgCheckmark7.SetActive(true);
        }
        if (PlayerPrefs.GetInt("todo9") != 1)
        {
            imgCheckmark8.SetActive(false);
        }
        else
        {
            imgCheckmark8.SetActive(true);
        }
        if (PlayerPrefs.GetInt("todo10") != 1)
        {
            imgCheckmark9.SetActive(false);
        }
        else
        {
            imgCheckmark9.SetActive(true);
        }
    }
}
