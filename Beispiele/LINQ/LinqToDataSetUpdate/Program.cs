﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Data;
using System.Data.SQLite;

namespace ADOBeispiel
{
    class Program
    {
        static void Main(string[] args)
        {
            
            SQLiteConnection connection = new SQLiteConnection("Data Source=../../../Chinook_Sqlite_AutoIncrementPKs.sqlite");

            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter("SELECT * FROM Album", connection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            DataTable albums = dataSet.Tables[0];

            IEnumerable<DataRow> albumsQuery =
                from album in albums.AsEnumerable()
                where album["AlbumId"].ToString() == "1"
                select album;

            DataRow albumRow = albumsQuery.ToArray()[0];

            albumRow["Title"] = "For Those About To Rock We Salute You Second Edition";

            SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(dataAdapter);

            dataAdapter.Update(dataSet);

            connection.Close();

        }
    }
}
