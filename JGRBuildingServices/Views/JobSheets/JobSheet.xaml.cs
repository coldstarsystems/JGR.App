using JGRBuildingServices.ViewModels;
using Newtonsoft.Json;
using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices.Views.JobSheets
{
    public class JobSheetPageObject
    {
        public String Id { get; set; }

        public String CustomerName { get; set; }

        public String SiteAddress { get; set; }

        public String CustomerJobNumber { get; set; }

        public String JGRJobNumber { get; set; }

        public String CompletedBy { get; set; }

        public String Position { get; set; }

        public DateTime? Date { get; set; }

        public String Description { get; set; }

        public String Location { get; set; }

        public String SubInvoiceTotal { get; set; }

        public String ValueAddedTax { get; set; }

        public String TotalInvoice { get; set; }

        public String PrintCustomerName { get; set; }

        public DateTime? CompletionDate { get; set; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class JobSheet : ContentPage
    {
        static readonly String _url = "https://api.jgr1.com";

        public static Boolean isTapped = false;

        JobSheetPageObject JobSheetobject = new JobSheetPageObject();

        public JobSheet()
        {
            InitializeComponent();

            BindingContext = new JobSheetViewModel()
            {
                CustomersList = App.CustomersDatabase.GetAllCustomers().Select(c => new CustomersViewModel() { Id = c.Id, Name = c.Name }).ToList()
            };

            SearchResults.ItemsSource = GetCustomers();

            NinthSection.IsVisible = false;

            var assignedStaffId = App.SettingsDatabase.GetSetting("AssignedStaffId");

            if (assignedStaffId != null)
            {
                var staffId = Convert.ToInt32(assignedStaffId.Value);

                var staffMember = App.StaffDatabase.GetStaffById(staffId);

                PositionEntry.Text = staffMember.Position;
            }
        }

        public JobSheet(JobSheetPageObject JobSheetobject)
        {
            InitializeComponent();

            this.JobSheetobject = JobSheetobject;

            SiteAddressEntry.Text = JobSheetobject.SiteAddress;
            CustomerJobNoEntry.Text = JobSheetobject.CustomerJobNumber;
            JgrJobNoEntry.Text = JobSheetobject.JGRJobNumber;
            AssessmentByEntry.Text = JobSheetobject.CompletedBy;
            PositionEntry.Text = JobSheetobject.Position;
            DateAndTimePicker.NullableDate = JobSheetobject.Date;
            SubInvoiceTotal.Text = JobSheetobject.SubInvoiceTotal;
            ValueAddedTax.Text = JobSheetobject.ValueAddedTax;
            TotalInvoice.Text = JobSheetobject.TotalInvoice;
            CustomerPrintName.Text = JobSheetobject.PrintCustomerName;
            FormCompletionDate.NullableDate = JobSheetobject.CompletionDate;
        }

        public JobSheet(Int32 id)
        {
            InitializeComponent();

            BindingContext = new JobSheetViewModel()
            {
                CustomersList = App.CustomersDatabase.GetAllCustomers().Select(c => new CustomersViewModel()
                {
                    Id = c.Id,
                    Name = c.Name,
                    AddressLine1 = c.AddressLine1,
                    AddressLine2 = c.AddressLine2,
                    AddressLine3 = c.AddressLine3,
                    AddressLine4 = c.AddressLine4,
                    TownCity = c.TownCity,
                    Postcode = c.Postcode
                }).ToList()
            };

            SearchResults.ItemsSource = GetCustomers();

            GetData(id);

            Delete.IsVisible = true;
            SendToOffice.IsVisible = true;
        }

        void CustomerChanged(object sender, EventArgs e)
        {
            var customerSelected = (CustomersViewModel)CustomerList.SelectedItem;

            if (customerSelected != null)
            {
                var customer = App.CustomersDatabase.GetCustomerById(customerSelected.Id);

                var address = new StringBuilder();

                if (!String.IsNullOrEmpty(customer.AddressLine1))
                {
                    address.Append(customer.AddressLine1);
                }

                if (!String.IsNullOrEmpty(customer.AddressLine2))
                {
                    address.Append(", " + customer.AddressLine2);
                }

                if (!String.IsNullOrEmpty(customer.AddressLine3))
                {
                    address.Append(", " + customer.AddressLine3);
                }

                if (!String.IsNullOrEmpty(customer.AddressLine4))
                {
                    address.Append(", " + customer.AddressLine4);
                }

                if (!String.IsNullOrEmpty(customer.TownCity))
                {
                    address.Append(", " + customer.TownCity);
                }

                if (!String.IsNullOrEmpty(customer.Postcode))
                {
                    address.Append(", " + customer.Postcode);
                }

                SiteAddressEntry.Text = address.ToString();
            }
        }

        private void GetData(Int32 id)
        {
            var jobSheet = App.JobSheetsDatabase.GetJobSheetById(id);

            Id.Text = id.ToString();

            CustomerList.SelectedItem = (BindingContext as JobSheetViewModel).CustomersList.FirstOrDefault(c => c.Id == jobSheet.CustomerId);

            CustomerSignatureSignaturePadView.IsVisible = false;
            CustomerSignImage.IsVisible = false;

            SignatureSignaturePadView.IsVisible = false;
            SignImage.IsVisible = false;

            if (!String.IsNullOrEmpty(jobSheet.Content))
            {
                var jobSheetObject = JsonConvert.DeserializeObject<JobSheetObjectViewModel>(jobSheet.Content);

                var fGasView = jobSheetObject.FGas;

                FGasSwitch.IsChecked = jobSheetObject.IsFGas;
                SiteAddressEntry.Text = jobSheetObject.CustomerSiteAddress;
                CustomerJobNoEntry.Text = jobSheetObject.CustomerJobNumber;
                JgrJobNoEntry.Text = jobSheetObject.JGRJobNumber;
                AssessmentByEntry.Text = jobSheetObject.AssessmentCompletedBy;
                PositionEntry.Text = jobSheetObject.Position;
                DateAndTimePicker.NullableDate = jobSheetObject.AssessmentCompletedDateTime.Date;

                SubInvoiceTotal.Text = jobSheetObject.SubInvoiceTotal;
                ValueAddedTax.Text = jobSheetObject.VAT;
                TotalInvoice.Text = jobSheetObject.TotalInvoice;
                CustomerPrintName.Text = jobSheetObject.CustomerPrintName;
                FormCompletionDate.NullableDate = jobSheetObject.CustomerDate.Date;

                #region Set Description

                Description.SetSectionDescription(new SectionDescription()
                {
                    Description = jobSheetObject.DescriptionOfWorksToBeCompleted
                });

                #endregion

                #region Set Location

                Location.SetSectionLocation(new SectionLocation
                {
                    LocationOfWork = jobSheetObject.LocationOfWorksAndEquipment
                });

                #endregion

                #region Set Persons Risk

                PersonsRisk.SetSectionPersonsRisk(new SectionPersonsRisk()
                {
                    Customers = jobSheetObject.PersonsAtRisk.MembersOfPublicCustomers,
                    JGREmployees = jobSheetObject.PersonsAtRisk.JGREmployees,
                    SitePersonnel = jobSheetObject.PersonsAtRisk.CustomerSitePersonnel,
                    YoungPersons = jobSheetObject.PersonsAtRisk.YoungPersons
                });

                #endregion

                #region Set Before You Start
                BeforeYouStart.SetSectionBefore(new SectionBeforeYouStart()
                {
                    Check1 = "Have you worked on this site before?",
                    Check1Comments = jobSheetObject.BeforeYouStart.Where(c => c.Name == "Have you worked on this site before?").Select(c => c.Comments).SingleOrDefault(),
                    Check1Value = jobSheetObject.BeforeYouStart.Where(c => c.Name == "Have you worked on this site before?").Select(c => c.Value).SingleOrDefault(),

                    Check2 = "Have all checks and procedures been completed, as detailed in the Service Engineers Method Statement?",
                    Check2Comments = jobSheetObject.BeforeYouStart.Where(c => c.Name == "Have all checks and procedures been completed, as detailed in the Service Engineers Method Statement?").Select(c => c.Comments).SingleOrDefault(),
                    Check2Value = jobSheetObject.BeforeYouStart.Where(c => c.Name == "Have all checks and procedures been completed, as detailed in the Service Engineers Method Statement?").Select(c => c.Value).SingleOrDefault(),

                    Check3 = "Have you done this type of job before?",
                    Check3Comments = jobSheetObject.BeforeYouStart.Where(c => c.Name == "Have you done this type of job before?").Select(c => c.Comments).SingleOrDefault(),
                    Check3Value = jobSheetObject.BeforeYouStart.Where(c => c.Name == "Have you done this type of job before?").Select(c => c.Value).SingleOrDefault(),

                    Check4 = "Do you have the right tools for the job?",
                    Check4Comments = jobSheetObject.BeforeYouStart.Where(c => c.Name == "Do you have the right tools for the job?").Select(c => c.Comments).SingleOrDefault(),
                    Check4Value = jobSheetObject.BeforeYouStart.Where(c => c.Name == "Do you have the right tools for the job?").Select(c => c.Value).SingleOrDefault(),

                    Check5 = "Where required, tools need calibrated and in date?",
                    Check5Comments = jobSheetObject.BeforeYouStart.Where(c => c.Name == "Where required, tools need calibrated and in date?").Select(c => c.Comments).SingleOrDefault(),
                    Check5Value = jobSheetObject.BeforeYouStart.Where(c => c.Name == "Where required, tools need calibrated and in date?").Select(c => c.Value).SingleOrDefault(),

                    Check6 = "Do you have the right documentation for the job?",
                    Check6Comments = jobSheetObject.BeforeYouStart.Where(c => c.Name == "Do you have the right documentation for the job?").Select(c => c.Comments).SingleOrDefault(),
                    Check6Value = jobSheetObject.BeforeYouStart.Where(c => c.Name == "Do you have the right documentation for the job?").Select(c => c.Value).SingleOrDefault(),

                    Check7 = "Do you have the right PPE for the job?",
                    Check7Comments = jobSheetObject.BeforeYouStart.Where(c => c.Name == "Do you have the right PPE for the job?").Select(c => c.Comments).SingleOrDefault(),
                    Check7Value = jobSheetObject.BeforeYouStart.Where(c => c.Name == "Do you have the right PPE for the job?").Select(c => c.Value).SingleOrDefault(),

                    Check8 = "Are power tools and leads PAT tested?",
                    Check8Comments = jobSheetObject.BeforeYouStart.Where(c => c.Name == "Are power tools and leads PAT tested?").Select(c => c.Comments).SingleOrDefault(),
                    Check8Value = jobSheetObject.BeforeYouStart.Where(c => c.Name == "Are power tools and leads PAT tested?").Select(c => c.Value).SingleOrDefault(),

                    Check9 = "Are scaffolds and ladders inspected?",
                    Check9Comments = jobSheetObject.BeforeYouStart.Where(c => c.Name == "Are scaffolds and ladders inspected?").Select(c => c.Comments).SingleOrDefault(),
                    Check9Value = jobSheetObject.BeforeYouStart.Where(c => c.Name == "Are scaffolds and ladders inspected?").Select(c => c.Value).SingleOrDefault()
                });

#endregion

                #region Set Risk Assessment
                RiskAssessment.SetSectionRisk(new SectionRisk()
                {
                    Check1 = "Working on Clients Sites / Premises:",
                    Check1Value = jobSheetObject.RiskAssessment.Where(c => c.Name == "Working on Clients Sites / Premises:").Select(c => c.Checked).SingleOrDefault(),

                    Check2 = "Working in Ceiling / Void Spaces:",
                    Check2Value = jobSheetObject.RiskAssessment.Where(c => c.Name == "Working in Ceiling / Void Spaces:").Select(c => c.Checked).SingleOrDefault(),

                    Check3 = "Working on Flat Roofs:",
                    Check3Value = jobSheetObject.RiskAssessment.Where(c => c.Name == "Working on Flat Roofs:").Select(c => c.Checked).SingleOrDefault(),

                    Check4 = "Use of Roof Man Anchor Systems:",
                    Check4Value = jobSheetObject.RiskAssessment.Where(c => c.Name == "Use of Roof Man Anchor Systems:").Select(c => c.Checked).SingleOrDefault(),

                    Check5 = "Lone Working / Out Of Hours Working:",
                    Check5Value = jobSheetObject.RiskAssessment.Where(c => c.Name == "Lone Working / Out Of Hours Working:").Select(c => c.Checked).SingleOrDefault(),

                    Check6 = "Use of Mobile Elevated Lift Platforms:",
                    Check6Value = jobSheetObject.RiskAssessment.Where(c => c.Name == "Use of Mobile Elevated Lift Platforms:").Select(c => c.Checked).SingleOrDefault(),

                    Check7 = "Use of Mobile Scaffold Towers:",
                    Check7Value = jobSheetObject.RiskAssessment.Where(c => c.Name == "Use of Mobile Scaffold Towers:").Select(c => c.Checked).SingleOrDefault(),

                    Check8 = "Use of Ladders & Step Ladders:",
                    Check8Value = jobSheetObject.RiskAssessment.Where(c => c.Name == "Use of Ladders & Step Ladders:").Select(c => c.Checked).SingleOrDefault(),

                    Check9 = "Use of Material Hoists:",
                    Check9Value = jobSheetObject.RiskAssessment.Where(c => c.Name == "Use of Material Hoists:").Select(c => c.Checked).SingleOrDefault(),

                    Check10 = "Use of Hand Tools:",
                    Check10Value = jobSheetObject.RiskAssessment.Where(c => c.Name == "Use of Hand Tools:").Select(c => c.Checked).SingleOrDefault(),

                    Check11 = "Use of Portable Electrical Equipment:",
                    Check11Value = jobSheetObject.RiskAssessment.Where(c => c.Name == "Use of Portable Electrical Equipment:").Select(c => c.Checked).SingleOrDefault(),

                    Check12 = "Use of Disc Cutters & Angle Grinders:",
                    Check12Value = jobSheetObject.RiskAssessment.Where(c => c.Name == "Use of Disc Cutters & Angle Grinders:").Select(c => c.Checked).SingleOrDefault(),

                    Check13 = "Pressure Testing of Pipework & Systems:",
                    Check13Value = jobSheetObject.RiskAssessment.Where(c => c.Name == "Pressure Testing of Pipework & Systems:").Select(c => c.Checked).SingleOrDefault(),

                    Check14 = "Electrical Works up to 415v:",
                    Check14Value = jobSheetObject.RiskAssessment.Where(c => c.Name == "Electrical Works up to 415v:").Select(c => c.Checked).SingleOrDefault(),

                    Check15 = "Use of Oxy-acetylene Equipment:",
                    Check15Value = jobSheetObject.RiskAssessment.Where(c => c.Name == "Use of Oxy-acetylene Equipment:").Select(c => c.Checked).SingleOrDefault(),

                    Check16 = "Storage of Materials on site:",
                    Check16Value = jobSheetObject.RiskAssessment.Where(c => c.Name == "Storage of Materials on site:").Select(c => c.Checked).SingleOrDefault(),

                    Check17 = "Storage of Flammable Substances & Compressed Gas Cylinders:",
                    Check17Value = jobSheetObject.RiskAssessment.Where(c => c.Name == "Storage of Flammable Substances & Compressed Gas Cylinders:").Select(c => c.Checked).SingleOrDefault(),

                    Check18 = "Slips, Trips & Falls:",
                    Check18Value = jobSheetObject.RiskAssessment.Where(c => c.Name == "Slips, Trips & Falls:").Select(c => c.Checked).SingleOrDefault(),

                    Check19 = "Working in Coldrooms:",
                    Check19Value = jobSheetObject.RiskAssessment.Where(c => c.Name == "Working in Coldrooms:").Select(c => c.Checked).SingleOrDefault(),

                    Check20 = "Noise:",
                    Check20Value = jobSheetObject.RiskAssessment.Where(c => c.Name == "Noise:").Select(c => c.Checked).SingleOrDefault(),

                    Check21 = "Removal Installation & Movement of Refrigeration Units:",
                    Check21Value = jobSheetObject.RiskAssessment.Where(c => c.Name == "Removal Installation & Movement of Refrigeration Units:").Select(c => c.Checked).SingleOrDefault(),

                    Check22 = "Removal Installation & Movement of Air Con Units:",
                    Check22Value = jobSheetObject.RiskAssessment.Where(c => c.Name == "Removal Installation & Movement of Air Con Units:").Select(c => c.Checked).SingleOrDefault(),

                    Check23 = "Loading & Unloading Vehicles:",
                    Check23Value = jobSheetObject.RiskAssessment.Where(c => c.Name == "Loading & Unloading Vehicles:").Select(c => c.Checked).SingleOrDefault(),

                    Check24 = "Movement of Refrigerant & Compressed Gas Cylinders:",
                    Check24Value = jobSheetObject.RiskAssessment.Where(c => c.Name == "Movement of Refrigerant & Compressed Gas Cylinders:").Select(c => c.Checked).SingleOrDefault(),

                    Check25 = "Use of Podium Steps:",
                    Check25Value = jobSheetObject.RiskAssessment.Where(c => c.Name == "Use of Podium Steps:").Select(c => c.Checked).SingleOrDefault()
                });
                #endregion

                #region Set SiteWorks

                SiteWorks.SetSectionSiteWorks(new SectionSiteWorks()
                {
                    Make = jobSheetObject.SiteWorks.Make,
                    Model = jobSheetObject.SiteWorks.Model,
                    SerialNumber = jobSheetObject.SiteWorks.SerialNumber,
                    AssetNumber = jobSheetObject.SiteWorks.AssetNumber,
                    WorkedCarriedOut = jobSheetObject.SiteWorks.WorksCarriedOut,
                });

                #endregion

                #region Set Additional Hazards

                var hazardList = new HazardList();

                foreach (var hazard in jobSheetObject.AdditionalHazards)
                {
                    hazardList.Hazards.Add(new HazardAccordionView()
                    {
                        AdditionalControlMeasuresRequired = hazard.AdditionalControlMeasuresRequired,
                        Hazard = hazard.Hazard
                    });
                }

                AdditionalHazard.SetHazardList(hazardList);
                #endregion

                #region Set FGas

                var misc = new FGasMiscViewModel();

                misc.TemperatureOnArrival = jobSheetObject.TemperatureOnArrival;
                misc.TemperatureOnDepart = jobSheetObject.TemperatureOnDepart;
                misc.PlantLeftOperational = jobSheetObject.PlantLeftOperational;
                misc.JobComplete = jobSheetObject.JobComplete;
                PartsTimeMisc.SetSectionPartsTimeMisc(misc);

                PartsTimeMisc.SetSectionParts(jobSheetObject);
                PartsTimeMisc.SetSectionTime(jobSheetObject);

                FGas.SetSectionFGas(new SectionFGas()
                {
                    LTDate = jobSheetObject.SiteWorks.LeakTestDate,
                    LTStartTime = jobSheetObject.SiteWorks.LeakTestStartTime,
                    LTFinishTime = jobSheetObject.SiteWorks.LeakTestFinishTime,
                    LTSystemNumber = jobSheetObject.SiteWorks.LeakTestSystemNumber,
                    LTLeakFoundYes = jobSheetObject.SiteWorks.LeakTestLeakFound,
                    LTLeakFoundNo = jobSheetObject.SiteWorks.LeakTestLeakFound,
                    LTLocationOfLeak = jobSheetObject.SiteWorks.LeakTestLocationOfLeak,
                    LTDetailsOfRepair = jobSheetObject.SiteWorks.DetailsOfRepair,
                    LTActionTaken = jobSheetObject.SiteWorks.ActionTakenToPreventFutureLeak,
                    RLTDate = jobSheetObject.SiteWorks.ReturnLeakTestDate,
                    RLTStartTime = jobSheetObject.SiteWorks.ReturnLeakTestStartTime,
                    RLTFinishTime = jobSheetObject.SiteWorks.ReturnLeakTestFinishTime,
                    RLTSystemNumber = jobSheetObject.SiteWorks.ReturnLeakTestSystemNumber,
                    RLTLeakFoundYes = jobSheetObject.SiteWorks.ReturnLeakTestLeakFound,
                    RLTLeakFoundNo = jobSheetObject.SiteWorks.ReturnLeakTestLeakFound,
                    RLTDetails = jobSheetObject.SiteWorks.ReturnLeakTestDetails
                });

                #endregion

                if (jobSheetObject.IsFGas == true)
                {
                    NinthSection.IsVisible = true;
                }
                else
                {
                    NinthSection.IsVisible = false;
                }

                if (jobSheetObject.Signature != null)
                {
                    SignImage.IsVisible = true;
                    SignImage.Source = ImageSource.FromStream(() => new MemoryStream(jobSheetObject.Signature));
                }
                else
                {
                    SignatureSignaturePadView.IsVisible = true;
                }

                if (jobSheetObject.CustomerSignature != null)
                {
                    CustomerSignImage.IsVisible = true;
                    CustomerSignImage.Source = ImageSource.FromStream(() => new MemoryStream(jobSheetObject.CustomerSignature));
                }
                else
                {
                    CustomerSignatureSignaturePadView.IsVisible = true;
                }
            }
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            SaveJobSheetToDatabase(true, false);
        }

        private void ValidationChecks() { }

        private async void SaveJobSheetToDatabase(Boolean displayAlert, Boolean sendToOffice)
        {
            try
            {
                ApplyActivityIndicator();

                var staff = App.StaffDatabase.GetStaffById(Convert.ToInt32(App.SettingsDatabase.GetSetting("AssignedStaffId").Value));
                var jobSheet = App.JobSheetsDatabase.GetJobSheetById(Convert.ToInt32(Id.Text));

                var customer = (CustomersViewModel)CustomerList.SelectedItem;

                var hasErrors = false;
                var errorsCount = 0;
                var errors = new StringBuilder();

                #region Errors

                if (CustomerList.SelectedItem == null)
                {
                    errors.Append("Customer is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (String.IsNullOrEmpty(JgrJobNoEntry.Text) && String.IsNullOrEmpty(CustomerJobNoEntry.Text))
                {
                    errors.Append("JGR Job No or Co-op Job No is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;

                    CustomerJobNo.TextColor = Color.Red;
                    CustomerJobNo.FontAttributes = FontAttributes.Bold;

                    JgrJobNo.TextColor = Color.Red;
                    JgrJobNo.FontAttributes = FontAttributes.Bold;
                }
                else
                {
                    JgrJobNo.TextColor = Color.Black;
                    JgrJobNo.FontAttributes = FontAttributes.None;

                    CustomerJobNo.TextColor = Color.Black;
                    CustomerJobNo.FontAttributes = FontAttributes.None;
                }

                if (String.IsNullOrEmpty(AssessmentByEntry.Text))
                {
                    errors.Append("Assessment Completed By is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;

                    AssessmentBy.TextColor = Color.Red;
                    AssessmentBy.FontAttributes = FontAttributes.Bold;
                }
                else
                {
                    AssessmentBy.TextColor = Color.Black;
                    AssessmentBy.FontAttributes = FontAttributes.None;
                }

                if (String.IsNullOrEmpty(PositionEntry.Text))
                {
                    errors.Append("Position is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;

                    Position.TextColor = Color.Red;
                    Position.FontAttributes = FontAttributes.Bold;
                }
                else
                {
                    Position.TextColor = Color.Black;
                    Position.FontAttributes = FontAttributes.None;
                }

                if (String.IsNullOrEmpty(Description.GetSectionDescription()))
                {
                    errors.Append("Description of Works to be Completed is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (String.IsNullOrEmpty(Location.GetSectionLocation()))
                {
                    errors.Append("Location of Works and Equipment to be Worked on is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (String.IsNullOrEmpty(Location.GetSectionLocation()))
                {
                    errors.Append("Location of Works and Equipment to be Worked on is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                #region Before you Start Validation

                var beforeyouStartSectionValidation = BeforeYouStart.GetSectionBefore();

                if (String.IsNullOrEmpty(beforeyouStartSectionValidation.Check1Value))
                {
                    errors.Append(beforeyouStartSectionValidation.Check1 + " is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (String.IsNullOrEmpty(beforeyouStartSectionValidation.Check2Value))
                {
                    errors.Append(beforeyouStartSectionValidation.Check2 + " is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (String.IsNullOrEmpty(beforeyouStartSectionValidation.Check3Value))
                {
                    errors.Append(beforeyouStartSectionValidation.Check3 + " is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (String.IsNullOrEmpty(beforeyouStartSectionValidation.Check4Value))
                {
                    errors.Append(beforeyouStartSectionValidation.Check4 + " is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (String.IsNullOrEmpty(beforeyouStartSectionValidation.Check5Value))
                {
                    errors.Append(beforeyouStartSectionValidation.Check5 + " is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (String.IsNullOrEmpty(beforeyouStartSectionValidation.Check6Value))
                {
                    errors.Append(beforeyouStartSectionValidation.Check6 + " is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (String.IsNullOrEmpty(beforeyouStartSectionValidation.Check7Value))
                {
                    errors.Append(beforeyouStartSectionValidation.Check7 + " is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (String.IsNullOrEmpty(beforeyouStartSectionValidation.Check8Value))
                {
                    errors.Append(beforeyouStartSectionValidation.Check8 + " is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (String.IsNullOrEmpty(beforeyouStartSectionValidation.Check9Value))
                {
                    errors.Append(beforeyouStartSectionValidation.Check9 + " is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                #endregion

                var partsTimeMiscSection = PartsTimeMisc.GetSectionFGas();

                if (String.IsNullOrEmpty(partsTimeMiscSection.PlantLeftOperational))
                {
                    errors.Append("Plant Left Operational is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                if (String.IsNullOrEmpty(partsTimeMiscSection.JobComplete))
                {
                    errors.Append("Job Complete is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                var siteWorksSection = SiteWorks.GetSectionSiteWorks();

                if (String.IsNullOrEmpty((siteWorksSection.WorkedCarriedOut)))
                {
                    errors.Append("Worked Carried Out to be Completed is Required." + Environment.NewLine);

                    hasErrors = true;
                    errorsCount++;
                }

                #endregion

                if (hasErrors)
                {
                    await DisplayAlert(errorsCount + " Errors", "The following errors have occurred." + Environment.NewLine + Environment.NewLine + errors.ToString(), "Acknowledge");

                    ResetActivityIndicator();
                }
                else
                {
                    var m = new Models.JobSheets()
                    {
                        Content = null,
                        CreatedDate = DateTime.Now,
                        CustomerId = customer.Id,
                        IsDeleted = false,
                        OfficeId = null,
                        SentToOffice = false,
                        StaffId = staff.Id
                    };

                    #region Job Sheet JSON

                    var beforeYouStart = new List<BeforeYouStartViewModel>();
                    var riskAssessment = new List<RiskAssessmentViewModel>();
                    var jobTimesheet = new List<JobTimesheetViewModel>();
                    var materialsUsed = new List<MaterialsUsedViewModel>();
                    var siteWorks = new SiteWorksViewModel();
                    var additionalHazards = new List<AdditionalHazardsViewModel>();
                    var fgas = new SectionFGas();
                    var fGasLeak = new LeakTestViewModel();
                    var fGasReturnLeak = new ReturnLeakTestViewModel();
                    var fGasMisc = new FGasMiscViewModel();

                    #region Before You Start

                    var beforeyouStartSection = BeforeYouStart.GetSectionBefore();

                    beforeYouStart.Add(new BeforeYouStartViewModel()
                    {
                        Name = beforeyouStartSection.Check1,
                        Value = beforeyouStartSection.Check1Value,
                        Comments = beforeyouStartSection.Check1Comments,
                    });

                    beforeYouStart.Add(new BeforeYouStartViewModel()
                    {
                        Name = beforeyouStartSection.Check2,
                        Value = beforeyouStartSection.Check2Value,
                        Comments = beforeyouStartSection.Check2Comments,
                    });

                    beforeYouStart.Add(new BeforeYouStartViewModel()
                    {
                        Name = beforeyouStartSection.Check3,
                        Value = beforeyouStartSection.Check3Value,
                        Comments = beforeyouStartSection.Check3Comments,
                    });

                    beforeYouStart.Add(new BeforeYouStartViewModel()
                    {
                        Name = beforeyouStartSection.Check4,
                        Value = beforeyouStartSection.Check4Value,
                        Comments = beforeyouStartSection.Check4Comments,
                    });

                    beforeYouStart.Add(new BeforeYouStartViewModel()
                    {
                        Name = beforeyouStartSection.Check5,
                        Value = beforeyouStartSection.Check5Value,
                        Comments = beforeyouStartSection.Check5Comments,
                    });

                    beforeYouStart.Add(new BeforeYouStartViewModel()
                    {
                        Name = beforeyouStartSection.Check6,
                        Value = beforeyouStartSection.Check6Value,
                        Comments = beforeyouStartSection.Check6Comments,
                    });

                    beforeYouStart.Add(new BeforeYouStartViewModel()
                    {
                        Name = beforeyouStartSection.Check7,
                        Value = beforeyouStartSection.Check7Value,
                        Comments = beforeyouStartSection.Check7Comments,
                    });

                    beforeYouStart.Add(new BeforeYouStartViewModel()
                    {
                        Name = beforeyouStartSection.Check8,
                        Value = beforeyouStartSection.Check8Value,
                        Comments = beforeyouStartSection.Check8Comments,
                    });

                    beforeYouStart.Add(new BeforeYouStartViewModel()
                    {
                        Name = beforeyouStartSection.Check9,
                        Value = beforeyouStartSection.Check9Value,
                        Comments = beforeyouStartSection.Check9Comments,
                    });

                    #endregion

                    #region Risk Assessment

                    var riskAssessmentSection = RiskAssessment.GetSectionRisk();

                    riskAssessment.Add(new RiskAssessmentViewModel()
                    {
                        Name = riskAssessmentSection.Check1,
                        Checked = riskAssessmentSection.Check1Value
                    });

                    riskAssessment.Add(new RiskAssessmentViewModel()
                    {
                        Name = riskAssessmentSection.Check2,
                        Checked = riskAssessmentSection.Check2Value
                    });

                    riskAssessment.Add(new RiskAssessmentViewModel()
                    {
                        Name = riskAssessmentSection.Check3,
                        Checked = riskAssessmentSection.Check3Value
                    });

                    riskAssessment.Add(new RiskAssessmentViewModel()
                    {
                        Name = riskAssessmentSection.Check4,
                        Checked = riskAssessmentSection.Check4Value
                    });

                    riskAssessment.Add(new RiskAssessmentViewModel()
                    {
                        Name = riskAssessmentSection.Check5,
                        Checked = riskAssessmentSection.Check5Value
                    });

                    riskAssessment.Add(new RiskAssessmentViewModel()
                    {
                        Name = riskAssessmentSection.Check6,
                        Checked = riskAssessmentSection.Check6Value
                    });

                    riskAssessment.Add(new RiskAssessmentViewModel()
                    {
                        Name = riskAssessmentSection.Check7,
                        Checked = riskAssessmentSection.Check7Value
                    });

                    riskAssessment.Add(new RiskAssessmentViewModel()
                    {
                        Name = riskAssessmentSection.Check8,
                        Checked = riskAssessmentSection.Check8Value
                    });

                    riskAssessment.Add(new RiskAssessmentViewModel()
                    {
                        Name = riskAssessmentSection.Check9,
                        Checked = riskAssessmentSection.Check9Value
                    });

                    riskAssessment.Add(new RiskAssessmentViewModel()
                    {
                        Name = riskAssessmentSection.Check10,
                        Checked = riskAssessmentSection.Check10Value
                    });

                    riskAssessment.Add(new RiskAssessmentViewModel()
                    {
                        Name = riskAssessmentSection.Check11,
                        Checked = riskAssessmentSection.Check11Value
                    });

                    riskAssessment.Add(new RiskAssessmentViewModel()
                    {
                        Name = riskAssessmentSection.Check12,
                        Checked = riskAssessmentSection.Check12Value
                    });

                    riskAssessment.Add(new RiskAssessmentViewModel()
                    {
                        Name = riskAssessmentSection.Check13,
                        Checked = riskAssessmentSection.Check13Value
                    });

                    riskAssessment.Add(new RiskAssessmentViewModel()
                    {
                        Name = riskAssessmentSection.Check14,
                        Checked = riskAssessmentSection.Check14Value
                    });

                    riskAssessment.Add(new RiskAssessmentViewModel()
                    {
                        Name = riskAssessmentSection.Check15,
                        Checked = riskAssessmentSection.Check15Value
                    });

                    riskAssessment.Add(new RiskAssessmentViewModel()
                    {
                        Name = riskAssessmentSection.Check16,
                        Checked = riskAssessmentSection.Check16Value
                    });

                    riskAssessment.Add(new RiskAssessmentViewModel()
                    {
                        Name = riskAssessmentSection.Check17,
                        Checked = riskAssessmentSection.Check17Value
                    });

                    riskAssessment.Add(new RiskAssessmentViewModel()
                    {
                        Name = riskAssessmentSection.Check18,
                        Checked = riskAssessmentSection.Check18Value
                    });

                    riskAssessment.Add(new RiskAssessmentViewModel()
                    {
                        Name = riskAssessmentSection.Check19,
                        Checked = riskAssessmentSection.Check19Value
                    });

                    riskAssessment.Add(new RiskAssessmentViewModel()
                    {
                        Name = riskAssessmentSection.Check20,
                        Checked = riskAssessmentSection.Check20Value
                    });

                    riskAssessment.Add(new RiskAssessmentViewModel()
                    {
                        Name = riskAssessmentSection.Check21,
                        Checked = riskAssessmentSection.Check21Value
                    });

                    riskAssessment.Add(new RiskAssessmentViewModel()
                    {
                        Name = riskAssessmentSection.Check22,
                        Checked = riskAssessmentSection.Check22Value
                    });

                    riskAssessment.Add(new RiskAssessmentViewModel()
                    {
                        Name = riskAssessmentSection.Check23,
                        Checked = riskAssessmentSection.Check23Value
                    });

                    riskAssessment.Add(new RiskAssessmentViewModel()
                    {
                        Name = riskAssessmentSection.Check24,
                        Checked = riskAssessmentSection.Check24Value
                    });

                    riskAssessment.Add(new RiskAssessmentViewModel()
                    {
                        Name = riskAssessmentSection.Check25,
                        Checked = riskAssessmentSection.Check25Value
                    });

                    #endregion

                    #region Additional Hazards

                    foreach (var item in AdditionalHazard.GetHazardList().Hazards)
                    {
                        additionalHazards.Add(new AdditionalHazardsViewModel()
                        {
                            AdditionalControlMeasuresRequired = item.AdditionalControlMeasuresRequired,
                            Hazard = item.Hazard
                        });
                    }

                    #endregion

                    #region FGas

                    var fGasSection = FGas.GetSectionFGas();

                    fGasLeak.LeakTestDate = fGasSection.LTDate;
                    fGasLeak.LeakTestStartTime = fGasSection.LTStartTime;
                    fGasLeak.LeakTestFinishTime = fGasSection.LTFinishTime;
                    fGasLeak.LeakTestSystemNumber = fGasSection.LTSystemNumber;
                    fGasLeak.LeakTestLeakFound = fGasSection.LTLeakFoundYes;
                    fGasLeak.LeakTestLocationOfLeak = fGasSection.LTLocationOfLeak;
                    fGasLeak.DetailsOfRepair = fGasSection.LTDetailsOfRepair;
                    fGasLeak.ActionTakenToPreventFutureLeak = fGasSection.LTActionTaken;

                    fGasReturnLeak.ReturnLeakTestDate = fGasSection.RLTDate;
                    fGasReturnLeak.ReturnLeakTestStartTime = fGasSection.RLTStartTime;
                    fGasReturnLeak.ReturnLeakTestFinishTime = fGasSection.RLTFinishTime;
                    fGasReturnLeak.ReturnLeakTestSystemNumber = fGasSection.RLTSystemNumber;
                    fGasReturnLeak.ReturnLeakTestLeakFound = fGasSection.RLTLeakFoundYes;
                    fGasReturnLeak.ReturnLeakTestDetails = fGasSection.RLTDetails;

                    #region Site Works

                    var siteWorkSection = SiteWorks.GetSectionSiteWorks();

                    siteWorks.Make = siteWorkSection.Make.ToString();
                    siteWorks.Model = siteWorkSection.Model.ToString();
                    siteWorks.SerialNumber = siteWorkSection.SerialNumber.ToString();
                    siteWorks.AssetNumber = siteWorkSection.AssetNumber.ToString();
                    siteWorks.WorksCarriedOut = siteWorkSection.WorkedCarriedOut.ToString();
                    siteWorks.LeakTestDate = fGasSection.LTDate;
                    siteWorks.LeakTestStartTime = fGasSection.LTStartTime;
                    siteWorks.LeakTestFinishTime = fGasSection.LTFinishTime;
                    siteWorks.LeakTestSystemNumber = fGasSection.LTSystemNumber;
                    siteWorks.LeakTestLeakFound = fGasSection.LTLeakFoundYes;
                    siteWorks.LeakTestLocationOfLeak = fGasSection.LTLocationOfLeak;
                    siteWorks.DetailsOfRepair = fGasSection.LTDetailsOfRepair;
                    siteWorks.ActionTakenToPreventFutureLeak = fGasSection.LTActionTaken;
                    siteWorks.ReturnLeakTestDate = fGasSection.RLTDate;
                    siteWorks.ReturnLeakTestStartTime = fGasSection.RLTStartTime;
                    siteWorks.ReturnLeakTestFinishTime = fGasSection.RLTFinishTime;
                    siteWorks.ReturnLeakTestSystemNumber = fGasSection.RLTSystemNumber;
                    siteWorks.ReturnLeakTestLeakFound = fGasSection.RLTLeakFoundYes;
                    siteWorks.ReturnLeakTestDetails = fGasSection.RLTDetails;

                    #endregion

                    #endregion

                    #region Parts / Time / Misc

                    fGasMisc.TemperatureOnArrival = partsTimeMiscSection.TempOnArrival;
                    fGasMisc.TemperatureOnDepart = partsTimeMiscSection.TempOnDeparture;
                    fGasMisc.PlantLeftOperational = partsTimeMiscSection.PlantLeftOperational;
                    fGasMisc.JobComplete = partsTimeMiscSection.JobComplete;

                    foreach (var item in partsTimeMiscSection.PartsList)
                    {
                        materialsUsed.Add(new MaterialsUsedViewModel()
                        {
                            Quantity = item.PartQty,
                            PartNumber = item.PartNumber,
                            SupplierReference = item.PartSupplierRef,
                            DescriptionOfMaterials = item.PartDescription,
                            U = item.U,
                            W = item.W,
                            MaterialCost = item.PartMaterialCost,
                            MaterialCharges = item.PartMatCharges
                        });
                    }

                    foreach (var item in partsTimeMiscSection.TimeSheetList)
                    {
                        jobTimesheet.Add(new JobTimesheetViewModel()
                        {
                            Engineer = item.Engineer,
                            Mate = item.Mate,
                            Date = item.Date,
                            StartTime = item.StartTime,
                            TimeArrive = item.TimeArrive,
                            TimeDepart = item.TimeDepart,
                            HoursOnSite = item.HoursOnSite,
                            HoursTravel = item.HoursTravel,
                            TotalHours = item.TotalHours,
                            NormalTime = item.NormalTime,
                            NormalTimeAtRateOf = item.NormalTimeAtRateOf,
                            Overtime = item.Overtime,
                            OvertimeAtRateOf = item.OvertimeAtRateOf,
                            LabourChargeEngineer = item.LabourChargeEngineer,
                            LabourChargeMate = item.LabourChargeMate
                        });
                    }

                    #endregion

                    var personsAtRisk = PersonsRisk.GetSectionPersonsRisk();

                    var CustomerJob = CustomerJobNoEntry.Text;
                    var JGRJob = JgrJobNoEntry.Text;

                    if (!String.IsNullOrEmpty(CustomerJobNoEntry.Text) && String.IsNullOrEmpty(JgrJobNoEntry.Text))
                    {
                        JGRJob = " ";
                    }

                    if (!String.IsNullOrEmpty(JgrJobNoEntry.Text) && String.IsNullOrEmpty(CustomerJobNoEntry.Text))
                    {
                        CustomerJob = " ";
                    }

                    JobSheetobject = new JobSheetPageObject()
                    {
                        SiteAddress = SiteAddressEntry.Text ?? "",
                        CustomerJobNumber = CustomerJob,
                        JGRJobNumber = JGRJob,
                        CompletedBy = AssessmentByEntry.Text ?? "",
                        Position = PositionEntry.Text ?? "",
                        Date = DateAndTimePicker.Date,
                        Description = Description.GetSectionDescription(),
                        Location = Location.GetSectionLocation(),
                        SubInvoiceTotal = SubInvoiceTotal.Text ?? "",
                        ValueAddedTax = ValueAddedTax.Text ?? "",
                        TotalInvoice = TotalInvoice.Text ?? "",
                        PrintCustomerName = CustomerPrintName.Text ?? "",
                        CompletionDate = FormCompletionDate.Date
                    };

                    Byte[] assessmentBySignature = null;
                    Byte[] customerSignature = null;

                    var assessmentBySignatureImage = await SignatureSignaturePadView.GetImageStreamAsync(SignatureImageFormat.Png);
                    var customerSignatureImage = await CustomerSignatureSignaturePadView.GetImageStreamAsync(SignatureImageFormat.Png);

                    if (jobSheet != null)
                    {
                        var jsObject = JsonConvert.DeserializeObject<JobSheetObjectViewModel>(jobSheet.Content);

                        if (jsObject.Signature != null)
                        {
                            assessmentBySignature = jsObject.Signature;
                        }
                        else
                        {
                            if (assessmentBySignatureImage != null)
                            {
                                byte[] arr;

                                using (var ms = new MemoryStream())
                                {
                                    await assessmentBySignatureImage.CopyToAsync(ms);

                                    arr = ms.ToArray();
                                }

                                assessmentBySignature = arr;
                            }
                        }
                    }
                    else
                    {
                        if (assessmentBySignatureImage != null)
                        {
                            byte[] arr;

                            using (var ms = new MemoryStream())
                            {
                                await assessmentBySignatureImage.CopyToAsync(ms);

                                arr = ms.ToArray();
                            }

                            assessmentBySignature = arr;
                        }
                    }

                    if (jobSheet != null)
                    {
                        var jsObject = JsonConvert.DeserializeObject<JobSheetObjectViewModel>(jobSheet.Content);

                        if (jsObject.CustomerSignature != null)
                        {
                            customerSignature = jsObject.CustomerSignature;
                        }
                        else
                        {
                            if (customerSignatureImage != null)
                            {
                                byte[] arr;

                                using (var ms = new MemoryStream())
                                {
                                    await customerSignatureImage.CopyToAsync(ms);

                                    arr = ms.ToArray();
                                }

                                customerSignature = arr;
                            }
                        }
                    }
                    else
                    {
                        if (customerSignatureImage != null)
                        {
                            byte[] arr;

                            using (var ms = new MemoryStream())
                            {
                                await customerSignatureImage.CopyToAsync(ms);

                                arr = ms.ToArray();
                            }

                            customerSignature = arr;
                        }
                    }

                    if (FGasSwitch.IsChecked == false)
                    {
                        siteWorks.LeakTestDate = null;
                        siteWorks.ReturnLeakTestDate = null;
                    }

                    m.Content = JsonConvert.SerializeObject(new
                    {
                        CustomerSiteAddress = JobSheetobject.SiteAddress,
                        CustomerJobNumber = JobSheetobject.CustomerJobNumber,
                        JGRJobNumber = JobSheetobject.JGRJobNumber,
                        AssessmentCompletedBy = JobSheetobject.CompletedBy,
                        AssessmentCompletedDateTime = JobSheetobject.Date,
                        Position = JobSheetobject.Position,
                        Signature = assessmentBySignature,
                        IsFGas = FGasSwitch.IsChecked,
                        DescriptionOfWorksToBeCompleted = JobSheetobject.Description,
                        LocationOfWorksAndEquipment = JobSheetobject.Location,
                        PersonsAtRisk = new PersonsAtRiskViewModel()
                        {
                            JGREmployees = personsAtRisk.JGREmployees,
                            CustomerSitePersonnel = personsAtRisk.SitePersonnel,
                            YoungPersons = personsAtRisk.YoungPersons,
                            MembersOfPublicCustomers = personsAtRisk.Customers
                        },
                        BeforeYouStart = beforeYouStart,
                        RiskAssessment = riskAssessment,
                        AdditionalHazards = additionalHazards,
                        SiteWorks = siteWorks,
                        MaterialsUsed = materialsUsed,
                        JobTimesheet = jobTimesheet,
                        CustomerSignature = customerSignature,
                        CustomerPrintName = JobSheetobject.PrintCustomerName,
                        CustomerDate = JobSheetobject.CompletionDate,
                        SubInvoiceTotal = JobSheetobject.SubInvoiceTotal,
                        VAT = JobSheetobject.ValueAddedTax,
                        TotalInvoice = JobSheetobject.TotalInvoice,
                        TemperatureOnArrival = fGasMisc.TemperatureOnArrival,
                        TemperatureOnDepart = fGasMisc.TemperatureOnDepart,
                        PlantLeftOperational = fGasMisc.PlantLeftOperational == "Yes" ? true : false,
                        JobComplete = fGasMisc.JobComplete == "Yes" ? true : false
                    });

                    #endregion

                    if (!String.IsNullOrEmpty(Id.Text))
                    {
                        m.Id = Convert.ToInt32(Id.Text);

                        App.JobSheetsDatabase.UpdateJobSheet(m);

                        if (displayAlert)
                        {
                            ResetActivityIndicator();

                            await DisplayAlert("Saved", "This job sheet has been saved successfully.", "Acknowledge");
                        }
                    }
                    else
                    {
                        App.JobSheetsDatabase.InsertJobSheet(m);

                        await Navigation.PushAsync(new JobSheet(m.Id));

                        Navigation.RemovePage(this);
                    }
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
            var isConfirmed = false;

            var helpers = new Helpers();

            if (helpers.IsDeviceOnline())
            {
                isConfirmed = await DisplayAlert("Confirm", "Are you sure you want to submit this job sheet?", "Yes", "Cancel");
            }
            else
            {
                ResetActivityIndicator();

                await DisplayAlert("No Internet Connection", "You need to be connected to the internet in order to submit this job sheet.", "Acknowledge");
            }

            SaveJobSheetToDatabase(false, true);

            if (isConfirmed)
            {
                var jobSheet = App.JobSheetsDatabase.GetJobSheetById(Convert.ToInt32(Id.Text));

                var client = new HttpClient
                {
                    BaseAddress = new Uri(_url),
                };

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var ma = new JobSheetViewModel()
                {
                    Content = jobSheet.Content,
                    CreatedDate = jobSheet.CreatedDate,
                    CustomerId = jobSheet.CustomerId,
                    IsDeleted = false,
                    StaffId = jobSheet.StaffId
                };

                var jsonObject = JsonConvert.SerializeObject(ma).ToString();

                var response = await client.PostAsync("PostJobSheet", new StringContent(jsonObject, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var contents = await response.Content.ReadAsStringAsync();

                    var jsonResponse = JsonConvert.DeserializeObject<CreateJobSheetResponseViewModel>(contents);

                    App.JobSheetsDatabase.JobSheetSentToOffice(jobSheet.Id, jsonResponse.Id);

                    await DisplayAlert("Success", "Your job sheet has been successfully sent to the office.", "Back to Job Sheets");

                    await Navigation.PopAsync(true);
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

            var confirmed = await DisplayAlert("Confirm", "Are you sure want to delete this job sheet?", "Yes", "No");

            if (confirmed)
            {
                App.JobSheetsDatabase.DeleteJobSheet(Convert.ToInt32(Id.Text));

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

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () => {
                var result = await this.DisplayAlert("Alert!", "Do you really want to exit?", "Yes", "No");

                if (result) await this.Navigation.PopAsync();
            });

            return true;
        }

        private void IsFGas_CheckChanged(object sender, EventArgs e)
        {
            if (FGasSwitch.IsChecked == true)
            {
                NinthSection.IsVisible = true;
            }
            else
            {
                NinthSection.IsVisible = false;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            FormLayout.IsVisible = false;
            ActivityIndicator_StackLayout.IsVisible = false;
            SubmittedLayout.IsVisible = false;
            SearchLayout.IsVisible = true;
            isTapped = false;
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBarController = (SearchBar)sender;
            SearchResults.ItemsSource = GetCustomers(e.NewTextValue);
        }

        IEnumerable<CustomersViewModel> GetCustomers(string searchText = null)
        {
            var customers = new ObservableCollection<CustomersViewModel>();

            foreach (CustomersViewModel i in CustomerList.ItemsSource)
            {
                customers.Add(new CustomersViewModel()
                {
                    Name = i.Name,
                    Id = i.Id,
                    AddressLine1 = i.AddressLine1,
                    AddressLine2 = i.AddressLine2,
                    AddressLine3 = i.AddressLine3,
                    AddressLine4 = i.AddressLine4,
                    TownCity = i.TownCity,
                    Postcode = i.Postcode
                });
            }

            if (string.IsNullOrEmpty(searchText))
            {
                return customers;
            }

            return customers.Where(p => p.Name.ToLower().StartsWith(searchText));
        }

        private void SearchResults_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (isTapped == false)
                {
                    isTapped = true;
                    SearchLayout.IsVisible = false;
                    FormLayout.IsVisible = true;
                    SubmittedLayout.IsVisible = true;
                    CustomerList.SelectedItem = (BindingContext as JobSheetViewModel).CustomersList.FirstOrDefault(c => c.Name == ((CustomersViewModel)e.SelectedItem).Name);
                    SearchResults.SelectedItem = null;
                    SearchBarController.Text = null;
                }
            });

        }
    }
}