using JGRBuildingServices.Models;
using JGRBuildingServices.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace JGRBuildingServices.Data
{
    public class PurchaseOrdersDatabaseController
    {
        static object locker = new object();

        SQLiteConnection database;

        public PurchaseOrdersDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();

            database.CreateTable<PurchaseOrders>();
        }

        public List<PurchaseOrdersListViewModel> GetAllPurchaseOrders()
        {
            lock (locker)
            {
                var purchaseOrders = new List<PurchaseOrdersListViewModel>();

                var q = database.Query<PurchaseOrdersListViewModel>(
                        "Select PO.Id, PO.CreatedDate, PO.Number From PurchaseOrders PO"
                        + " Where PO.IsDeleted = ? And PO.SentToOffice = ? Order By PO.CreatedDate Desc", false, false).ToList();

                foreach (var item in q)
                {
                    purchaseOrders.Add(new PurchaseOrdersListViewModel
                    {
                        Id = item.Id,
                        CreatedDate = item.CreatedDate,
                        Number = item.Number
                    });
                }

                return purchaseOrders;
            }
        }

        public PurchaseOrders GetPurchaseOrderById(Int32 id)
        {
            lock (locker)
            {
                return (from t in database.Table<PurchaseOrders>() select t).Where(c => c.Id == id).SingleOrDefault();
            }
        }

        public Boolean PurchaseOrderSentToOffice(Int32 purchaseOrderId, Int32 officeId)
        {
            lock (locker)
            {
                var o = (from t in database.Table<PurchaseOrders>() select t).Where(c => c.Id == purchaseOrderId).SingleOrDefault();

                o.SentToOffice = true;
                o.OfficeId = officeId;

                return database.Update(o) > 0;
            }
        }

        public Int32 InsertPurchaseOrder(PurchaseOrders model)
        {
            lock (locker)
            {
                var insert = database.Insert(model);

                return insert;
            }
        }

        public Int32 UpdatePurchaseOrder(PurchaseOrders model)
        {
            lock (locker)
            {
                var update = database.Update(model);

                return update;
            }
        }

        public Boolean DeletePurchaseOrder(Int32 id)
        {
            lock (locker)
            {
                var o = (from t in database.Table<PurchaseOrders>() select t).Where(c => c.Id == id).SingleOrDefault();

                return database.Delete(o) > 0;
            }
        }
    }
}