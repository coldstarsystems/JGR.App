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
    public partial class AnnualLeaveTabbed : TabbedPage
    {
        private String _url = "https://api.jgr1.com";
        private Models.Staff staff;

        public AnnualLeaveTabbed()
        {
            InitializeComponent();

            Children.Add(new Pending());
            Children.Add(new Approved());
            Children.Add(new Declined());
            Children.Add(new Cancelled());
        }

        private void GetAnnualLeaveRequests()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(_url),
            };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = client.GetAsync("GetAnnualLeaveRequests?StaffId=" + staff.Id).Result;

            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<List<AnnualLeaveRequestsViewModel>>(response.Content.ReadAsStringAsync().Result);

                foreach (var item in result)
                {
                    var annualLeaveRequest = new Models.AnnualLeave()
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
                        DeclineReason = item.DeclineReason,
                    };

                    App.AnnualLeaveDatabase.InsertUpdateAnnualLeaveRequest(annualLeaveRequest);
                }
            }
        }
    }
}