using System;
using JGRBuildingServices.Data;
using SQLite;
using System.IO;
using Xamarin.Forms;
using JGRBuildingServices.Droid.Data;

[assembly: Dependency(typeof(SQLiteAndroid))]
namespace JGRBuildingServices.Droid.Data
{
    public class SQLiteAndroid : ISQLite
    {
        public SQLiteAndroid()
        {

        }

        public SQLiteConnection GetConnection()
        {
            var fileName = "JGRBuildingServices.db3";

            String documentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

            var path = Path.Combine(documentPath, fileName);
            var conn = new SQLiteConnection(path);

            return conn;
        }
    }
}