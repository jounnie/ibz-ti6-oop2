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

            //1. Variante 
            // Create the Command
            SQLiteCommand command = connection.CreateCommand();
            command.CommandText = "SELECT Title FROM Album";

            connection.Open();

            SQLiteDataReader reader = command.ExecuteReader();

            
            while (reader.Read())
            {
                Console.WriteLine(" Title:{0}", reader[0]);

            }

            reader.Close();

            connection.Close();

            Thread.Sleep(5000);

        }
    }
}
