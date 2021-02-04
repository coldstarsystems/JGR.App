using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices
{
    public class HazardAccordionView
    {
        public string Hazard { get; set; }
        public string AdditionalControlMeasuresRequired { get; set; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HazardView : ContentView
    {
        public HazardView()
        {
            InitializeComponent();
        }

        public HazardView(HazardAccordionView inspection)
        {
            InitializeComponent();
            Hazard.Text = inspection.Hazard;
            ControlMeasure.Text = inspection.AdditionalControlMeasuresRequired;
        }

        private void DeleteRowButton_Clicked(object sender, System.EventArgs e)
        {
            Hazard.Text = null;
            ControlMeasure.Text = null;
            HazardParent.Children.Clear();
        }

        internal HazardAccordionView GetHazard()
        {
            return new HazardAccordionView
            {
                Hazard = (Hazard.Text == null ? "" : Hazard.Text),
                AdditionalControlMeasuresRequired = (ControlMeasure.Text == null ? "" : ControlMeasure.Text)
            };
        }
    }
}
