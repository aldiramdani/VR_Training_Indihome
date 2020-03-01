﻿using System.Collections;
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
    double pnDouble; //convert nilai ke double
    public Text test_txt,debug_txt,txt_KataSaran,txt_status; //tampilan text debug dan test
    string hasilSpeech; //variable tampung hasil speech dari lib
    static List<Words> word = new List<Words>(); //variable list nampung word.txt
    public GameObject imgCheckmark0,imgCheckmark1, imgCheckmark2, imgCheckmark3, 
        imgCheckmark4, imgCheckmark5, imgCheckmark6, imgCheckmark7, imgCheckmark8, 
        imgCheckmark9; //image check list
    public GameObject fCanvas,nextSceneCanvas,btn_restart_transition;
    public static string session_mode;
    public static string nextScene;
    private string benar_salah;
    // Start is called before the first frame update
    void Start()
    {
        hasilSpeech = " ";
        nextScene = "";
        benar_salah = "";
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
        test_txt.text = benar_salah;
        /* foreach (var x in word)
         {
             test_txt.text += x.kataKunci;
         }*/
        testSpeak();
        if (nextSceneCanvas.activeSelf)
        {
            fCanvas.SetActive(false);
        }
        debug_txt.text = hasilSpeech;
    }

    public void changeScene(string namaScene){
        SceneManager.LoadScene(namaScene);
    }

    void testSpeak(){
        if (currentScene != "FirstScene" || currentScene != "BantuanScene"){
            if(Input.GetButton("A")){
                SpeakNow.startSpeech(LanguageUtil.INDONESIAN);
            }
            else if (Input.GetButtonDown("B"))
            {
                sceneControl(hasilSpeech);
            }
        }
        hasilSpeech += SpeakNow.speechResult().ToLower().Replace("no match", " ");
    }

    public void sceneControl(string s_Result){
        getScore();
        for(int i = 0;i < word.Count;i++){
            if (s_Result.Contains(word[i].kataKunci.ToString())){
                benar_salah = "benar";
                txt_status.text = "Kamu Berhasil !, Sebaik nya kamu mengucapkan";
                txt_status.color = Color.green;
                if (word[i].isWajib != "1"){
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
            benar_salah = "salah";
            if(benar_salah == "salah")
            {
                fCanvas.SetActive(true);
                hasilSpeech = "";
                /* txt_status.text = "Kamu Gagal !, Sebaik nya kamu mengucapkan";
                 txt_status.color = Color.red;
                 dialogBoxMode("Kamu Gagal !, Sebaik nya kamu mengucapkan");*/
            }
/*            if (!s_Result.Contains(word[i].kataKunci.ToString()))
            {
                
            }*/
        }
    }


    private void speechManager(int pos){
        //SceneManager.LoadScene(word[pos].skenarioTujuan);
        dialogBoxMode(word[pos].kataSaran);
        string sTujuan = word[pos].skenarioTujuan;
        string stoDo = word[pos].toDo;
        double nDouble = word[pos].nilai;
        nextScene = word[pos].skenarioTujuan;
        for (int i=0;i<30;i++){
            word.Find(x => x.kataKunci.Contains(sTujuan));
            word.Remove(new Words{skenarioTujuan = sTujuan});
        }
        pnDouble = double.Parse(PlayerPrefs.GetString("nilai"));  
        double nilai = nDouble + pnDouble;
        PlayerPrefs.SetInt(stoDo,1);
        PlayerPrefs.SetString("nilai", nilai.ToString());
        SpeakNow.reset();
        hasilSpeech = "";
        sTujuan="";
    }

    void dialogBoxMode(string kataKunci)
    {
        txt_KataSaran.text = kataKunci;
        if(session_mode == "evaluasi")
        {
           btn_restart_transition.SetActive(false);
           nextSceneCanvas.SetActive(true);
        }
        nextSceneCanvas.SetActive(true);
    }

    public void f_nextScene()
    {
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
                    kataSaran = parts[6]
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
