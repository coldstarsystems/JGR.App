using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using JGRBuildingServices.ViewModels;
using Newtonsoft.Json;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices.Views.HotWorkPermit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Permit : ContentPage
    {
        static readonly String _url = "https://api.jgr1.com";

        public Permit()
        {
            InitializeComponent();
        }

        public Permit(Int32 id)
        {
            InitializeComponent();

            GetData(id);

            Delete.IsVisible = true;
            SendToOffice.IsVisible = true;
        }

        private void GetData(Int32 id)
        {
            var WorksPermit = App.HotWorksPermitDatabase.GetHotWorksPermitById(id);

            Id.Text = id.ToString();

            if (!String.IsNullOrEmpty(WorksPermit.Content))
            {
                var worksPermitObject = JsonConvert.DeserializeObject<HotWorkPermitViewModel>(WorksPermit.Content);

                PermitNumberEntry.Text = worksPermitObject.PermitNo;
                PermitDate.Date = WorksPermit.CreatedDate;
                SiteNameEntry.Text = worksPermitObject.SiteName;
                LocationEntry.Text = worksPermitObject.LocationArea;
                EquipmentEntry.Text = worksPermitObject.EquipmentPlantToBeWorkedOn;
                NatureOfWorkEntry.Text = worksPermitObject.NatureOfWork;

                RequestSection.SetSection(new RequestOfPermit()
                {
                    RequestMinutes = worksPermitObject.RequestOfPermit.RequestMinutes,
                    RequestSigned = worksPermitObject.RequestOfPermit.RequestSigned,
                    RequestPrintName = worksPermitObject.RequestOfPermit.RequestPrintName,
                    RequestCompany = worksPermitObject.RequestOfPermit.RequestCompany,
                    RequestJobTitle = worksPermitObject.RequestOfPermit.RequestJobTitle,
                    RequestDate = worksPermitObject.RequestOfPermit.RequestDate,
                    RequestTime = worksPermitObject.RequestOfPermit.RequestTime
                });

                IssueSection.SetSection(new IssueOfPermit()
                {
                    IssuePrintName = worksPermitObject.IssueOfPermit.IssuePrintName,
                    IssueSigned = worksPermitObject.IssueOfPermit.IssueSigned,
                    IssueCompany = worksPermitObject.IssueOfPermit.IssueCompany,
                    IssueJobTitle = worksPermitObject.IssueOfPermit.IssueJobTitle,
                    IssueDate = worksPermitObject.IssueOfPermit.IssueDate,
                    IssueTime = worksPermitObject.IssueOfPermit.IssueTime,
                    IssueTimePermit = worksPermitObject.IssueOfPermit.IssueTimePermit,
                    IssueExpiryTimePermit = worksPermitObject.IssueOfPermit.IssueExpiryTimePermit
                });

                ReturnSection.SetSection(new ReturnOfPermit()
                {
                    ReturnMinutes = worksPermitObject.ReturnOfPermit.ReturnMinutes,
                    ReturnSigned = worksPermitObject.ReturnOfPermit.ReturnSigned,
                    ReturnPrintName = worksPermitObject.ReturnOfPermit.ReturnPrintName,
                    ReturnCompany = worksPermitObject.ReturnOfPermit.ReturnCompany,
                    ReturnJobTitle = worksPermitObject.ReturnOfPermit.ReturnJobTitle,
                    ReturnDate = worksPermitObject.ReturnOfPermit.ReturnDate,
                    ReturnTime = worksPermitObject.ReturnOfPermit.ReturnTime,
                    ReturnCompleted = worksPermitObject.ReturnOfPermit.ReturnCompleted,
                    ReturnCancelled = worksPermitObject.ReturnOfPermit.ReturnCancelled
                });

                foreach (var item in worksPermitObject.PermitChecklist)
                {
                    ChecklistSection.SetSection(new PermitChecklist()
                    {
                        Id = item.Id,
                        Question = item.Question,
                        InPlace = item.InPlace,
                        Comments = item.Comments,
                    });
                }
            }
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            SaveHotWorksPermitToDatabase(true);
        }

        private async void SaveHotWorksPermitToDatabase(Boolean displayAlert)
        {
            try
            {
                ApplyActivityIndicator();

                var staff = App.StaffDatabase.GetStaffById(Convert.ToInt32(App.SettingsDatabase.GetSetting("AssignedStaffId").Value));
                var hotWorksPermit = App.HotWorksPermitDatabase.GetHotWorksPermitById(Convert.ToInt32(Id.Text));

                var timeSpan = new TimeSpan(0, 0, 0, 0, 0);

                var model = new HotWorkPermitViewModel();

                model.PermitNo = PermitNumberEntry.Text;
                model.SiteName = SiteNameEntry.Text;
                model.LocationArea = LocationEntry.Text;
                model.EquipmentPlantToBeWorkedOn = EquipmentEntry.Text;
                model.NatureOfWork = NatureOfWorkEntry.Text;                

                var RequestAccordion = await RequestSection.GetSection(hotWorksPermit);

                var Request = new RequestOfPermit()
                {
                    RequestMinutes = RequestAccordion.RequestMinutes,
                    RequestSigned = RequestAccordion.RequestSigned,
                    RequestPrintName = RequestAccordion.RequestPrintName,
                    RequestCompany = RequestAccordion.RequestCompany,
                    RequestJobTitle = RequestAccordion.RequestJobTitle,
                    RequestDate = RequestAccordion.RequestDate,
                    RequestTime = RequestAccordion.RequestTime
                };

                model.RequestOfPermit = Request;
                var IssueAccordion = await IssueSection.GetSection(hotWorksPermit);

                var Issue = new IssueOfPermit()
                {
                    IssueSigned = IssueAccordion.IssueSigned,
                    IssuePrintName = IssueAccordion.IssuePrintName,
                    IssueCompany = IssueAccordion.IssueCompany,
                    IssueJobTitle = IssueAccordion.IssueJobTitle,
                    IssueDate = IssueAccordion.IssueDate,
                    IssueTime = IssueAccordion.IssueTime,
                    IssueTimePermit = IssueAccordion.IssueTimePermit,
                    IssueExpiryTimePermit = IssueAccordion.IssueExpiryTimePermit,
                };

                model.IssueOfPermit = Issue;
                var ReturnAccordion = await ReturnSection.GetSection(hotWorksPermit);

                var Return = new ReturnOfPermit()
                {
                    ReturnMinutes = ReturnAccordion.ReturnMinutes,
                    ReturnSigned = ReturnAccordion.ReturnSigned,
                    ReturnPrintName = ReturnAccordion.ReturnPrintName,
                    ReturnCompany = ReturnAccordion.ReturnCompany,
                    ReturnJobTitle = ReturnAccordion.ReturnJobTitle,
                    ReturnDate = ReturnAccordion.ReturnDate,
                    ReturnTime = ReturnAccordion.ReturnTime,
                    ReturnCancelled = ReturnAccordion.ReturnCancelled,
                    ReturnCompleted = ReturnAccordion.ReturnCompleted
                };

                model.ReturnOfPermit = Return;

                var ChecklistAccordion = ChecklistSection.GetSection();

                model.PermitChecklist = ChecklistAccordion;

                var modelContent = JsonConvert.SerializeObject(model).ToString();

                var m = new Models.HotWorksPermit()
                {
                    Content = modelContent,
                    CreatedDate = PermitDate.Date,
                    IsDeleted = false,
                    OfficeId = null,
                    SentToOffice = false,
                    StaffId = staff.Id
                };

                if (!String.IsNullOrEmpty(Id.Text))
                {
                    m.Id = Convert.ToInt32(Id.Text);

                    App.HotWorksPermitDatabase.UpdateHotWorksPermit(m);

                    if (displayAlert)
                    {
                        ResetActivityIndicator();

                        await DisplayAlert("Saved", "This Hot Works Permit has been saved successfully.", "Acknowledge");
                    }
                }
                else
                {
                    App.HotWorksPermitDatabase.InsertHotWorksPermit(m);

                    await Navigation.PushAsync(new Permit(m.Id));

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
            SaveHotWorksPermitToDatabase(false);

            var isConfirmed = false;

            var helpers = new Helpers();

            if (helpers.IsDeviceOnline())
            {
                isConfirmed = await DisplayAlert("Confirm", "Are you sure you want to submit this Hot Works Permit?", "Yes", "Cancel");
            }
            else
            {
                ResetActivityIndicator();

                await DisplayAlert("No Internet Connection", "You need to be connected to the internet in order to submit this Hot Works Permit.", "Acknowledge");
            }

            if (isConfirmed)
            {
                var hotWorksPermit = App.HotWorksPermitDatabase.GetHotWorksPermitById(Convert.ToInt32(Id.Text));

                var client = new HttpClient
                {
                    BaseAddress = new Uri(_url),
                };

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var hasErrors = false;
                var errorsCount = 0;
                var errors = new StringBuilder();

                var model = new HotWorkPermitViewModel();

                model.PermitNo = PermitNumberEntry.Text;
                model.SiteName = SiteNameEntry.Text;
                model.LocationArea = LocationEntry.Text;
                model.EquipmentPlantToBeWorkedOn = EquipmentEntry.Text;
                model.NatureOfWork = NatureOfWorkEntry.Text;

                #region Sent To Office Validation Checks 1
                if (model.PermitNo == null || model.PermitNo == "")
                {
                    errors.Append("Permit Number is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (model.SiteName == null || model.SiteName == "")
                {
                    errors.Append("Site Name is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (model.LocationArea == null || model.LocationArea == "")
                {
                    errors.Append("Location Area is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (model.EquipmentPlantToBeWorkedOn == null || model.EquipmentPlantToBeWorkedOn == "")
                {
                    errors.Append("Plant / Equipment To Be Worked On is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (model.NatureOfWork == null || model.NatureOfWork == "")
                {
                    errors.Append("Nature of work is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                #endregion

                var RequestAccordion = await RequestSection.GetSection(hotWorksPermit);

                var Request = new RequestOfPermit()
                {
                    RequestMinutes = RequestAccordion.RequestMinutes,
                    RequestSigned = RequestAccordion.RequestSigned,
                    RequestPrintName = RequestAccordion.RequestPrintName,
                    RequestCompany = RequestAccordion.RequestCompany,
                    RequestJobTitle = RequestAccordion.RequestJobTitle,
                    RequestDate = RequestAccordion.RequestDate,
                    RequestTime = RequestAccordion.RequestTime
                };

                model.RequestOfPermit = Request;

                #region Sent To Office Validation Checks 2
                if (String.IsNullOrEmpty((Request.RequestMinutes)))
                {
                    errors.Append("Request - Request Minutes is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (String.IsNullOrEmpty((Request.RequestPrintName)))
                {
                    errors.Append("Request - Print Name is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (String.IsNullOrEmpty((Request.RequestCompany)))
                {
                    errors.Append("Request - Company Name is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (String.IsNullOrEmpty((Request.RequestJobTitle)))
                {
                    errors.Append("Request - Job Title is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (Request.RequestDate == null)
                {
                    errors.Append("Request - Date is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (Request.RequestTime == null)
                {
                    errors.Append("Request - Time is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                #endregion

                var IssueAccordion = await IssueSection.GetSection(hotWorksPermit);

                var Issue = new IssueOfPermit()
                {
                    IssueSigned = IssueAccordion.IssueSigned,
                    IssuePrintName = IssueAccordion.IssuePrintName,
                    IssueCompany = IssueAccordion.IssueCompany,
                    IssueJobTitle = IssueAccordion.IssueJobTitle,
                    IssueDate = IssueAccordion.IssueDate,
                    IssueTime = IssueAccordion.IssueTime,
                    IssueTimePermit = IssueAccordion.IssueTimePermit,
                    IssueExpiryTimePermit = IssueAccordion.IssueExpiryTimePermit,
                };

                model.IssueOfPermit = Issue;

                #region Sent To Office Validation Checks 2


                if (String.IsNullOrEmpty((Issue.IssuePrintName)))
                {
                    errors.Append("Issue - Print Name is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (String.IsNullOrEmpty((Issue.IssueCompany)))
                {
                    errors.Append("Issue - Company Name is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (String.IsNullOrEmpty((Issue.IssueJobTitle)))
                {
                    errors.Append("Issue - Job Title is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (Issue.IssueDate == null)
                {
                    errors.Append("Issue - Date is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (Issue.IssueTime == null)
                {
                    errors.Append("Issue - Time is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (Issue.IssueTimePermit == null)
                {
                    errors.Append("Issue - Permit Time is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (Issue.IssueExpiryTimePermit == null)
                {
                    errors.Append("Issue - Expirty Time Permit is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                #endregion

                var ReturnAccordion = await ReturnSection.GetSection(hotWorksPermit);

                var Return = new ReturnOfPermit()
                {
                    ReturnMinutes = ReturnAccordion.ReturnMinutes,
                    ReturnSigned = ReturnAccordion.ReturnSigned,
                    ReturnPrintName = ReturnAccordion.ReturnPrintName,
                    ReturnCompany = ReturnAccordion.ReturnCompany,
                    ReturnJobTitle = ReturnAccordion.ReturnJobTitle,
                    ReturnDate = ReturnAccordion.ReturnDate,
                    ReturnTime = ReturnAccordion.ReturnTime
                };

                model.ReturnOfPermit = Return;

                #region Sent To Office Validate Checks 3

                if (String.IsNullOrEmpty((Return.ReturnPrintName)))
                {
                    errors.Append("Return - Print Name is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (String.IsNullOrEmpty((Return.ReturnCompany)))
                {
                    errors.Append("Return - Company Name is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (String.IsNullOrEmpty((Return.ReturnJobTitle)))
                {
                    errors.Append("Return - Job Title is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (Return.ReturnDate == null)
                {
                    errors.Append("Return - Date is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (Return.ReturnTime == null)
                {
                    errors.Append("Return - Time is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }
                #endregion

                var ChecklistAccordion = ChecklistSection.GetSection();

                model.PermitChecklist = ChecklistAccordion;

                #region Sent To Office Validate Checks 4

                foreach (var child in ChecklistAccordion)
                {
                    if (String.IsNullOrEmpty((child.InPlace)))
                    {
                        errors.Append("Checklist - Some or more In place checks are Required." + Environment.NewLine);

                        hasErrors = true;
                        errorsCount++;
                    }
                }

                #endregion

                if (!hasErrors)
                {
                    ResetActivityIndicator();

                    var ma = new HotWorksPermitViewModel()
                    {
                        Content = hotWorksPermit.Content,
                        CreatedDate = PermitDate.Date
                    };

                    var jsonObject = JsonConvert.SerializeObject(ma).ToString();

                     var response = await client.PostAsync("PostHotWorksPermit", new StringContent(jsonObject, Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        var contents = await response.Content.ReadAsStringAsync();

                        var jsonResponse = JsonConvert.DeserializeObject<CreateHotWorksPermitResponseViewModel>(contents);

                        if (jsonResponse.Id == -1)
                        {
                            ResetActivityIndicator();

                            await DisplayAlert("Error", jsonResponse.ErrorMessage, "Acknowledge");
                        }
                        else
                        {
                            App.HotWorksPermitDatabase.HotWorksPermitsentToOffice(hotWorksPermit.Id, jsonResponse.Id);

                            await DisplayAlert("Success", "Your Hot Works Permit has been successfully sent to the office.", "Back to Hot Works Permits");

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
                    await DisplayAlert(errorsCount + " Errors", "The following errors have occurred." + Environment.NewLine + Environment.NewLine + errors.ToString(), "Acknowledge");
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

            var confirmed = await DisplayAlert("Confirm", "Are you sure want to delete this Hot Works Permit?", "Yes", "No");

            if (confirmed)
            {
                App.HotWorksPermitDatabase.DeleteHotWorksPermit(Convert.ToInt32(Id.Text));

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