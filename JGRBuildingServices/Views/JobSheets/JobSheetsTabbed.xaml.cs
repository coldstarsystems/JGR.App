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

namespace JGRBuildingServices.Views.JobSheets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobSheetsTabbed : TabbedPage
    {
        private String _url = "https://api.jgr1.com";
        private Models.Staff staff;

        public JobSheetsTabbed()
        {
            InitializeComponent();

            Children.Add(new JobSheets());
            Children.Add(new JobSheetsSentToOffice());
        }
    }
}