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
    public class BankHolidaysDatabaseController
    {
        static object locker = new object();

        SQLiteConnection database;

        public BankHolidaysDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();

            database.CreateTable<BankHolidays>();
        }

        public IQueryable<BankHolidaysViewModel> GetAllBankHolidays(String year)
        {
            lock (locker)
            {
                var o = database.Table<BankHolidays>().Select(c => new BankHolidaysViewModel()
                {
                    Id = c.Id,
                    BankHolidayDate = c.BankHolidayDate,
                    Name = c.Name,
                    Year = c.Year
                }).Where(c => c.Year == year).AsQueryable();

                return o;
            }
        }

        public void InsertAllBankHolidays(List<BankHolidays> model)
        {
            lock (locker)
            {
                database.InsertAll(model);
            }
        }

        public void DeleteAllBankHolidays()
        {
            lock (locker)
            {
                database.DeleteAll<BankHolidays>();
            }
        }
    }
}