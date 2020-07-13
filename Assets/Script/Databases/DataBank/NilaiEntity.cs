﻿using System;
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
        public string _modul;
        public string _dateCreated;

        public NilaiEntity( string nik, string nama, string lok_kerja,string nilai,string modul)
        {
            
            _nik = nik;
            _nama = nama;
            _lok_kerja = lok_kerja;
            _nilai = nilai;
            _modul = modul;
            _dateCreated = "";
        }
        public NilaiEntity(string nik, string nama, string lok_kerja,string nilai,string modul, string dateCreated)
        {
            
            _nik = nik;
            _nama = nama;
            _lok_kerja = lok_kerja;
            _nilai = nilai;
            _modul = modul;
            _dateCreated = dateCreated;
        }

        public static NilaiEntity getFakeNilai()
        {
            return new NilaiEntity("001", "Jajang","Bandung", "200","1");
        }

    }
}

