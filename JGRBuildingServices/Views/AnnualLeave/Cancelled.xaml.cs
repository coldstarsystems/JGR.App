﻿using JGRBuildingServices.ViewModels;
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
	public partial class Cancelled : ContentPage
	{
        private String _url = "https://api.jgr1.com";
        private Models.Staff staff;

        public Cancelled()
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

            var annualLeaveRequestsCancelledList = new ObservableCollection<AnnualLeaveRequestsListViewModel>();

            AnnualLeaveRequestsCancelledList.ItemsSource = annualLeaveRequestsCancelledList;

            var annualLeaveRequestsPending = App.AnnualLeaveDatabase.GetCancelledAnnualLeaveRequests(staff.Id);

            foreach (var item in annualLeaveRequestsPending)
            {
                annualLeaveRequestsCancelledList.Add(new AnnualLeaveRequestsListViewModel()
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