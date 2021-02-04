using JGRBuildingServices.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices.Views.AnnualLeave
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Edit : ContentPage
	{
        private String _url = "https://api.jgr1.com";

        public Edit()
		{
			InitializeComponent();
        }

        public Edit(Int32 id)
        {
            InitializeComponent();

            BindTimesPicker();
            LoadInformation(id);            
        }

        public void LoadInformation(Int32 id)
        {
            Id.Text = id.ToString();

            var staff = App.StaffDatabase.GetStaffById(Convert.ToInt32(App.SettingsDatabase.GetSetting("AssignedStaffId").Value));

            var staffInfo = App.StaffDatabase.GetStaffById(staff.Id);

            var annualLeaveInfo = App.AnnualLeaveDatabase.GetAnnualLeaveRequestById(id);

            if (annualLeaveInfo.StatusId != 1 || annualLeaveInfo.SentToOffice == true)
            {
                CallToActions_StackLayout.IsVisible = false;

                RequestingDays.Text = "You requested " + annualLeaveInfo.TotalDays.ToString() + " day(s) annual leave.";                
            }
            else
            {
                RequestingDays.Text = "You are currently requesting " + annualLeaveInfo.TotalDays.ToString() + " day(s) annual leave.";
            }

            AnnualLeaveBalance.Text = staffInfo.AnnualLeaveBalance.ToString();
            Notes.Text = annualLeaveInfo.Notes;

            StartDate.Date = annualLeaveInfo.Starting;
            EndDate.Date = annualLeaveInfo.Ending;

            if (annualLeaveInfo.StartingTime == 1)
            {
                StartTime.SelectedItem = "Morning";
            }
            else
            {
                StartTime.SelectedItem = "Afternoon";
            }

            if (annualLeaveInfo.EndingTime == 1)
            {
                EndTime.SelectedItem = "Afternoon";
            }
            else
            {
                EndTime.SelectedItem = "End of Day";
            }
        }

        private async void SaveAnnualLeaveRequest_ClickedAsync(object sender, EventArgs e)
        {
            ApplyActivityIndicator();

            SaveDailyAnnualLeaveRequestToDatabase(true);

            ResetActivityIndicator();
        }

        private async void SaveDailyAnnualLeaveRequestToDatabase(Boolean displayAlert)
        {
            try
            {
                var annualLeaveRequestId = Convert.ToInt32(Id.Text);

                if (displayAlert)
                {
                    await DisplayAlert("Saved", "This annual leave request has been saved successfully.", "Acknowledge");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "An error has occurred. " + ex.Message, "Acknowledge");
            }
        }

        private void BindTimesPicker()
        {
            var startTime = StartTime;
            var endTime = EndTime;

            var startTimeList = new List<String>
            {
                "Morning",
                "Afternoon"
            };

            var endTimeList = new List<String>
            {
                "Afternoon",
                "End of Day"
            };

            startTime.ItemsSource = startTimeList;
            endTime.ItemsSource = endTimeList;
        }

        private void ApplyActivityIndicator()
        {
            ActivityIndicator_StackLayout.IsVisible = true;
            CallToActions_StackLayout.IsVisible = false;
        }

        private void ResetActivityIndicator()
        {
            ActivityIndicator_StackLayout.IsVisible = false;
            CallToActions_StackLayout.IsVisible = true;
        }

        private async void DeleteAnnualLeaveRequest_Clicked(object sender, EventArgs e)
        {
            ApplyActivityIndicator();

            var confirmed = await DisplayAlert("Confirm", "Are you sure want to delete this annual leave request?", "Yes", "No");

            if (confirmed)
            {
                App.AnnualLeaveDatabase.DeleteAnnualLeaveRequest(Convert.ToInt32(Id.Text));

                await Navigation.PopAsync(true);
            }
            else
            {
                ResetActivityIndicator();
            }
        }

        private async void SendToOffice_Clicked(object sender, EventArgs e)
        {
            ApplyActivityIndicator();

            SaveDailyAnnualLeaveRequestToDatabase(false);

            var isConfirmed = false;

            var helpers = new Helpers();

            if (helpers.IsDeviceOnline())
            {
                isConfirmed = await DisplayAlert("Confirm", "Are you sure you want to submit this annual leave request?", "Yes", "Cancel");
            }
            else
            {
                ResetActivityIndicator();

                await DisplayAlert("No Internet Connection", "You need to be connected to the internet in order to submit this annual leave request.", "Acknowledge");
            }

            if (isConfirmed)
            {
                var annualLeaveRequest = App.AnnualLeaveDatabase.GetAnnualLeaveRequestById(Convert.ToInt32(Id.Text));

                var client = new HttpClient
                {
                    BaseAddress = new Uri(_url),
                };

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var ma = new CreateAnnualLeaveRequestViewModel()
                {
                    StaffId = annualLeaveRequest.StaffId,
                    Ending = annualLeaveRequest.Ending,
                    EndingTime = annualLeaveRequest.EndingTime,
                    Notes = annualLeaveRequest.Notes,
                    Starting = annualLeaveRequest.Starting,
                    RequestId = annualLeaveRequest.RequestId,
                    StartingTime = annualLeaveRequest.StartingTime,
                    TotalDays = annualLeaveRequest.TotalDays,
                };

                var jsonConvert = JsonConvert.SerializeObject(ma).ToString();

                var response = await client.PostAsync("PostAnnualLeaveRequest", new StringContent(jsonConvert, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    App.AnnualLeaveDatabase.AnnualLeaveSentToOffice(annualLeaveRequest.Id);

                    await DisplayAlert("Success", "Your annual leave request has been successfully sent to the office.", "Back to Annual Leave");

                    await Navigation.PushAsync(new AnnualLeaveTabbed());
                }
                else
                {
                    ResetActivityIndicator();

                    await DisplayAlert("Error", "An error has occurred sending this annual leave request. Please contact support.", "Acknowledge");
                }
            }
            else
            {
                ResetActivityIndicator();
            }
        }

        private void StartTime_SelectedIndexChanged(object sender, EventArgs e)
        {
            var startTime = StartTime;
            var endTime = EndTime;

            if ((String)startTime.SelectedItem == "Afternoon")
            {
                endTime.ItemsSource.Clear();

                var endTimeList = new List<String>
                {
                    "End of Day"
                };

                endTime.ItemsSource = endTimeList;
                endTime.SelectedItem = "End of Day";
            }
            else
            {
                endTime.ItemsSource.Clear();

                var endTimeList = new List<String>
                {
                    "Afternoon",
                    "End of Day"
                };

                endTime.ItemsSource = endTimeList;
                endTime.SelectedItem = "End of Day";
            }
        }
    }
}