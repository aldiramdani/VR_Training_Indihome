using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DataBank
{
    public class NilaiDB : SQLliteHelper
    {
        private const String Tag = "Debug: LocationDB:\t";

        private const String TABLE_NAME = "Nilai_TBL1";
        private const String KEY_NIK = "nik";
        private const String KEY_NAMA = "nama";
        private const String KEY_LOK_KERJA = "lok_kerja";
        private const String KEY_NILAI = "nilai";
        private const String KEY_MODUL1 = "modul1";
        private const String KEY_MODUL2 = "modul2";
        private const String KEY_MODUL3 = "modul3";
        private const String KEY_MODUL4 = "modul4";
        private const String KEY_DATE = "date";
        private String[] COLUMNS = new String[] {KEY_NIK, KEY_NAMA, KEY_LOK_KERJA, KEY_NILAI,KEY_MODUL1,
            KEY_MODUL2, KEY_MODUL3, KEY_MODUL4, KEY_DATE };

        public NilaiDB() : base()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText = "CREATE TABLE IF NOT EXISTS " + TABLE_NAME + " ( " +
                KEY_NIK + " TEXT, " +
                KEY_NAMA + " TEXT, " +
                KEY_LOK_KERJA + " TEXT, " +
                KEY_NILAI + " TEXT, " +
                KEY_MODUL1 + " TEXT, " +
                KEY_MODUL2 + " TEXT, " +
                KEY_MODUL3 + " TEXT, " +
                KEY_MODUL4 + " TEXT, " +
                KEY_DATE + " DATETIME DEFAULT CURRENT_TIMESTAMP )";
            dbcmd.ExecuteNonQuery();
        }

        public void addData(NilaiEntity nilaiEntity)
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "INSERT INTO " + TABLE_NAME
                + " ( "
                + KEY_NIK + ", "
                + KEY_NAMA + ", "
                + KEY_LOK_KERJA + ", "
                + KEY_NILAI + ", "
                + KEY_MODUL1 + ", "
                + KEY_MODUL2 + ", "
                + KEY_MODUL3 + ", "
                + KEY_MODUL4 + ", "
                + KEY_DATE + " ) "
                + "VALUES ( '"
                
                + nilaiEntity._nik + "', '"
                + nilaiEntity._nama + "', '"
                + nilaiEntity._lok_kerja + "', '"
                + nilaiEntity._nilai + "', '"
                + nilaiEntity._modul1+ "', '"
                + nilaiEntity._modul2+ "', '"
                + nilaiEntity._modul3+ "', '"
                + nilaiEntity._modul4+ "', '"
                + nilaiEntity._dateCreated + "' )";
            dbcmd.ExecuteNonQuery();
        }

        public override IDataReader getDataById(int id)
        {
            return base.getDataById(id);
        }

        public override IDataReader getDataByString(string str)
        {
            Debug.Log(Tag + "Get Data By Nik: " + str);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME + " WHERE " + KEY_NIK + " = '" + str + "'";
            return dbcmd.ExecuteReader();
        }

        public override void deleteDataByString(string nik)
        {
            Debug.Log(Tag + "Get Data By Nik: " + nik);

            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "DELETE FROM " + TABLE_NAME + " WHERE " + KEY_NIK + " = '" + nik + "'";
            dbcmd.ExecuteNonQuery();
        }

        public override void deleteDataById(int nik)
        {
            base.deleteDataById(nik);
        }
        public override void deleteAllData()
        {
            Debug.Log(Tag + "Deleting Table");

            base.deleteAllData(TABLE_NAME);
        }

        public override IDataReader getAllData()
        {
            return base.getAllData(TABLE_NAME);
        }

        public IDataReader getLatestTimeStamp()
        {
            IDbCommand dbcmd = getDbCommand();
            dbcmd.CommandText =
                "SELECT * FROM " + TABLE_NAME + " ORDER BY " + KEY_DATE + " DESC LIMIT 1";
            return dbcmd.ExecuteReader();
        }
    }
}

