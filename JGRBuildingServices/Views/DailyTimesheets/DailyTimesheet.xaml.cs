using JGRBuildingServices.ViewModels;
using Newtonsoft.Json;
using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices.Views.DailyTimesheets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DailyTimesheet : ContentPage
    {
        static readonly String _url = "https://api.jgr1.com";

        public DailyTimesheet()
        {
            InitializeComponent();
        }

        public DailyTimesheet(Int32 id)
        {
            InitializeComponent();

            GetData(id);
        }

        private void GetData(Int32 id)
        {
            var dailyTimesheet = App.DailyTimesheetsDatabase.GetDailyTimesheetById(id);

            Id.Text = id.ToString();

            if (dailyTimesheet.SentToOffice)
            {
                Delete.IsVisible = false;
                SendToOffice.IsVisible = false;
                Save.IsVisible = false;
            }
            else
            {
                Delete.IsVisible = true;
                SendToOffice.IsVisible = true;
                Save.IsVisible = true;
            }

            Date_DatePicker.Date = dailyTimesheet.Date;
            TotalHours_Entry.Text = dailyTimesheet.TotalHours.ToString();
            CallOut_CheckBox.IsChecked = dailyTimesheet.CallOut;

            if (dailyTimesheet.LunchStartTime != null)
            {
                LunchStartTime_TimePicker.Time = (TimeSpan)dailyTimesheet.LunchStartTime;
            }

            if (dailyTimesheet.LunchEndTime != null)
            {
                LunchEndTime_TimePicker.Time = (TimeSpan)dailyTimesheet.LunchEndTime;
            }

            MileageStart_Entry.Text = dailyTimesheet.MileageStart;
            MileageEnd_Entry.Text = dailyTimesheet.MileageEnd;
            DailyMileage_Entry.Text = dailyTimesheet.DailyMileage;

            StaffSign_StackLayout.IsVisible = false;
            StaffSignImage_StackLayout.IsVisible = false;

            if (dailyTimesheet.StaffSignature != null)
            {
                StaffSignImage_StackLayout.IsVisible = true;
                StaffSignImage.Source = ImageSource.FromStream(() => new MemoryStream(dailyTimesheet.StaffSignature));
            }
            else
            {
                StaffSign_StackLayout.IsVisible = true;
            }

            var lineItems = JsonConvert.DeserializeObject<DailyTimesheetLineItemsViewModel>(dailyTimesheet.LineItems);

            if (lineItems.DailyTimesheetLineItems != null)
            {
                ListItems.Children.Clear();

                foreach (var item in lineItems.DailyTimesheetLineItems)
                {
                    ListItems.Children.Add(new ListItems(item)
                    {

                    });
                }
            }
        }

        private void AddMoreButton_Clicked(object sender, EventArgs e)
        {
            ListItems.Children.Add(new ListItems()
            {

            });
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            SaveDailyTimesheetToDatabase(true);
        }

        private async void SaveDailyTimesheetToDatabase(Boolean displayAlert)
        {
            try
            {
                ApplyActivityIndicator();

                var staff = App.StaffDatabase.GetStaffById(Convert.ToInt32(App.SettingsDatabase.GetSetting("AssignedStaffId").Value));
                var dailyTimesheet = App.DailyTimesheetsDatabase.GetDailyTimesheetById(Convert.ToInt32(Id.Text));

                var timeSpan = new TimeSpan(0, 0, 0, 0, 0);

                var m = new Models.DailyTimesheets()
                {
                    DailyMileage = DailyMileage_Entry.Text,
                    Date = Date_DatePicker.Date,
                    IsDeleted = false,
                    LineItems = null,
                    LunchEndTime = null,
                    LunchStartTime = null,
                    MileageEnd = MileageEnd_Entry.Text,
                    MileageStart = MileageStart_Entry.Text,
                    OfficeId = null,
                    SentToOffice = false,
                    StaffId = staff.Id,
                    StaffSignature = null,
                    CallOut = CallOut_CheckBox.IsChecked,
                    TotalHours = 0,
                    WeekNumber = Weeks.GetISO8601WeekOfYear(Date_DatePicker.Date)
                };

                if (LunchEndTime_TimePicker.Time != timeSpan)
                {
                    m.LunchEndTime = LunchEndTime_TimePicker.Time;
                }

                if (LunchStartTime_TimePicker.Time != timeSpan)
                {
                    m.LunchStartTime = LunchStartTime_TimePicker.Time;
                }

                #region Line Items

                Decimal? totalHours = 0;

                var lineItems = new List<DailyTimesheetLineItemViewModel>();

                foreach (var item in ListItems.Children)
                {
                    if (item.GetType() == typeof(ListItems))
                    {
                        var items = (ListItems)item;

                        var section = items.GetItems();

                        if (section.JobNumber != 0 && section.JobHours != 0)
                        {
                            lineItems.Add(new DailyTimesheetLineItemViewModel()
                            {
                                ArriveSite = section.ArriveSite,
                                SiteAddress = section.SiteAddress,
                                Comment = section.Comment,
                                Complete = section.Complete,
                                FinishJob = section.FinishJob,
                                JobHours = section.JobHours,
                                JobNumber = section.JobNumber,
                                LeaveSite = section.LeaveSite,
                                StartJob = section.StartJob
                            });

                            totalHours = totalHours + section.JobHours;
                        }
                    }
                }

                m.TotalHours = totalHours ?? 0;

                m.LineItems = JsonConvert.SerializeObject(new
                {
                    DailyTimesheetLineItems = lineItems
                });

                #endregion

                if (dailyTimesheet != null)
                {
                    if (dailyTimesheet.StaffSignature != null)
                    {
                        m.StaffSignature = dailyTimesheet.StaffSignature;
                    }
                    else
                    {
                        if (StaffSign != null)
                        {
                            StaffSign_StackLayout.IsVisible = true;
                            var image = await StaffSign.GetImageStreamAsync(SignatureImageFormat.Png);

                            if (image != null)
                            {
                                byte[] arr;

                                using (var ms = new MemoryStream())
                                {
                                    await image.CopyToAsync(ms);

                                    arr = ms.ToArray();
                                }

                                m.StaffSignature = arr;
                            }
                        }
                    }
                }
                else
                {
                    if (StaffSign != null)
                    {
                        var image = await StaffSign.GetImageStreamAsync(SignatureImageFormat.Png);

                        if (image != null)
                        {
                            byte[] arr;

                            using (var ms = new MemoryStream())
                            {
                                await image.CopyToAsync(ms);

                                arr = ms.ToArray();
                            }

                            m.StaffSignature = arr;
                        }
                    }
                }

                if (!String.IsNullOrEmpty(Id.Text))
                {
                    m.Id = Convert.ToInt32(Id.Text);

                    App.DailyTimesheetsDatabase.UpdateDailyTimesheet(m);

                    if (displayAlert)
                    {
                        ResetActivityIndicator();

                        await DisplayAlert("Saved", "This daily timesheet has been saved successfully.", "Acknowledge");
                    }
                }
                else
                {
                    App.DailyTimesheetsDatabase.InsertDailyTimesheet(m);

                    await Navigation.PushAsync(new DailyTimesheet(m.Id));

                    Navigation.RemovePage(this);
                }
            }
            catch (Exception ex)
            {
                ResetActivityIndicator();

                await DisplayAlert("Error", "An error has occurred. " + ex.Message, "Acknowledge");
            }
        }

        private async void SendToOffice_Clicked(object sender, EventArgs e)
        {
            SaveDailyTimesheetToDatabase(false);

            var isConfirmed = false;

            var helpers = new Helpers();

            if (helpers.IsDeviceOnline())
            {
                isConfirmed = await DisplayAlert("Confirm", "Are you sure you want to submit this daily timesheet?", "Yes", "Cancel");
            }
            else
            {
                ResetActivityIndicator();

                await DisplayAlert("No Internet Connection", "You need to be connected to the internet in order to submit this daily timesheet.", "Acknowledge");
            }

            if (isConfirmed)
            {
                var dailyTimesheet = App.DailyTimesheetsDatabase.GetDailyTimesheetById(Convert.ToInt32(Id.Text));

                var client = new HttpClient
                {
                    BaseAddress = new Uri(_url),
                };

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var ma = new DailyTimesheetViewModel()
                {
                    DailyMileage = dailyTimesheet.DailyMileage,
                    Date = dailyTimesheet.Date,
                    IsDeleted = false,
                    LineItems = dailyTimesheet.LineItems,
                    LunchEndTime = dailyTimesheet.LunchEndTime,
                    LunchStartTime = dailyTimesheet.LunchStartTime,
                    MileageEnd = dailyTimesheet.MileageEnd,
                    MileageStart = dailyTimesheet.MileageStart,
                    SentToOffice = false,
                    StaffId = dailyTimesheet.StaffId,
                    StaffSignature = dailyTimesheet.StaffSignature,
                    CallOut = dailyTimesheet.CallOut,
                    TotalHours = dailyTimesheet.TotalHours,
                    WeekNumber = dailyTimesheet.WeekNumber
                };

                var jsonObject = JsonConvert.SerializeObject(ma).ToString();

                var response = await client.PostAsync("PostDailyTimesheet", new StringContent(jsonObject, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var contents = await response.Content.ReadAsStringAsync();

                    var jsonResponse = JsonConvert.DeserializeObject<CreateDailyTimesheetResponseViewModel>(contents);

                    if (jsonResponse.Id == -1)
                    {
                        ResetActivityIndicator();

                        await DisplayAlert("Error", jsonResponse.ErrorMessage, "Acknowledge");
                    }
                    else
                    {
                        App.DailyTimesheetsDatabase.DailyTimesheetsentToOffice(dailyTimesheet.Id, jsonResponse.Id);

                        await DisplayAlert("Success", "Your daily timesheet has been successfully sent to the office.", "Back to Daily Timesheets");

                        await Navigation.PopAsync(true);
                    }
                }
                else
                {
                    ResetActivityIndicator();

                    await DisplayAlert("Error", "An error has occurred.", "Acknowledge");
                }
            }
            else
            {
                ResetActivityIndicator();
            }
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            ApplyActivityIndicator();

            var confirmed = await DisplayAlert("Confirm", "Are you sure want to delete this daily timesheet?", "Yes", "No");

            if (confirmed)
            {
                App.DailyTimesheetsDatabase.DeleteDailyTimesheet(Convert.ToInt32(Id.Text));

                await Navigation.PopAsync(true);
            }
            else
            {
                ResetActivityIndicator();
            }
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
    }
}