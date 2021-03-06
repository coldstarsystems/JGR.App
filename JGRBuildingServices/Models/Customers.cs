﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace JGRBuildingServices.Models
{
    public class Customers
    {
        [PrimaryKey]
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public String AddressLine1 { get; set; }

        public String AddressLine2 { get; set; }

        public String AddressLine3 { get; set; }

        public String AddressLine4 { get; set; }

        public String TownCity { get; set; }

        public String County { get; set; }

        public String Postcode { get; set; }

        public Boolean IsActive { get; set; }

        public Boolean IsDeleted { get; set; }
    }
}