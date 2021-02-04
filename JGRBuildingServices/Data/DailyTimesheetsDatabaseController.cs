using JGRBuildingServices.Models;
using JGRBuildingServices.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace JGRBuildingServices.Data
{
    public class DailyTimesheetsDatabaseController
    {
        static object locker = new object();

        SQLiteConnection database;

        public DailyTimesheetsDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();

            database.CreateTable<DailyTimesheets>();
        }

        public List<DailyTimesheetsListViewModel> GetAllDailyTimesheets()
        {
            lock (locker)
            {
                var dailyTimesheets = new List<DailyTimesheetsListViewModel>();

                var q = database.Query<DailyTimesheetsListViewModel>(
                        "Select DT.Id, DT.Date From DailyTimesheets DT"
                        + " Where DT.IsDeleted = ? And DT.SentToOffice = ? Order By DT.Id Desc", false, false).ToList();

                foreach (var item in q)
                {
                    dailyTimesheets.Add(new DailyTimesheetsListViewModel
                    {
                        Id = item.Id,
                        Date = item.Date
                    });
                }

                return dailyTimesheets;
            }
        }

        public List<DailyTimesheetsListViewModel> GetAllDailyTimesheetsSentToOffice()
        {
            lock (locker)
            {
                var dailyTimesheets = new List<DailyTimesheetsListViewModel>();

                var q = database.Query<DailyTimesheetsListViewModel>(
                        "Select DT.Id, DT.Date From DailyTimesheets DT"
                        + " Where DT.IsDeleted = ? And DT.SentToOffice = ? Order By DT.Id Desc", false, true).ToList();

                foreach (var item in q)
                {
                    dailyTimesheets.Add(new DailyTimesheetsListViewModel
                    {
                        Id = item.Id,
                        Date = item.Date
                    });
                }

                return dailyTimesheets;
            }
        }

        public DailyTimesheets GetDailyTimesheetById(Int32 id)
        {
            lock (locker)
            {
                return (from t in database.Table<DailyTimesheets>() select t).Where(c => c.Id == id).SingleOrDefault();
            }
        }

        public Boolean DailyTimesheetsentToOffice(Int32 dailyTimesheetId, Int32 officeId)
        {
            lock (locker)
            {
                var o = (from t in database.Table<DailyTimesheets>() select t).Where(c => c.Id == dailyTimesheetId).SingleOrDefault();

                o.SentToOffice = true;
                o.OfficeId = officeId;

                return database.Update(o) > 0;
            }
        }

        public Int32 InsertDailyTimesheet(DailyTimesheets model)
        {
            lock (locker)
            {
                var insert = database.Insert(model);

                return insert;
            }
        }

        public Int32 UpdateDailyTimesheet(DailyTimesheets model)
        {
            lock (locker)
            {
                var update = database.Update(model);

                return update;
            }
        }

        public Boolean DeleteDailyTimesheet(Int32 id)
        {
            lock (locker)
            {
                var o = (from t in database.Table<DailyTimesheets>() select t).Where(c => c.Id == id).SingleOrDefault();

                return database.Delete(o) > 0;
            }
        }
    }
}