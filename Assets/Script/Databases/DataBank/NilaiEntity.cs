using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBank
{
    public class NilaiEntity
    {
        
        public string _nik;
        public string _nama;
        public string _lok_kerja;
        public string _nilai;
        public string _modul1;
        public string _modul2;
        public string _modul3;
        public string _modul4;
        public string _dateCreated;

        public NilaiEntity( string nik, string nama, string lok_kerja,string nilai,string modul1)
        {
            
            _nik = nik;
            _nama = nama;
            _lok_kerja = lok_kerja;
            _nilai = nilai;
            _modul1 = modul1;
            _dateCreated = "";
        }
        public NilaiEntity(string nik, string nama, string lok_kerja,string nilai,string modul1, 
            string modul2, string modul3, string modul4, string dateCreated)
        {
            
            _nik = nik;
            _nama = nama;
            _lok_kerja = lok_kerja;
            _nilai = nilai;
            _modul1 = modul1;
            _modul2 = modul2;
            _modul3 = modul3;
            _modul4 = modul4;
            _dateCreated = dateCreated;
        }

        public static NilaiEntity getFakeNilai()
        {
            return new NilaiEntity("001", "Jajang","Bandung", "200","1");
        }

    }
}

