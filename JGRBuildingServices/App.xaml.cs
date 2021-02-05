using JGRBuildingServices.Data;
using JGRBuildingServices.Models;
using JGRBuildingServices.ViewModels;
using Microsoft.AppCenter.Distribute;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace JGRBuildingServices
{
    public partial class App : Application
    {
        public static String _buildNum { get; set; }
        static readonly String _url = "https://api.jgr1.com";

        static AnnualLeaveDatabaseController annualLeaveDatabase;
        static BankHolidaysDatabaseController bankHolidaysDatabase;
        static StaffDatabaseController staffDatabase;
        static SettingsDatabaseController settingsDatabase;
        static CustomersDatabaseController customersDatabase;
        static DailyTimesheetsDatabaseController dailyTimesheetsDatabase;
        static PurchaseOrdersDatabaseController purchaseOrdersDatabase;
        static VehicleChecklistsDatabaseController vehicleChecklistsDatabase;
        static JobSheetsDatabaseController jobSheetsDatabase;
        static HotWorksPermitDatabaseController hotWorksPermitDatabase;

        public App()
        {
            InitializeComponent();

            MainPage = new Views.Dashboard.MainPage();

            _buildNum = "Changes V1.8.0";

            Distribute.CheckForUpdate();
        }

        protected override void OnStart()
        {
            var helpers = new Helpers();

            if (helpers.IsDeviceOnline())
            {
                SyncStaffDatabase();
                SyncCustomersDatabase();
                SyncAnnualLeaveRequests();
                App.SettingsDatabase.SaveSetting("LastSynced", DateTime.Now.ToShortDateString());
            }
        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {

        }

        #region Syncs

        public static void SyncAnnualLeaveRequests()
        {
            var assignedStaffId = App.SettingsDatabase.GetSetting("AssignedStaffId");

            if (assignedStaffId != null)
            {
                var client = new HttpClient
                {
                    BaseAddress = new Uri(_url),
                    Timeout = TimeSpan.FromSeconds(60)
                };

                var response = client.GetAsync("GetAnnualLeaveRequests?StaffId=" + assignedStaffId.Value + "&format=json").Result;

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<List<AnnualLeaveRequestsViewModel>>(response.Content.ReadAsStringAsync().Result);

                    foreach (var item in result)
                    {
                        var annualLeaveRequest = new AnnualLeave()
                        {
                            CancelReason = item.CancelReason,
                            DateRequested = item.DateRequested,
                            Ending = item.Ending,
                            EndingTime = item.EndingTime,
                            Id = item.Id,
                            IsSynced = item.IsSynced,
                            Notes = item.Notes,
                            RequestId = item.RequestId,
                            StaffId = item.StaffId,
                            Starting = item.Starting,
                            StartingTime = item.StartingTime,
                            TotalDays = item.TotalDays,
                            StatusId = item.StatusId,
                            DeclineReason = item.DeclineReason
                        };

                        App.AnnualLeaveDatabase.InsertUpdateAnnualLeaveRequest(annualLeaveRequest);
                    }
                }
            }
        }

        public static void SyncCustomersDatabase()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(_url),
                Timeout = TimeSpan.FromSeconds(60)
            };

            var response = client.GetAsync("GetCustomers?format=json").Result;

            if (response.IsSuccessStatusCode)
            {
                App.CustomersDatabase.DeleteAllCustomers();

                var result = JsonConvert.DeserializeObject<List<Customers>>(response.Content.ReadAsStringAsync().Result);

                App.CustomersDatabase.InsertAllCustomers(result);
            }
        }

        public static void SyncStaffDatabase()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(_url),
                Timeout = TimeSpan.FromSeconds(60)
            };

            var response = client.GetAsync("GetStaff?format=json").Result;

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<List<Staff>>(response.Content.ReadAsStringAsync().Result);

                App.StaffDatabase.InsertAllStaff(result);
            }
        }

        #endregion

        #region Database Controllers

        public static CustomersDatabaseController CustomersDatabase
        {
            get
            {
                if (customersDatabase == null)
                {
                    customersDatabase = new CustomersDatabaseController();
                }

                return customersDatabase;
            }
        }

        public static AnnualLeaveDatabaseController AnnualLeaveDatabase
        {
            get
            {
                if (annualLeaveDatabase == null)
                {
                    annualLeaveDatabase = new AnnualLeaveDatabaseController();
                }

                return annualLeaveDatabase;
            }
        }

        public static BankHolidaysDatabaseController BankHolidaysDatabase
        {
            get
            {
                if (bankHolidaysDatabase == null)
                {
                    bankHolidaysDatabase = new BankHolidaysDatabaseController();
                }

                return bankHolidaysDatabase;
            }
        }

        public static StaffDatabaseController StaffDatabase
        {
            get
            {
                if (staffDatabase == null)
                {
                    staffDatabase = new StaffDatabaseController();
                }

                return staffDatabase;
            }
        }

        public static SettingsDatabaseController SettingsDatabase
        {
            get
            {
                if (settingsDatabase == null)
                {
                    settingsDatabase = new SettingsDatabaseController();
                }

                return settingsDatabase;
            }
        }

        public static DailyTimesheetsDatabaseController DailyTimesheetsDatabase
        {
            get
            {
                if (dailyTimesheetsDatabase == null)
                {
                    dailyTimesheetsDatabase = new DailyTimesheetsDatabaseController();
                }

                return dailyTimesheetsDatabase;
            }
        }

        public static PurchaseOrdersDatabaseController PurchaseOrdersDatabase
        {
            get
            {
                if (purchaseOrdersDatabase == null)
                {
                    purchaseOrdersDatabase = new PurchaseOrdersDatabaseController();
                }

                return purchaseOrdersDatabase;
            }
        }

        public static VehicleChecklistsDatabaseController VehicleChecklistsDatabase
        {
            get
            {
                if (vehicleChecklistsDatabase == null)
                {
                    vehicleChecklistsDatabase = new VehicleChecklistsDatabaseController();
                }

                return vehicleChecklistsDatabase;
            }
        }

        public static JobSheetsDatabaseController JobSheetsDatabase
        {
            get
            {
                if (jobSheetsDatabase == null)
                {
                    jobSheetsDatabase = new JobSheetsDatabaseController();
                }

                return jobSheetsDatabase;
            }
        }

        public static HotWorksPermitDatabaseController HotWorksPermitDatabase
        {
            get
            {
                if (hotWorksPermitDatabase == null)
                {
                    hotWorksPermitDatabase = new HotWorksPermitDatabaseController();
                }

                return hotWorksPermitDatabase;
            }
        }

        #endregion
    }
}
