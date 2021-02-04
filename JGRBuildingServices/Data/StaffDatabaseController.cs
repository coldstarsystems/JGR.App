using JGRBuildingServices.Models;
using JGRBuildingServices.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace JGRBuildingServices.Data
{
    public class StaffDatabaseController
    {
        static object locker = new object();

        SQLiteConnection database;

        public StaffDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();

            database.CreateTable<Staff>();
        }

        public Staff GetStaffById(Int32 id)
        {
            lock (locker)
            {
                return (from t in database.Table<Staff>() select t).Where(c => c.Id == id).SingleOrDefault();
            }
        }

        public IQueryable<StaffViewModel> GetAllStaff()
        {
            lock (locker)
            {
                return (from t in database.Table<Staff>() select new StaffViewModel() { FirstName = t.FirstName, LastName = t.LastName, Id = t.Id }).OrderBy(c => c.LastName).AsQueryable();
            }
        }

        public void InsertAllStaff(List<Staff> model)
        {
            lock (locker)
            {
                foreach (var item in model)
                {
                    var o = (from t in database.Table<Staff>() select t).Where(c => c.Id == item.Id).SingleOrDefault();

                    if (o != null)
                    {
                        o.Id = item.Id;
                        o.FirstName = item.FirstName;
                        o.LastName = item.LastName;
                        o.AnnualLeaveAllowance = item.AnnualLeaveAllowance;
                        o.AnnualLeaveBalance = item.AnnualLeaveBalance;
                        o.PurchaseOrderIdentifier = item.PurchaseOrderIdentifier;

                        database.Update(o);
                    }
                    else
                    {
                        database.Insert(item);
                    }
                }
            }
        }

        public void DeleteAllStaff()
        {
            lock (locker)
            {
                database.DeleteAll<Staff>();
            }
        }

        public Boolean UpdateStaffPurchaseOrderNumber(Int32 staffId)
        {
            lock (locker)
            {
                var o = (from t in database.Table<Staff>() select t).Where(c => c.Id == staffId).SingleOrDefault();

                o.NextPurchaseOrderNumber = o.NextPurchaseOrderNumber + 1;

                return database.Update(o) > 0;
            }
        }
    }
}