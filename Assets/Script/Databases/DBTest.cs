using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataBank;
public class DBTest : MonoBehaviour
{
    // Start is called before the first frame update
    public Text txt_nik,txt_nama,txt_nilai;
    void Start()
    {
        

        NilaiDB mNilaiDB2 = new NilaiDB();
        System.Data.IDataReader reader = mNilaiDB2.getDataByString(Profilling.session_nik);
        
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
            txt_nilai.text += myList[i]._nilai + "\n";
        }
    }
                                 
    
    public void addtoDB()
    {
        NilaiDB mNilaiDB = new NilaiDB();
        mNilaiDB.addData(new NilaiEntity(Profilling.session_nik, PlayerPrefs.GetString("Nama" + Profilling.session_nik), 
            PlayerPrefs.GetString("Lok Kerja" + Profilling.session_nik), PlayerPrefs.GetString("nilai")));
        mNilaiDB.close();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
