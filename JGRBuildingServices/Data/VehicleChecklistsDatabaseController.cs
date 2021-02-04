using JGRBuildingServices.Models;
using JGRBuildingServices.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace JGRBuildingServices.Data
{
    public class VehicleChecklistsDatabaseController
    {
        static object locker = new object();

        SQLiteConnection database;

        public VehicleChecklistsDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();

            database.CreateTable<VehicleChecklists>();
        }

        public List<VehicleChecklistsListViewModel> GetAllVehicleChecklists()
        {
            lock (locker)
            {
                var vehicleChecklists = new List<VehicleChecklistsListViewModel>();

                var q = database.Query<VehicleChecklistsListViewModel>(
                        "Select VC.Id, VC.CreatedDate, VC.Mileage, VC.Registration From VehicleChecklists VC"
                        + " Where VC.IsDeleted = ? And VC.SentToOffice = ? Order By VC.CreatedDate Desc", false, false).ToList();

                foreach (var item in q)
                {
                    vehicleChecklists.Add(new VehicleChecklistsListViewModel
                    {
                        Id = item.Id,
                        CreatedDate = item.CreatedDate,
                        Mileage = item.Mileage,
                        Registration = item.Registration
                    });
                }

                return vehicleChecklists;
            }
        }

        public VehicleChecklists GetVehicleChecklistById(Int32 id)
        {
            lock (locker)
            {
                return (from t in database.Table<VehicleChecklists>() select t).Where(c => c.Id == id).SingleOrDefault();
            }
        }

        public Boolean VehicleChecklistSentToOffice(Int32 vehicleChecklistId, Int32 officeId)
        {
            lock (locker)
            {
                var o = (from t in database.Table<VehicleChecklists>() select t).Where(c => c.Id == vehicleChecklistId).SingleOrDefault();

                o.SentToOffice = true;
                o.OfficeId = officeId;

                return database.Update(o) > 0;
            }
        }

        public Int32 InsertVehicleChecklist(VehicleChecklists model)
        {
            lock (locker)
            {
                var insert = database.Insert(model);

                return insert;
            }
        }

        public Int32 UpdateVehicleChecklist(VehicleChecklists model)
        {
            lock (locker)
            {
                var update = database.Update(model);

                return update;
            }
        }

        public Boolean DeleteVehicleChecklist(Int32 id)
        {
            lock (locker)
            {
                var o = (from t in database.Table<VehicleChecklists>() select t).Where(c => c.Id == id).SingleOrDefault();

                return database.Delete(o) > 0;
            }
        }
    }
}