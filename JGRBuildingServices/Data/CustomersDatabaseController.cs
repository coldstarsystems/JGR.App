using JGRBuildingServices.Models;
using JGRBuildingServices.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace JGRBuildingServices.Data
{
    public class CustomersDatabaseController
    {
        static object locker = new object();

        SQLiteConnection database;

        public CustomersDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();

            database.CreateTable<Customers>();
        }

        public void InsertAllCustomers(List<Customers> model)
        {
            lock (locker)
            {
                database.InsertAll(model);
            }
        }

        public void DeleteAllCustomers()
        {
            lock (locker)
            {
                database.DeleteAll<Customers>();
            }
        }

        public CustomersViewModel GetCustomerById(Int32 id)
        {
            lock (locker)
            {
                var o = (from t in database.Table<Customers>()
                         select new CustomersViewModel()
                         {
                             Id = t.Id,
                             Name = t.Name,
                             AddressLine1 = t.AddressLine1,
                             AddressLine2 = t.AddressLine2,
                             AddressLine3 = t.AddressLine3,
                             AddressLine4 = t.AddressLine4,
                             TownCity = t.TownCity,
                             Postcode = t.Postcode
                         }).Where(c => c.Id == id).Single();

                return o;
            }
        }

        public IQueryable<CustomersViewModel> GetAllCustomers()
        {
            lock (locker)
            {
                var o = (from t in database.Table<Customers>() select new CustomersViewModel()
                {
                    Id = t.Id,
                    Name = t.Name,
                    AddressLine1 = t.AddressLine1,
                    AddressLine2 = t.AddressLine2,
                    AddressLine3 = t.AddressLine3,
                    AddressLine4 = t.AddressLine4,
                    TownCity = t.TownCity,
                    Postcode = t.Postcode
                }).OrderBy(i => i.Name).AsQueryable();

                return o;
            }
        }
    }
}