using System;
using System.Collections.Generic;
using System.Text;

namespace JGRBuildingServices.ViewModels
{
    public class CreateAnnualLeaveRequestViewModel
    {
        public Int32 StaffId { get; set; }

        public DateTime Starting { get; set; }

        public Int32 StartingTime { get; set; }

        public DateTime Ending { get; set; }

        public Int32 EndingTime { get; set; }

        public String Notes { get; set; }

        public Double TotalDays { get; set; }

        public Guid RequestId { get; set; }
    }

    public class AnnualLeaveRequestsListViewModel
    {
        public Int32 Id { get; set; }

        public DateTime Starting { get; set; }

        public DateTime Ending { get; set; }

        public Double TotalDays { get; set; }
    }

    public class AnnualLeaveRequestsViewModel
    {
        public Int32 Id { get; set; }

        public Int32 StaffId { get; set; }

        public DateTime Starting { get; set; }

        public Int32 StartingTime { get; set; }

        public DateTime Ending { get; set; }

        public Int32 EndingTime { get; set; }

        public Int32? ActionedByUserId { get; set; }

        public String Notes { get; set; }

        public Double TotalDays { get; set; }

        public DateTime DateRequested { get; set; }

        public Boolean IsSynced { get; set; }

        public Guid RequestId { get; set; }

        public Int32 StatusId { get; set; }

        public String DeclineReason { get; set; }

        public String CancelReason { get; set; }

        public StaffMemberViewModel Staff { get; set; }
    }
}