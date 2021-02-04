using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace JGRBuildingServices.Models
{
    public class HotWorksPermit
    {
        [PrimaryKey, AutoIncrement]
        public Int32 Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public Int32 StaffId { get; set; }

        public String Content { get; set; }

        public Int32? OfficeId { get; set; }

        public Boolean IsDeleted { get; set; }

        public Boolean SentToOffice { get; set; }
    }
}
