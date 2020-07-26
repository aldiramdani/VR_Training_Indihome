using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataBank;
public class scoreDB : MonoBehaviour
{
     public Text txt_nik,txt_nama,
        txt_nilai,txt_tanggal,txt_lokasi,
        txt_mod1,txt_mod2,txt_mod3,txt_mod4;
    public Transform scoreContainer;
    public Transform scoreTemplete;
     private void Awake() {

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

        scoreTemplete.gameObject.SetActive(false);
        float templeteHeight = 205f;
        for(int i = 0 ; i < myList.Count;i++){
            txt_nik.text = myList[i]._nik ;
            txt_nama.text = myList[i]._nama;
            txt_nilai.text = myList[i]._nilai;
            txt_tanggal.text = myList[i]._dateCreated;
            txt_lokasi.text = myList[i]._lok_kerja;
            txt_mod1.text = myList[i]._modul1;
            txt_mod2.text = myList[i]._modul2;
            txt_mod3.text = myList[i]._modul3;
            txt_mod4.text = myList[i]._modul4;
            Transform entryTransform = Instantiate(scoreTemplete,scoreContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0,-templeteHeight * i);
            entryTransform.gameObject.SetActive(true);
        }
    }
    // Start is called before the first frame update
}
