using System;
using System.Collections.Generic;
using System.Text;

namespace JGRBuildingServices.ViewModels
{
    public class SettingsViewModel
    {
        public SettingsViewModel()
        {

        }

        private List<StaffViewModel> _staffList { get; set; }

        public List<StaffViewModel> StaffList
        {
            get { return _staffList; }
            set
            {
                if (_staffList != value)
                {
                    _staffList = value;
                }
            }
        }
    }

    public class SettingViewModel
    {
        public Int32 Id { get; set; }

        public String Name { get; set; }

        public String Value { get; set; }
    }
}