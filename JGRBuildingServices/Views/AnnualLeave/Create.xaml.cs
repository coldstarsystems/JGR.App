using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices.Views.AnnualLeave
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Create : ContentPage
    {
        public Create()
        {
            InitializeComponent();

            LoadInformation();
            BindTimesPicker();
        }

        public void LoadInformation()
        {
            var staff = App.StaffDatabase.GetStaffById(Convert.ToInt32(App.SettingsDatabase.GetSetting("AssignedStaffId").Value));

            var staffInfo = App.StaffDatabase.GetStaffById(staff.Id);

            AnnualLeaveBalance.Text = staffInfo.AnnualLeaveBalance.ToString();
        }

        private async void SaveAnnualLeaveRequest_ClickedAsync(object sender, EventArgs e)
        {
            try
            {
                ApplyActivityIndicator();

                var staff = App.StaffDatabase.GetStaffById(Convert.ToInt32(App.SettingsDatabase.GetSetting("AssignedStaffId").Value));

                var startDate = StartDate.Date;
                var endDate = EndDate.Date;

                var m = new Models.AnnualLeave
                {
                    StaffId = staff.Id,
                    CancelReason = null,
                    DateRequested = DateTime.Now,
                    Ending = endDate,
                    Notes = Notes.Text,
                    RequestId = Guid.NewGuid(),
                    Starting = startDate,
                    StatusId = 1,
                    DeclineReason = null,
                    SentToOffice = false,
                    EndingTime = 1,
                    StartingTime = 1,
                    IsSynced = false,
                    TotalDays = 0
                };

                var startTime = StartTime;
                var endTime = EndTime;

                if ((String)startTime.SelectedItem == "Morning")
                {
                    m.StartingTime = 1;
                }
                else
                {
                    m.StartingTime = 2;
                }

                if ((String)endTime.SelectedItem == "Afternoon")
                {
                    m.EndingTime = 1;
                }
                else
                {
                    m.EndingTime = 2;
                }

                var totalWholeDays = Weeks.GetBusinessDays(m.Starting, m.Ending);
                var bankHolidays = Weeks.CountBankHolidays(m.Starting, m.Ending);

                if (m.StartingTime == 1 && m.EndingTime == 1)
                {
                    m.TotalDays = (totalWholeDays - .5);
                }
                else if (m.StartingTime == 1 && m.EndingTime == 2)
                {
                    m.TotalDays = totalWholeDays;
                }
                else if (m.StartingTime == 2 && m.EndingTime == 2)
                {
                    m.TotalDays = (totalWholeDays - .5);
                }
                else
                {
                    m.TotalDays = totalWholeDays;
                }

                m.TotalDays = (m.TotalDays - bankHolidays);

                if (staff.AnnualLeaveBalance < m.TotalDays)
                {
                    ResetActivityIndicator();

                    await DisplayAlert("Error", "You currently do not have enough annual leave remaining.", "Acknowledge");
                }
                else if (m.Starting > m.Ending)
                {
                    ResetActivityIndicator();

                    await DisplayAlert("Error", "Ending date must be greater than the starting date.", "Acknowledge");
                }
                else
                {
                    if (App.AnnualLeaveDatabase.InsertAnnualLeaveRequest(m))
                    {
                        await Navigation.PushAsync(new Edit(m.Id));
                    }
                    else
                    {
                        ResetActivityIndicator();

                        await DisplayAlert("Error", "An error has occurred creating this annual leave request. Please contact support.", "Acknowledge");
                    }
                }
            }
            catch (Exception ex)
            {

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

            startTime.SelectedItem = "Morning";
            endTime.SelectedItem = "End of Day";
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