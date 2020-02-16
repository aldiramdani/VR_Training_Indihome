using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataBank;
public class DBTest : MonoBehaviour
{
    // Start is called before the first frame update
    public Text txt_debug;
    void Start()
    {
        NilaiDB mNilaiDB = new NilaiDB();

        mNilaiDB.addData(new NilaiEntity("005", "Aldi Ganteng", "Bandung", "200"));

        mNilaiDB.close();

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
            txt_debug.text = myList[i]._nik + myList[i]._nama + myList[i]._nilai;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
