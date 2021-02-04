using JGRBuildingServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices.Views.Settings
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Settings : ContentPage
	{
		public Settings()
		{
			InitializeComponent();

            BindingContext = new SettingsViewModel()
            {
                StaffList = App.StaffDatabase.GetAllStaff().Select(c => new StaffViewModel() { Id = c.Id, FirstName = c.FirstName, LastName = c.LastName }).ToList()
            };

            GetSettings();
        }

        public void GetSettings()
        {
            var assignedStaffId = App.SettingsDatabase.GetSetting("AssignedStaffId");

            if (assignedStaffId != null)
            {
                var staffId = Convert.ToInt32(assignedStaffId.Value);

                StaffList.SelectedItem = (BindingContext as SettingsViewModel).StaffList.FirstOrDefault(c => c.Id == staffId);
            }
        }

        private void SaveSettings_Clicked(object sender, EventArgs e)
        {
            var staffId = (StaffViewModel)StaffList.SelectedItem;

            if (App.SettingsDatabase.SaveSetting("AssignedStaffId", staffId.Id.ToString()))
            {
                DisplayAlert("Saved", "This settings have been saved successfully.", "Acknowledge");
            }
            else
            {
                DisplayAlert("Error", "This settings have not been saved.", "Acknowledge");
            }
        }
    }
}