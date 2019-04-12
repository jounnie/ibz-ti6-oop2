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

            Console.WriteLine("DataSet Example!!!");
            Console.WriteLine("------------------");

            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter("SELECT Title FROM Album", connection);

            DataSet dataSet = new DataSet();

            dataAdapter.Fill(dataSet);

            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
            {
                Console.WriteLine(" Title:{0}", dataRow[0].ToString());
            }

            Thread.Sleep(10000);

            connection.Close();

        }
    }
}
