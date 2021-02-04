using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace JGRBuildingServices.Models
{
    public class VehicleChecklists
    {
        [PrimaryKey, AutoIncrement]
        public Int32 Id { get; set; }

        public Int32 StaffId { get; set; }

        public DateTime CreatedDate { get; set; }

        public String Information { get; set; }

        public String Comments { get; set; }

        public Byte[] StaffSignature { get; set; }

        public String Registration { get; set; }

        public String Mileage { get; set; }

        public Boolean IsDeleted { get; set; }

        public Int32? OfficeId { get; set; }

        public Boolean SentToOffice { get; set; }
    }
}