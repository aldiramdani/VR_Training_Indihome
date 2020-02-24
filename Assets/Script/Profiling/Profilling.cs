using DataBank;
using GoogleSheetsForUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Profilling : MonoBehaviour
{
    //hapus variable ga kepake
    // Start is called before the first frame update
    public Text welcome_text;
    public Text debug_text;
    public Button btn_mulai;
    public InputField inputNama, inputNik,inputAuthBelajar,inputAuthEvaluasi;
    public Dropdown drpTempatKerja;
    string nama, nik, lok_kerja;
    string _table_name = "Data Pengguna VR Training";
    sceneControler sc = new sceneControler();
    public static string session_nik;
    private string authBelajar;
    private string authEvaluasi;

    [System.Serializable]
    public struct UserInfo {
        public string nik;
        public string nama;
        public string lokasi;
        public string nilai;
        public string mode;
        public string auth_belajar;
        public string auth_evaluasi;
    }

    private UserInfo userInfo = new UserInfo
    {
        nik = session_nik,
        nama = PlayerPrefs.GetString("Nama" + session_nik),
        lokasi = PlayerPrefs.GetString("Lok Kerja" + session_nik),
        nilai = PlayerPrefs.GetString("nilai"),
        mode = sceneManager.session_mode,
        auth_belajar = "XX",
        auth_evaluasi = "XX"
    };

    private void OnEnable()
    {
        // Suscribe for catching cloud responses.
        Drive.responseCallback += HandleDriveResponse;
    }

    private void OnDisable()
    {
        // Remove listeners.
        Drive.responseCallback -= HandleDriveResponse;
    }

    void Start()
    {
        RetrieveAuth();
        setProfillingtoScene();
    }

    // Update is called once per frame
    void Update()
    {
        
        Debug.Log("Debug Status" + authBelajar +" " + authEvaluasi);
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

    public void Authtentifikasi(string jenis_auth)
    {
        string _inputAuthBelajar = inputAuthBelajar.text;
        string _inputAuthEvaluasi = inputAuthEvaluasi.text;

        if(jenis_auth == "belajar")
        {
            if (_inputAuthBelajar == authBelajar)
            {
                SSTools.ShowMessage("Auth Belajar Sama !!", SSTools.Position.bottom, SSTools.Time.threeSecond);
            }
            else
            {
                SSTools.ShowMessage("Auth Salah Harap Tanyakan Kode Autentifikasi !!", SSTools.Position.bottom, SSTools.Time.threeSecond);
            }
        }else if(jenis_auth == "evaluasi")
        {
            if (_inputAuthEvaluasi == authEvaluasi)
            {
                SSTools.ShowMessage("Auth Evaluasi Sama !!", SSTools.Position.bottom, SSTools.Time.threeSecond);
            }
            else
            {
                SSTools.ShowMessage("Auth Salah Harap Tanyakan Kode Autentifikasi !!", SSTools.Position.bottom, SSTools.Time.threeSecond);
            }
        }
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
        string[] fieldNames = new string[7];
        fieldNames[0] = "nik";
        fieldNames[1] = "nama";
        fieldNames[2] = "lokasi";
        fieldNames[3] = "nilai";
        fieldNames[4] = "mode";
        fieldNames[5] = "auth_belajar";
        fieldNames[6] = "auth_evaluasi";
 
        // Request for the table to be created on the cloud.
        Drive.CreateTable(fieldNames, _table_name, true);
    }

    public void RetrieveAuth()
    {
        Debug.Log("<color=yellow>Retrieving player of name Mithrandir from the Cloud.</color>");

        // Get any objects from table 'PlayerInfo' with value 'Mithrandir' in the field called 'name'.
        Drive.GetObjectsByField(_table_name, "nama", "admin", true);
    }


    private void GetAllPlayers()
    {
        Debug.Log("<color=yellow>Retrieving all players from the Cloud.</color>");

        // Get all objects from table 'PlayerInfo'.
        Drive.GetTable(_table_name, true);
    }

    private void GetAllTables()
    {
        Debug.Log("<color=yellow>Retrieving all data tables from the Cloud.</color>");

        // Get all objects from table 'PlayerInfo'.
        Drive.GetAllTables(true);
    }

    public void HandleDriveResponse(Drive.DataContainer dataContainer)
    {
        Debug.Log(dataContainer.msg);

        // First check the type of answer.
        if (dataContainer.QueryType == Drive.QueryType.getObjectsByField)
        {
            string rawJSon = dataContainer.payload;
            Debug.Log(rawJSon);

            // Check if the type is correct.
            if (string.Compare(dataContainer.objType, _table_name) == 0)
            {
                // Parse from json to the desired object type.
                UserInfo[] users = JsonHelper.ArrayFromJson<UserInfo>(rawJSon);

                for (int i = 0; i < users.Length; i++)
                {
                    userInfo = users[i];
                    authBelajar = userInfo.auth_belajar;
                    authEvaluasi = userInfo.auth_evaluasi;
                    Debug.Log("<color=yellow>Object retrieved from the cloud and parsed: \n</color>" +
                        "Name: " + userInfo.nama );
                }
            }
        }

        // First check the type of answer.
        if (dataContainer.QueryType == Drive.QueryType.getTable)
        {
            string rawJSon = dataContainer.payload;
            Debug.Log(rawJSon);

            // Check if the type is correct.
            if (string.Compare(dataContainer.objType, _table_name) == 0)
            {
                // Parse from json to the desired object type.
                UserInfo[] players = JsonHelper.ArrayFromJson<UserInfo>(rawJSon);

                for (int i = 0; i < players.Length; i++)
                {
                    userInfo = players[i];
                    Debug.Log("<color=yellow>Object retrieved from the cloud and parsed: \n</color>" +
                        "Name: " + userInfo.nama );
                }
            }
        }

        // First check the type of answer.
        if (dataContainer.QueryType == Drive.QueryType.getAllTables)
        {
            string rawJSon = dataContainer.payload;

            // The response for this query is a json list of objects that hold tow fields:
            // * objType: the table name (we use for identifying the type).
            // * payload: the contents of the table in json format.
            Drive.DataContainer[] tables = JsonHelper.ArrayFromJson<Drive.DataContainer>(rawJSon);

            // Once we get the list of tables, we could use the objTypes to know the type and convert json to specific objects.
            // On this example, we will just dump all content to the console, sorted by table name.
            string logMsg = "<color=yellow>All data tables retrieved from the cloud.\n</color>";
            for (int i = 0; i < tables.Length; i++)
            {
                logMsg += "\n<color=blue>Table Name: " + tables[i].objType + "</color>\n" + tables[i].payload + "\n";
            }
            Debug.Log(logMsg);
        }
    }
    
    //untuk auth kelas belajar/evaluasi

}

