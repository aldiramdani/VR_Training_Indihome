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
    Scene m_sceneName;
    string currentScene;
    double pnDouble;
    public Text test_txt,debug_txt;
    public Button btn_startSpeak;
    public string hasilSpeech;
    static string testNama;
    static Boolean isStart;
    static List<Words> word = new List<Words>();
    public GameObject imgCheckmark0,imgCheckmark1, imgCheckmark2, imgCheckmark3, 
        imgCheckmark4, imgCheckmark5, imgCheckmark6, imgCheckmark7, imgCheckmark8, 
        imgCheckmark9;
    public GameObject fCanvas;
    // Start is called before the first frame update
    void Start()
    {
        testNama = PlayerPrefs.GetString("Nama" + Profilling.session_nik);
        isStart = false;
        hasilSpeech = " ";
        m_sceneName = SceneManager.GetActiveScene();
        currentScene = m_sceneName.name;
        if (currentScene == "TestScene")
        {
            PlayerPrefs.SetString("nilai", "0");
            resetScore();

        }
        if (currentScene == "HomeScene")
        {
            unLoadWord();
        }
        toDoController();
        
    }

    // Update is called once per frame
    void Update()
    {
        //string dbg_text = PlayerPrefs.GetString("Nama"+session_nik);
        Debug.Log(testNama);
        test_txt.text ="";            
        foreach (var x in word)
        {
            test_txt.text += x.kataKunci;
        }
        testSpeak();
        debug_txt.text = hasilSpeech;
    }



    public void changeScene(string namaScene){
        SceneManager.LoadScene(namaScene);
    }

    void testSpeak(){
        /*            if (Input.GetButtonDown("B"))
            {*/

        //SpeakNow.reset();
        // }
        //hasilSpeech = SpeakNow.speechResult().ToLower().Replace("no match", " ");
        //if (isStart) { 
            if (currentScene != "FirstScene" || currentScene != "BantuanScene"){
                if(Input.GetButton("A")){
                    SpeakNow.startSpeech(LanguageUtil.INDONESIAN);
                }
                else if (Input.GetButtonDown("B"))
                {
                    sceneControl(hasilSpeech);
                }
            }
       // }
        hasilSpeech += SpeakNow.speechResult().ToLower().Replace("no match", " ");
    }

    public void sceneControl(string s_Result){
        getScore();
        for(int i = 0;i < word.Count;i++){
            if(s_Result.Contains(word[i].kataKunci)){
                if(word[i].isWajib != "1"){
                        speechManager(i);
                }
                else if(word[i].isWajib =="1"){
                    if(s_Result.Contains(word[i].kataWajib)){
                        speechManager(i);
                    }else{
                        fCanvas.SetActive(true); 
                    }
                }
            }
        }
//        hasilSpeech = "";
//       SpeakNow.reset();
    }

    private void speechManager(int pos){
        SceneManager.LoadScene(word[pos].skenarioTujuan);
        string sTujuan = word[pos].skenarioTujuan;
        string stoDo = word[pos].toDo;
        double nDouble = word[pos].nilai;
        for (int i=0;i<30;i++){
            word.Remove(new Words{skenarioTujuan = sTujuan});
        }
        pnDouble = double.Parse(PlayerPrefs.GetString("nilai"));
        double nilai = nDouble + pnDouble;
        PlayerPrefs.SetInt(stoDo,1);
        PlayerPrefs.SetString("nilai", nilai.ToString());
        hasilSpeech = "";
        sTujuan="";
        SpeakNow.reset();
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
                    toDo = parts[5]
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
        PlayerPrefs.SetString("nilai", "0");
    }
    
    public void addDb()
    {
        DBTest dBTest = new DBTest();
        dBTest.addtoDB();
    }

    public void mulai()
    {
        isStart = true;
    }

    public void unLoadWord()
    {
        word.Clear();
    }
    public void getScore()
    {
        PlayerPrefs.GetInt("todo1");
        PlayerPrefs.GetInt("todo2");
        PlayerPrefs.GetInt("todo3");
        PlayerPrefs.GetInt("todo4");
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
