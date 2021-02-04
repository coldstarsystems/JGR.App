using System;
using System.Collections.Generic;
using System.Text;

namespace JGRBuildingServices.ViewModels
{
    public class PurchaseOrdersListViewModel
    {
        public Int32 Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public String Number { get; set; }
    }

    public class PurchaseOrderViewModel
    {
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

    public class PurchaseOrderLineItemsViewModel
    {
        public List<PurchaseOrderLineItemViewModel> LineItems { get; set; }
    }

    public class PurchaseOrderLineItemViewModel
    {
        public Decimal? Quantity { get; set; }

        public String Item { get; set; }

        public String Description { get; set; }

        public String Job { get; set; }

        public Decimal? UnitPrice { get; set; }

        public Decimal? LineTotal { get; set; }
    }

    public class CreatePurchaseOrderResponseViewModel
    {
        public Int32 Id { get; set; }

        public String ErrorMessage { get; set; }
    }
}