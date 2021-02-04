using JGRBuildingServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices.Views.AnnualLeave
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Approved : ContentPage
	{
        private Models.Staff staff;

        public Approved()
        {
            InitializeComponent();

            LoadInformation();
        }

        public void LoadInformation()
        {
            staff = App.StaffDatabase.GetStaffById(Convert.ToInt32(App.SettingsDatabase.GetSetting("AssignedStaffId").Value));

            AnnualLeaveBalance.Text = staff.AnnualLeaveBalance.ToString();
            AnnualLeaveEntitlement.Text = "Your annual leave entitlement is " + staff.AnnualLeaveAllowance.ToString() + " days, excluding English bank holidays, from Janurary 1st to December 31st.";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var annualLeaveRequestsApprovedList = new ObservableCollection<AnnualLeaveRequestsListViewModel>();

            AnnualLeaveRequestsApprovedList.ItemsSource = annualLeaveRequestsApprovedList;

            var annualLeaveRequestsPending = App.AnnualLeaveDatabase.GetApprovedAnnualLeaveRequests(staff.Id);

            foreach (var item in annualLeaveRequestsPending)
            {
                annualLeaveRequestsApprovedList.Add(new AnnualLeaveRequestsListViewModel()
                {
                    Id = item.Id,
                    Starting = item.Starting,
                    Ending = item.Ending,
                    TotalDays = item.TotalDays
                });
            }
        }

        private void AnnualLeaveRequestsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var annualLeaveRequestId = ((AnnualLeaveRequestsListViewModel)e.SelectedItem).Id;

            Navigation.PushAsync(new Edit(annualLeaveRequestId));
        }
    }
}