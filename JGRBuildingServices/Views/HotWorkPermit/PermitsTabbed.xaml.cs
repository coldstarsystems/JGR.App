using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices.Views.HotWorkPermit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PermitsTabbed : TabbedPage
    {
        private String _url = "https://api.jgr1.com";
        private Models.HotWorksPermit permit;
        public PermitsTabbed()
        {
            InitializeComponent();

            Children.Add(new Permits());
            Children.Add(new PermitsSentToOffice());
        }
    }
}