using System;
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
                select album;

            IEnumerable<DataRow> albumsArray = albumsQuery.ToArray();

            foreach (DataRow album in albumsArray)
            {
                Console.WriteLine(album.Field<string>("Title"));
            }

            Thread.Sleep(10000);

            connection.Close();

        }
    }
}
