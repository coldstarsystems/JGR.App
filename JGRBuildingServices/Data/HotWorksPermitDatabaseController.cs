using JGRBuildingServices.Models;
using JGRBuildingServices.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace JGRBuildingServices.Data
{
    public class HotWorksPermitDatabaseController
    {
        static object locker = new object();

        SQLiteConnection database;

        public HotWorksPermitDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();

            database.CreateTable<HotWorksPermit>();
        }

        public List<HotWorkPermitListViewModel> GetAllHotWorksPermits()
        {
            lock (locker)
            {
                var hotWorksPermit = new List<HotWorkPermitListViewModel>();

                var q = database.Query<HotWorkPermitListViewModel>(
                        "Select DT.Id, DT.CreatedDate, DT.Content From HotWorksPermit DT"
                        + " Where DT.IsDeleted = ? And DT.SentToOffice = ? Order By DT.Id Desc", false, false).ToList();

                foreach (var item in q)
                {
                    hotWorksPermit.Add(new HotWorkPermitListViewModel
                    {
                        Id = item.Id,
                        Content = item.Content,
                        CreatedDate = item.CreatedDate
                    });
                }

                return hotWorksPermit;
            }
        }

        public List<HotWorkPermitListViewModel> GetAllHotWorksPermitsSentToOffice()
        {
            lock (locker)
            {
                var hotWorksPermit = new List<HotWorkPermitListViewModel>();

                var q = database.Query<HotWorkPermitListViewModel>(
                        "Select DT.Id, DT.CreatedDate, DT.Content From HotWorksPermit DT"
                        + " Where DT.IsDeleted = ? And DT.SentToOffice = ? Order By DT.Id Desc", false, true).ToList();

                foreach (var item in q)
                {
                    hotWorksPermit.Add(new HotWorkPermitListViewModel
                    {
                        Id = item.Id,
                        Content = item.Content,
                        CreatedDate = item.CreatedDate
                    });
                }

                return hotWorksPermit;
            }
        }

        public HotWorksPermit GetHotWorksPermitById(Int32 id)
        {
            lock (locker)
            {
                return (from t in database.Table<HotWorksPermit>() select t).Where(c => c.Id == id).SingleOrDefault();
            }
        }

        public Boolean HotWorksPermitsentToOffice(Int32 HotWorksPermitId, Int32 officeId)
        {
            lock (locker)
            {
                var o = (from t in database.Table<HotWorksPermit>() select t).Where(c => c.Id == HotWorksPermitId).SingleOrDefault();

                o.SentToOffice = true;
                o.OfficeId = officeId;

                return database.Update(o) > 0;

            }
        }

        public Int32 InsertHotWorksPermit(Models.HotWorksPermit model)
        {
            lock (locker)
            {
                var insert = database.Insert(model);

                return insert;
            }
        }

        public Int32 UpdateHotWorksPermit(Models.HotWorksPermit model)
        {
            lock (locker)
            {
                var update = database.Update(model);

                return update;
            }
        }

        public Boolean DeleteHotWorksPermit(Int32 id)
        {
            lock (locker)
            {
                var o = (from t in database.Table<HotWorksPermit>() select t).Where(c => c.Id == id).SingleOrDefault();

                return database.Delete(o) > 0;
            }
        }
    }
}
