using DataBank;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Profilling : MonoBehaviour
{
    // Start is called before the first frame update
    public Text welcome_text;
    public Button btn_mulai;
    public InputField inputNama,inputNik;
    public Dropdown drpTempatKerja;
    string nama, nik, lok_kerja;
    sceneControler sc = new sceneControler();
    public static string session_nik;
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
        welcome_text.text = "Selamat Datang : "+session_nik;
    }

 
}
