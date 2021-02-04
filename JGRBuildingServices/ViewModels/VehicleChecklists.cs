using System;
using System.Collections.Generic;
using System.Text;

namespace JGRBuildingServices.ViewModels
{
    public class VehicleChecklistsListViewModel
    {
        public Int32 Id { get; set; }

        public String Mileage { get; set; }

        public String Registration { get; set; }

        public DateTime CreatedDate { get; set; }
    }

    public class VehicleChecklistViewModel
    {
        public Int32 Id { get; set; }

        public Int32 StaffId { get; set; }

        public DateTime CreatedDate { get; set; }

        public String Registration { get; set; }

        public String Mileage { get; set; }

        public Boolean IsDeleted { get; set; }

        public String Information { get; set; }

        public String Comments { get; set; }

        public Byte[] StaffSignature { get; set; }
    }

    public class VehicleChecklistIssuesObjectViewModel
    {
        public List<VehicleChecklistIssuesViewModel> Issues { get; set; }
    }

    public class VehicleChecklistIssuesViewModel
    {
        public String IssueName { get; set; }

        public String Condition { get; set; }

        public String Comments { get; set; }

        public String IssueCategory { get; set; }
    }

    public class CreateVehicleChecklistResponseViewModel
    {
        public Int32 Id { get; set; }

        public String ErrorMessage { get; set; }
    }
}