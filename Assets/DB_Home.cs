using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataBank;

public class DB_Home : MonoBehaviour
{
    public Text txt_nik, txt_nama, 
        txt_nilai, txt_tanggal;
    Profilling pf = new Profilling();
    // Start is called before the first frame update
    void Start()
    {
        NilaiDB mNilaiDB = new NilaiDB();
        System.Data.IDataReader reader = mNilaiDB.getDataByString(Profilling.session_nik);
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

        for (int i = 0; i < myList.Count; i++)
        {
            txt_nik.text += myList[i]._nik + "\n";
            txt_nama.text += myList[i]._nama + "\n";
            txt_nilai.text += myList[i]._nilai + "\n";
            txt_tanggal.text += myList[i]._dateCreated + "\n";
        }
    }
}
