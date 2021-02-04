using JGRBuildingServices.Models;
using JGRBuildingServices.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace JGRBuildingServices.Data
{
    public class AnnualLeaveDatabaseController
    {
        static object locker = new object();

        SQLiteConnection database;

        public AnnualLeaveDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();

            database.CreateTable<AnnualLeave>();
        }

        public Boolean AnnualLeaveSentToOffice(Int32 annualLeaveId)
        {
            lock (locker)
            {
                var o = (from t in database.Table<AnnualLeave>() select t).Where(c => c.Id == annualLeaveId).SingleOrDefault();

                o.SentToOffice = true;

                return database.Update(o) > 0;
            }
        }

        public Boolean InsertAnnualLeaveRequest(AnnualLeave model)
        {
            lock (locker)
            {
                var insert = database.Insert(model) > 0;

                return insert;
            }
        }

        public Boolean UpdateAnnualLeaveRequest(AnnualLeave model)
        {
            lock (locker)
            {
                var update = database.Update(model) > 0;

                return update;
            }
        }

        public Boolean InsertUpdateAnnualLeaveRequest(AnnualLeave model)
        {
            lock (locker)
            {
                var annualLeaveRequest = database.Table<AnnualLeave>().Where(c => c.RequestId == model.RequestId).SingleOrDefault();

                if (annualLeaveRequest != null)
                {
                    model.Id = annualLeaveRequest.Id;

                    var mn = database.Update(model);

                    return true;
                }
                else
                {
                    var mn = database.Insert(model);

                    return true;
                }
            }
        }

        public Boolean DeleteAnnualLeaveRequest(Int32 annualLeaveRequestId)
        {
            lock (locker)
            {
                var o = (from t in database.Table<AnnualLeave>() select t).Where(c => c.Id == annualLeaveRequestId).SingleOrDefault();

                var delete = database.Delete(o) > 0;

                return delete;
            }
        }

        public AnnualLeave GetAnnualLeaveRequestById(Int32 id)
        {
            lock (locker)
            {
                return (from t in database.Table<AnnualLeave>() select t).Where(c => c.Id == id).SingleOrDefault();
            }
        }

        public List<AnnualLeaveRequestsListViewModel> GetPendingAnnualLeaveRequests(Int32 staffId)
        {
            lock (locker)
            {
                var annualLeaveRequests = new List<AnnualLeaveRequestsListViewModel>();

                var q = database.Query<AnnualLeaveRequestsListViewModel>(
                        "Select Id, Starting, Ending, TotalDays From AnnualLeave"
                        + " Where StatusId = ? And StaffId = ? Order By Starting Desc, Ending Desc", 1, staffId).ToList();

                foreach (var item in q)
                {
                    annualLeaveRequests.Add(new AnnualLeaveRequestsListViewModel { Id = item.Id, Starting = item.Starting, Ending = item.Ending, TotalDays = item.TotalDays });
                }

                return annualLeaveRequests;
            }
        }

        public List<AnnualLeaveRequestsListViewModel> GetApprovedAnnualLeaveRequests(Int32 staffId)
        {
            lock (locker)
            {
                var annualLeaveRequests = new List<AnnualLeaveRequestsListViewModel>();

                var q = database.Query<AnnualLeaveRequestsListViewModel>(
                        "Select Id, Starting, Ending, TotalDays From AnnualLeave"
                        + " Where StatusId = ? And StaffId = ? Order By Starting Desc, Ending Desc", 2, staffId).ToList();

                foreach (var item in q)
                {
                    annualLeaveRequests.Add(new AnnualLeaveRequestsListViewModel { Id = item.Id, Starting = item.Starting, Ending = item.Ending, TotalDays = item.TotalDays });
                }

                return annualLeaveRequests;
            }
        }

        public List<AnnualLeaveRequestsListViewModel> GetDeclinedAnnualLeaveRequests(Int32 staffId)
        {
            lock (locker)
            {
                var annualLeaveRequests = new List<AnnualLeaveRequestsListViewModel>();

                var q = database.Query<AnnualLeaveRequestsListViewModel>(
                        "Select Id, Starting, Ending, TotalDays From AnnualLeave"
                        + " Where StatusId = ? And StaffId = ? Order By Starting Desc, Ending Desc", 3, staffId).ToList();

                foreach (var item in q)
                {
                    annualLeaveRequests.Add(new AnnualLeaveRequestsListViewModel { Id = item.Id, Starting = item.Starting, Ending = item.Ending, TotalDays = item.TotalDays });
                }

                return annualLeaveRequests;
            }
        }

        public List<AnnualLeaveRequestsListViewModel> GetCancelledAnnualLeaveRequests(Int32 staffId)
        {
            lock (locker)
            {
                var annualLeaveRequests = new List<AnnualLeaveRequestsListViewModel>();

                var q = database.Query<AnnualLeaveRequestsListViewModel>(
                        "Select Id, Starting, Ending, TotalDays From AnnualLeave"
                        + " Where StatusId = ? And StaffId = ? Order By Starting Desc, Ending Desc", 4, staffId).ToList();

                foreach (var item in q)
                {
                    annualLeaveRequests.Add(new AnnualLeaveRequestsListViewModel { Id = item.Id, Starting = item.Starting, Ending = item.Ending, TotalDays = item.TotalDays });
                }

                return annualLeaveRequests;
            }
        }
    }
}