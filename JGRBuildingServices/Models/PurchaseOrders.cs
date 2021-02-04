using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace JGRBuildingServices.Models
{
    public class PurchaseOrders
    {
        [PrimaryKey, AutoIncrement]
        public Int32 Id { get; set; }

        public String Number { get; set; }

        public Int32 StaffId { get; set; }

        public DateTime CreatedDate { get; set; }

        public String Supplier { get; set; }

        public String ShipTo { get; set; }

        public String ShippingMethod { get; set; }

        public String ShippingTerms { get; set; }

        public DateTime? DeliveryDate { get; set; }

        public String LineItems { get; set; }

        public Decimal? SubTotal { get; set; }

        public Decimal? VAT { get; set; }

        public Decimal? Total { get; set; }

        public String InvoiceNumber { get; set; }

        public Decimal? InvoiceAmount { get; set; }

        public Boolean IsDeleted { get; set; }

        public Int32? OfficeId { get; set; }

        public Boolean SentToOffice { get; set; }
    }
}