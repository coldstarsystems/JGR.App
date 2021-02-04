using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace JGRBuildingServices.ViewModels
{
    public class HotWorksPermitViewModel
    {
        public DateTime CreatedDate { get; set; }

        public String Content { get; set; }
    }

    public class HotWorkPermitListViewModel
    {
        public Int32 Id { get; set; }
        public String Content { get; set; }
        public String PermitNo { get; set; }
        public String SiteName { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class HotWorkPermitViewModel
    {
        [PrimaryKey, AutoIncrement]
        public Int32 Id { get; set; }
        public String PermitNo { get; set; }
        public String SiteName { get; set; }
        public String LocationArea { get; set; }
        public String EquipmentPlantToBeWorkedOn { get; set; }
        public String NatureOfWork { get; set; }
        public RequestOfPermit RequestOfPermit { get; set; }
        public IssueOfPermit IssueOfPermit { get; set; }
        public ReturnOfPermit ReturnOfPermit { get; set; }
        public List<PermitChecklist> PermitChecklist { get; set; }
    }

    public class RequestOfPermit
    {
        public String RequestMinutes { get; set; }
        public Byte[] RequestSigned { get; set; }
        public String RequestPrintName { get; set; }
        public String RequestCompany { get; set; }
        public String RequestJobTitle { get; set; }
        public DateTime? RequestDate { get; set; }
        public TimeSpan? RequestTime { get; set; }
    }

    public class IssueOfPermit
    {
        public Byte[] IssueSigned { get; set; }
        public String IssuePrintName { get; set; }
        public String IssueCompany { get; set; }
        public String IssueJobTitle { get; set; }
        public DateTime? IssueDate { get; set; }
        public TimeSpan? IssueTime { get; set; }
        public TimeSpan? IssueTimePermit { get; set; }
        public TimeSpan? IssueExpiryTimePermit { get; set; }
    }

    public class ReturnOfPermit
    {
        public String ReturnMinutes { get; set; }
        public Byte[] ReturnSigned { get; set; }
        public String ReturnPrintName { get; set; }
        public String ReturnCompany { get; set; }
        public String ReturnJobTitle { get; set; }
        public DateTime? ReturnDate { get; set; }
        public TimeSpan? ReturnTime { get; set; }
        public Boolean ReturnCompleted { get; set; }

        public Boolean ReturnCancelled { get; set; }
    }

    public class PermitChecklist
    {
        public String Id { get; set; }
        public String Question { get; set; }
        public String InPlace { get; set; }
        public String Comments { get; set; }
    }

    public class CreateHotWorksPermitResponseViewModel
    {
        public Int32 Id { get; set; }

        public String ErrorMessage { get; set; }
    }
}
