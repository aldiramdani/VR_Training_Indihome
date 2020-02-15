using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Profilling : MonoBehaviour
{
    // Start is called before the first frame update

    public Button btn_mulai;
    public InputField inputNama,inputNik;
    public Dropdown drpTempatKerja;
    string nama, nik, lok_kerja;
    void Start()
    {
        
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
        if (nik == PlayerPrefs.GetString("Nik User"))
        {
            SSTools.ShowMessage("Nik Sudah Terdaftar", SSTools.Position.bottom, SSTools.Time.threeSecond);
        }
        else
        {
            PlayerPrefs.SetString("Nik User", nik);
            PlayerPrefs.SetString("Nama" + nik, nama);
            PlayerPrefs.SetString("Lok Kerja" + nik, lok_kerja);
        }
        Debug.Log(nama + nik + lok_kerja);
    }

    public void checkOtherUser()
    {
        
    }
}
