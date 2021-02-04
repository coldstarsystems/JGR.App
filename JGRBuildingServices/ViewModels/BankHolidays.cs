using System;
using System.Collections.Generic;
using System.Text;

namespace JGRBuildingServices.ViewModels
{
    public class BankHolidaysViewModel
    {
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public DateTime BankHolidayDate { get; set; }

        public String Year { get; set; }
    }
}