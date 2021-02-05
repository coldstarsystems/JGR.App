using JGRBuildingServices.Models;
using SQLite;
using System;
using System.Linq;
using Xamarin.Forms;

namespace JGRBuildingServices.Data
{
    public class SettingsDatabaseController
    {
        static object locker = new object();

        SQLiteConnection database;

        public SettingsDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();

            database.CreateTable<Settings>();
        }

        public Settings GetSetting(String name)
        {
            lock (locker)
            {
                var o = (from t in database.Table<Settings>() select t).Where(c => c.Name == name).SingleOrDefault();

                if (o != null)
                {
                    return o;
                }
                else
                {
                    return null;
                }
            }
        }

        public Boolean SaveSetting(String name, String value)
        {
            lock (locker)
            {
                var setingInformation = GetSetting(name);

                if (setingInformation == null)
                {
                    var s = new Settings()
                    {
                        Name = name,
                        Value = value,
                        LastSynced = DateTime.Now
                    };

                    return database.Insert(s) > 0;
                }
                else
                {
                    var s = new Settings()
                    {
                        Id = setingInformation.Id,
                        Name = name,
                        Value = value,
                        LastSynced = DateTime.Now
                    };

                    return database.Update(s) > 0;
                }
            }
        }
    }
}