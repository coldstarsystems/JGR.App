using System;
using System.Collections.Generic;
using System.Text;

namespace JGRBuildingServices.ViewModels
{
    public class StaffViewModel
    {
        public Int32 Id { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String FullName { get { return FirstName + " " + LastName; } }

        public Double AnnualLeaveBalance { get; set; }
    }

    public class StaffMemberViewModel
    {
        public Int32 Id { get; set; }

        public String FirstName { get; set; }

        public String LastName { get; set; }

        public String EmailAddress { get; set; }

        public String MobileNumber { get; set; }

        public DateTime DateOfBirth { get; set; }

        public Boolean IsDeleted { get; set; }

        public Double AnnualLeaveAllowance { get; set; }

        public Double AnnualLeaveBalance { get; set; }
    }
}