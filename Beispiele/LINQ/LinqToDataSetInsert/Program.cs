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
        private static SQLiteConnection _connection;

        static void Main(string[] args)
        {
            _connection = new SQLiteConnection("Data Source=../../../Chinook_Sqlite_AutoIncrementPKs.sqlite");

            //AddAlbumWithTracks("title", 123);
            //DeleteAlbum(352);
            //FindAlbumByTitle("Mozart: Chamber Music");
            //FindTrackByTitle("Go Down");
            FindAlbumByInterpret("AC/DC", "Let There Be Rock");


            _connection.Close();
        }

        private static void FindAlbumByInterpret(string artistNme, string albumName)
        {
            Console.WriteLine("search artist {0} album {1}", artistNme, albumName);

            DataSet albumDs = new DataSet();
            new SQLiteDataAdapter("SELECT * FROM Album; SELECT * FROM Artist", _connection).Fill(albumDs);
            DataTable albums = albumDs.Tables[0];
            DataTable artists = albumDs.Tables[1];

            IEnumerable<DataRow> albumsQuery =
                from artist in artists.AsEnumerable()
                join album in albums.AsEnumerable()
                    on artist["ArtistId"] equals album["ArtistId"]
                where artist["Name"].ToString() == artistNme
                      && album["Title"].ToString() == albumName
                select album;
            IEnumerable<DataRow> albumsArray = albumsQuery.ToArray();
            foreach (DataRow album in albumsArray)
            {
                Console.WriteLine("Found: " + album.Field<string>("Title"));
            }
        }

        private static void FindTrackByTitle(string title)
        {
            Console.WriteLine("search track title {0}", title);
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter("SELECT * FROM Track", _connection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            DataTable tracks = dataSet.Tables[0];

            IEnumerable<DataRow> albumsQuery =
                from track in tracks.AsEnumerable()
                where track["Name"].ToString() == title
                select track;
            IEnumerable<DataRow> albumsArray = albumsQuery.ToArray();
            foreach (DataRow album in albumsArray)
            {
                Console.WriteLine("Found: " + album.Field<string>("Name"));
            }
        }

        private static void AddAlbumWithTracks(string albumName, int artistId)
        {
            Console.WriteLine("add album: name {0}, artistId {1}", albumName, artistId);
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter("SELECT * FROM Album", _connection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            DataTable albums = dataSet.Tables[0];

            DataRow newRow = albums.NewRow();
            newRow["Title"] = albumName;
            newRow["ArtistId"] = artistId;
            albums.Rows.Add(newRow);

            SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(dataAdapter);
            var rowsAffected = dataAdapter.Update(dataSet);
            Console.WriteLine("{0} rows added", rowsAffected);
        }

        private static void DeleteAlbum(int albumId)
        {
            Console.WriteLine("delete album {0}", albumId);
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter("SELECT * FROM Album", _connection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            DataTable albums = dataSet.Tables[0];

            IEnumerable<DataRow> albumsQuery =
                from album in albums.AsEnumerable()
                where album["AlbumId"].ToString() == albumId.ToString()
                select album;
            DataRow albumRow = albumsQuery.ToArray()[0];
            albumRow.Delete();

            SQLiteCommandBuilder commandBuilder = new SQLiteCommandBuilder(dataAdapter);
            var rowsAffected = dataAdapter.Update(dataSet);
            Console.WriteLine("{0} rows deleted", rowsAffected);
        }

        private static void FindAlbumByTitle(string title)
        {
            Console.WriteLine("search album title {0}", title);
            SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter("SELECT * FROM Album", _connection);
            DataSet dataSet = new DataSet();
            dataAdapter.Fill(dataSet);
            DataTable albums = dataSet.Tables[0];

            IEnumerable<DataRow> albumsQuery =
                from album in albums.AsEnumerable()
                where album["Title"].ToString() == title
                select album;
            IEnumerable<DataRow> albumsArray = albumsQuery.ToArray();
            foreach (DataRow album in albumsArray)
            {
                Console.WriteLine("Found: " + album.Field<string>("Title"));
            }
        }
    }
}