using System;
using System.Collections.Generic;
using System.Text;

namespace JGRBuildingServices.ViewModels
{
    public class CustomersViewModel
    {
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public String AddressLine1 { get; set; }

        public String AddressLine2 { get; set; }

        public String AddressLine3 { get; set; }

        public String AddressLine4 { get; set; }

        public String TownCity { get; set; }

        public String Postcode { get; set; }

        public Int32? StatusId { get; set; }
    }

    public class Status
    {
        public Int32? Id { get; set; }
        public String Name { get; set; }
        public String HexCode { get; set; }
    }
}