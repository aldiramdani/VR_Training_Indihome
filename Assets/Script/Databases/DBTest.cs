using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataBank;
public class DBTest : MonoBehaviour
{
    // Start is called before the first frame update
    public Text txt_nik,txt_nama,
        txt_nilai,txt_tanggal,txt_lokasi,
        txt_mod1,txt_mod2,txt_mod3,txt_mod4;
    Profilling pf = new Profilling();
    void Start()
    {
        

        NilaiDB mNilaiDB2 = new NilaiDB();
        System.Data.IDataReader reader = mNilaiDB2.getAllData(); 
        int fieldCount = reader.FieldCount;
        List<NilaiEntity> myList = new List<NilaiEntity>();
       
        while (reader.Read())
        {
            NilaiEntity nilaiEntity = new NilaiEntity(reader[0].ToString(),
                                    reader[1].ToString(),
                                    reader[2].ToString(),
                                    reader[3].ToString(),
                                    reader[4].ToString()
                                    );
            Debug.Log("Nik: " + nilaiEntity._nik);
            myList.Add(nilaiEntity);
        }

        for (int i=0;i < myList.Count; i++)
        {
            txt_nik.text += myList[i]._nik + "\n";
            txt_nama.text += myList[i]._nama + "\n";
            txt_nilai.text += myList[i]._nilai + "\n" ;
            txt_tanggal.text += myList[i]._dateCreated + "\n";
            txt_lokasi.text += myList[i]._lok_kerja + "\n";
            switch (myList[i]._modul)
            {
                case "1":
                    txt_mod1.text = "V";
                    break;
                case "2":
                    txt_mod2.text = "V";
                    break;
                case "3":
                    txt_mod3.text = "V";
                    break;
                case "4":
                    txt_mod4.text = "V";
                    break;
            }
        }
    }
                                 
    
    public void addtoDB()
    {
        NilaiDB mNilaiDB = new NilaiDB();
        mNilaiDB.addData(new NilaiEntity(Profilling.session_nik, PlayerPrefs.GetString("Nama" + Profilling.session_nik), 
            PlayerPrefs.GetString("Lok Kerja" + Profilling.session_nik), PlayerPrefs.GetString("nilai"),PlayerPrefs.GetString("c_modul")));
        mNilaiDB.close();
        pf.addToSheet();    
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
