using DataBank;
using GoogleSheetsForUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Profilling : MonoBehaviour
{
    // Start is called before the first frame update
    public Text welcome_text;
    public Button btn_mulai;
    public InputField inputNama, inputNik;
    public Dropdown drpTempatKerja;
    string nama, nik, lok_kerja;
    string _table_name = "Data Pengguna VR Training";
    sceneControler sc = new sceneControler();
    public static string session_nik;



    public struct UserInfo {
        public string nik;
        public string nama;
        public string lokasi;
        public string nilai;
        public string mode;
    }

    private UserInfo userInfo = new UserInfo
    {
        nik = session_nik,
        nama = PlayerPrefs.GetString("Nama" + session_nik),
        lokasi = PlayerPrefs.GetString("Lok Kerja" + session_nik),
        nilai = PlayerPrefs.GetString("nilai"),
        mode = sceneManager.session_mode
    };


    void Start()
    {
        setProfillingtoScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getProfilling()
    {
        nama = inputNama.text;
        nik = inputNik.text;
        lok_kerja = drpTempatKerja.itemText.text;
        session_nik = nik;
        if(nama == "" || nik=="" || lok_kerja == "")
        {
            SSTools.ShowMessage("Harap Form di isi!!", SSTools.Position.bottom, SSTools.Time.threeSecond);
        }
        else
        {
            if (nik == PlayerPrefs.GetString("Nik User" + nik))
            {

                sc.changeScene("FirstScene");
                session_nik = PlayerPrefs.GetString("Nik User" + nik);
                setProfillingtoScene();
            }
            else
            {
                PlayerPrefs.SetString("Nik User" + nik, nik);
                PlayerPrefs.SetString("Nama" + nik, nama);
                PlayerPrefs.SetString("Lok Kerja" + nik, lok_kerja);
                session_nik = nik;
                sc.changeScene("FirstScene");
                setProfillingtoScene();
            }
        }
       
        Debug.Log(nama + nik + lok_kerja);
    }

    public void deleteAll()
    {
        NilaiDB mNilaiDB = new NilaiDB();
        mNilaiDB.deleteAllData();
        mNilaiDB.close();
    }

    public void setProfillingtoScene()
    {
        welcome_text.text = "Selamat Datang : " + session_nik;
    }
 
    public void addToSheet()
    {

        CreatePlayerTable();
        string jsonPlayer = JsonUtility.ToJson(userInfo);
        Debug.Log("<color=yellow>Sending following player to the cloud: \n</color>" + jsonPlayer);

        // Save the object on the cloud, in a table called like the object type.
        Drive.CreateObject(jsonPlayer, _table_name, true);
    }

    private void CreatePlayerTable()
    {
        Debug.Log("<color=yellow>Creating a table in the cloud for players data.</color>");

        // Creating a string array for field names (table headers) .
        string[] fieldNames = new string[5];
        fieldNames[0] = "nik";
        fieldNames[1] = "nama";
        fieldNames[2] = "lokasi";
        fieldNames[3] = "nilai";
        fieldNames[4] = "mode";
 
        // Request for the table to be created on the cloud.
        Drive.CreateTable(fieldNames, _table_name, true);
    }


}
