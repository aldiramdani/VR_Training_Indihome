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
                                    reader[4].ToString(),
                                    reader[5].ToString(),
                                    reader[6].ToString(),
                                    reader[7].ToString(),
                                    reader[8].ToString()
                                    );
            myList.Add(nilaiEntity);
        }

        for (int i=0;i < myList.Count; i++)
        {
            txt_nik.text += myList[i]._nik + "\n";
            txt_nama.text += myList[i]._nama + "\n";
            txt_nilai.text += myList[i]._nilai + "\n" ;
            txt_tanggal.text += myList[i]._dateCreated + "\n";
            txt_lokasi.text += myList[i]._lok_kerja + "\n";
            txt_mod1.text += myList[i]._modul1 + "\n";
            txt_mod2.text += myList[i]._modul2 + "\n";
            txt_mod3.text += myList[i]._modul3 + "\n";
            txt_mod4.text += myList[i]._modul4 + "\n";
        }
    }
                                 
    
    public void addtoDB()
    {
        NilaiDB mNilaiDB = new NilaiDB();
        mNilaiDB.addData(new NilaiEntity(Profilling.session_nik, 
            PlayerPrefs.GetString("Nama" + Profilling.session_nik), 
            PlayerPrefs.GetString("Lok Kerja" + Profilling.session_nik), 
            PlayerPrefs.GetString("nilai"),
            PlayerPrefs.GetString("c_modul1"), 
            PlayerPrefs.GetString("c_modul2"), 
            PlayerPrefs.GetString("c_modul3"), 
            PlayerPrefs.GetString("c_modul4"), 
            System.DateTime.Now.ToString("yyyyMMddhhmmss")));
        mNilaiDB.close();
        pf.addToSheet();    
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
