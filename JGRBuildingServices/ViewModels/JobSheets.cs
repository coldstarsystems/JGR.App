using System;
using System.Collections.Generic;
using System.Text;

namespace JGRBuildingServices.ViewModels
{
    public class JobSheetsListViewModel
    {
        public Int32 Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public String CustomerName { get; set; }
    }

    public class JobSheetViewModel
    {
        public JobSheetViewModel()
        {

        }

        private List<CustomersViewModel> _customersList { get; set; }

        public List<CustomersViewModel> CustomersList
        {
            get { return _customersList; }
            set
            {
                if (_customersList != value)
                {
                    _customersList = value;
                }
            }
        }

        public Int32 Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public Int32 StaffId { get; set; }

        public Int32 CustomerId { get; set; }

        public String Content { get; set; }

        public Boolean IsDeleted { get; set; }

        public JobSheetObjectViewModel JobSheetObject { get; set; }
    }

    public class CreateJobSheetResponseViewModel
    {
        public Int32 Id { get; set; }

        public String ErrorMessage { get; set; }
    }

    public class JobSheetObjectViewModel
    {
        public String CustomerSiteAddress { get; set; }

        public String CustomerJobNumber { get; set; }

        public String JGRJobNumber { get; set; }

        public String AssessmentCompletedBy { get; set; }

        public DateTime AssessmentCompletedDateTime { get; set; }

        public Boolean IsFGas { get; set; }

        public String Position { get; set; }

        public Byte[] Signature { get; set; }

        public String DescriptionOfWorksToBeCompleted { get; set; }

        public String LocationOfWorksAndEquipment { get; set; }

        public Byte[] CustomerSignature { get; set; }

        public String CustomerPrintName { get; set; }

        public DateTime CustomerDate { get; set; }

        public String SubInvoiceTotal { get; set; }

        public String VAT { get; set; }

        public String TotalInvoice { get; set; }

        public PersonsAtRiskViewModel PersonsAtRisk { get; set; }

        public List<BeforeYouStartViewModel> BeforeYouStart { get; set; }

        public List<RiskAssessmentViewModel> RiskAssessment { get; set; }

        public List<AdditionalHazardsViewModel> AdditionalHazards { get; set; }

        public SiteWorksViewModel SiteWorks { get; set; }

        public FGasViewModel FGas { get; set; }

        public List<MaterialsUsedViewModel> MaterialsUsed { get; set; }

        public List<JobTimesheetViewModel> JobTimesheet { get; set; }

        public String TemperatureOnArrival { get; set; }

        public String TemperatureOnDepart { get; set; }

        public String PlantLeftOperational { get; set; }

        public String JobComplete { get; set; }
    }

    public class PersonsAtRiskViewModel
    {
        public Boolean JGREmployees { get; set; }

        public Boolean YoungPersons { get; set; }

        public Boolean CustomerSitePersonnel { get; set; }

        public Boolean MembersOfPublicCustomers { get; set; }
    }

    public class BeforeYouStartViewModel
    {
        public String Name { get; set; }

        public String Value { get; set; }

        public String Comments { get; set; }
    }

    public class RiskAssessmentViewModel
    {
        public String Name { get; set; }

        public Boolean Checked { get; set; }
    }

    public class AdditionalHazardsViewModel
    {
        public String Hazard { get; set; }

        public String AdditionalControlMeasuresRequired { get; set; }
    }

    public class SiteWorksViewModel
    {
        public String Make { get; set; }

        public String Model { get; set; }

        public String SerialNumber { get; set; }

        public String AssetNumber { get; set; }

        public String WorksCarriedOut { get; set; }

        public DateTime? LeakTestDate { get; set; }

        public TimeSpan? LeakTestStartTime { get; set; }

        public TimeSpan? LeakTestFinishTime { get; set; }

        public String LeakTestSystemNumber { get; set; }

        public Boolean? LeakTestLeakFound { get; set; }

        public String LeakTestLocationOfLeak { get; set; }

        public String DetailsOfRepair { get; set; }

        public String ActionTakenToPreventFutureLeak { get; set; }

        public DateTime? ReturnLeakTestDate { get; set; }

        public TimeSpan? ReturnLeakTestStartTime { get; set; }

        public TimeSpan? ReturnLeakTestFinishTime { get; set; }

        public String ReturnLeakTestSystemNumber { get; set; }

        public Boolean? ReturnLeakTestLeakFound { get; set; }

        public String ReturnLeakTestDetails { get; set; }

    }

    public class FGasViewModel
    {
        public LeakTestViewModel LeakTest { get; set; }

        public ReturnLeakTestViewModel ReturnLeakTest { get; set; }

        public FGasMiscViewModel FGasMisc { get; set; }
    }

    public class LeakTestViewModel
    {
        public DateTime? LeakTestDate { get; set; }

        public TimeSpan? LeakTestStartTime { get; set; }

        public TimeSpan? LeakTestFinishTime { get; set; }

        public String LeakTestSystemNumber { get; set; }

        public Boolean? LeakTestLeakFound { get; set; }

        public String LeakTestLocationOfLeak { get; set; }

        public String DetailsOfRepair { get; set; }

        public String ActionTakenToPreventFutureLeak { get; set; }
    }

    public class ReturnLeakTestViewModel
    {
        public DateTime? ReturnLeakTestDate { get; set; }

        public TimeSpan? ReturnLeakTestStartTime { get; set; }

        public TimeSpan? ReturnLeakTestFinishTime { get; set; }

        public String ReturnLeakTestSystemNumber { get; set; }

        public Boolean? ReturnLeakTestLeakFound { get; set; }

        public String ReturnLeakTestDetails { get; set; }
    }

    public class MaterialsUsedViewModel
    {
        public String Quantity { get; set; }

        public String PartNumber { get; set; }

        public String SupplierReference { get; set; }

        public String DescriptionOfMaterials { get; set; }

        public Boolean U { get; set; }

        public Boolean W { get; set; }

        public String MaterialCost { get; set; }

        public String MaterialCharges { get; set; }
    }

    public class JobTimesheetViewModel
    {
        public String Engineer { get; set; }

        public String Mate { get; set; }

        public DateTime? Date { get; set; }

        public TimeSpan? StartTime { get; set; }

        public TimeSpan? TimeArrive { get; set; }

        public TimeSpan? TimeDepart { get; set; }

        public String HoursOnSite { get; set; }

        public String HoursTravel { get; set; }

        public String TotalHours { get; set; }

        public String NormalTime { get; set; }

        public String NormalTimeAtRateOf { get; set; }

        public String Overtime { get; set; }

        public String OvertimeAtRateOf { get; set; }

        public String LabourChargeEngineer { get; set; }

        public String LabourChargeMate { get; set; }
    }

    public class FGasMiscViewModel
    {
        public String TemperatureOnArrival { get; set; }

        public String TemperatureOnDepart { get; set; }

        public String PlantLeftOperational { get; set; }

        public String JobComplete { get; set; }
    }
}