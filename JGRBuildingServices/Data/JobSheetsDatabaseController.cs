using JGRBuildingServices.Models;
using JGRBuildingServices.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace JGRBuildingServices.Data
{
    public class JobSheetsDatabaseController
    {
        static object locker = new object();

        SQLiteConnection database;

        public JobSheetsDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();

            database.CreateTable<JobSheets>();
        }

        public List<JobSheetsListViewModel> GetAllJobSheets()
        {
            lock (locker)
            {
                var jobSheets = new List<JobSheetsListViewModel>();

                var q = database.Query<JobSheetsListViewModel>(
                        "Select JS.Id, JS.CreatedDate As CreatedDate, C.Name As CustomerName From JobSheets JS"
                        + " Inner Join Customers C On JS.CustomerId = C.Id"
                        + " Where JS.IsDeleted = ? And JS.SentToOffice = ? Order By JS.CreatedDate Desc", false, false).ToList();

                foreach (var item in q)
                {
                    jobSheets.Add(new JobSheetsListViewModel
                    {
                        Id = item.Id,
                        CreatedDate = item.CreatedDate,
                        CustomerName = item.CustomerName
                    });
                }

                return jobSheets;
            }
        }

        public List<JobSheetsListViewModel> GetAllJobSheetsSentToOffice()
        {
            lock (locker)
            {
                var jobSheets = new List<JobSheetsListViewModel>();

                var q = database.Query<JobSheetsListViewModel>(
                        "Select JS.Id, JS.CreatedDate As CreatedDate, C.Name As CustomerName From JobSheets JS"
                        + " Inner Join Customers C On JS.CustomerId = C.Id"
                        + " Where JS.IsDeleted = ? And JS.SentToOffice = ? Order By JS.CreatedDate Desc", false, true).ToList();

                foreach (var item in q)
                {
                    jobSheets.Add(new JobSheetsListViewModel
                    {
                        Id = item.Id,
                        CreatedDate = item.CreatedDate,
                        CustomerName = item.CustomerName
                    });
                }

                return jobSheets;
            }
        }

        public JobSheets GetJobSheetById(Int32 id)
        {
            lock (locker)
            {
                return (from t in database.Table<JobSheets>() select t).Where(c => c.Id == id).SingleOrDefault();
            }
        }

        public Boolean JobSheetSentToOffice(Int32 jobSheetId, Int32 officeId)
        {
            lock (locker)
            {
                var o = (from t in database.Table<JobSheets>() select t).Where(c => c.Id == jobSheetId).SingleOrDefault();

                o.SentToOffice = true;
                o.OfficeId = officeId;

                return database.Update(o) > 0;
            }
        }

        public Int32 InsertJobSheet(JobSheets model)
        {
            lock (locker)
            {
                var insert = database.Insert(model);

                return insert;
            }
        }

        public Int32 UpdateJobSheet(JobSheets model)
        {
            lock (locker)
            {
                var update = database.Update(model);

                return update;
            }
        }

        public Boolean DeleteJobSheet(Int32 id)
        {
            lock (locker)
            {
                var o = (from t in database.Table<JobSheets>() select t).Where(c => c.Id == id).SingleOrDefault();

                return database.Delete(o) > 0;
            }
        }
    }
}