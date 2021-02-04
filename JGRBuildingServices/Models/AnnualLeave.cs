using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JGRBuildingServices.Models
{
    public class AnnualLeave
    {
        [PrimaryKey, AutoIncrement]
        public Int32 Id { get; set; }

        public Int32 StaffId { get; set; }

        public DateTime Starting { get; set; }

        public Int32 StartingTime { get; set; }

        public DateTime Ending { get; set; }

        public Int32 EndingTime { get; set; }

        public String Notes { get; set; }

        public Double TotalDays { get; set; }

        public DateTime DateRequested { get; set; }

        public Int32 StatusId { get; set; }

        public String CancelReason { get; set; }

        public String DeclineReason { get; set; }

        public Guid RequestId { get; set; }

        public Boolean IsSynced { get; set; }

        public Boolean SentToOffice { get; set; }
    }
}