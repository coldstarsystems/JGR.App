using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace JGRBuildingServices.Models
{
    public class Staff
    {
        [PrimaryKey]
        public Int32 Id { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public Double AnnualLeaveBalance { get; set; }

        public Double AnnualLeaveAllowance { get; set; }

        public Int32 NextPurchaseOrderNumber { get; set; }

        public String PurchaseOrderIdentifier { get; set; }

        public String Position { get; set; }
    }
}