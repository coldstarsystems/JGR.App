using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices.Views.DailyTimesheets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimesheetTabbed : TabbedPage
    {
        public TimesheetTabbed()
        {
            InitializeComponent();

            Children.Add(new DailyTimesheets());
            Children.Add(new TimesheetSentToOffice());
        }
    }
}