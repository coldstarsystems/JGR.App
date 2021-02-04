using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace JGRBuildingServices.Models
{
    public class DailyTimesheets
    {
        [PrimaryKey, AutoIncrement]
        public Int32 Id { get; set; }

        public Int32 StaffId { get; set; }

        public DateTime Date { get; set; }

        public Int32 WeekNumber { get; set; }

        public String LineItems { get; set; }

        public Decimal TotalHours { get; set; }

        public Boolean CallOut { get; set; }

        public TimeSpan? LunchStartTime { get; set; }

        public TimeSpan? LunchEndTime { get; set; }

        public String MileageStart { get; set; }

        public String MileageEnd { get; set; }

        public String DailyMileage { get; set; }

        public Byte[] StaffSignature { get; set; }

        public Int32? OfficeId { get; set; }

        public Boolean IsDeleted { get; set; }

        public Boolean SentToOffice { get; set; }
    }
}