using System;
using System.Collections.Generic;
using System.Text;

namespace JGRBuildingServices.ViewModels
{
    public class DailyTimesheetsListViewModel
    {
        public Int32 Id { get; set; }

        public DateTime Date { get; set; }
    }

    public class DailyTimesheetViewModel
    {
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

    public class DailyTimesheetLineItemsViewModel
    {
        public List<DailyTimesheetLineItemViewModel> DailyTimesheetLineItems { get; set; }
    }

    public class DailyTimesheetLineItemViewModel
    {
        public Int32? JobNumber { get; set; }

        public String SiteAddress { get; set; }

        public TimeSpan? StartJob { get; set; }

        public TimeSpan? FinishJob { get; set; }

        public TimeSpan? ArriveSite { get; set; }

        public TimeSpan? LeaveSite { get; set; }

        public Boolean Complete { get; set; }

        public String Comment { get; set; }

        public Decimal? JobHours { get; set; }
    }

    public class CreateDailyTimesheetResponseViewModel
    {
        public Int32 Id { get; set; }

        public String ErrorMessage { get; set; }
    }
}