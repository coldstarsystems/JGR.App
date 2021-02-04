using JGRBuildingServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices
{

    public class PartsAccordion
    {
        public List<PartsAccordionView> Part { get; set; }

        public PartsAccordion()
        {
            Part = new List<PartsAccordionView>();
        }
    }

    public class PartsAccordionView
    {
        public string PartQty { get; set; }
        public string PartNumber { get; set; }
        public string PartSupplierRef { get; set; }
        public string PartDescription { get; set; }
        public Boolean U { get; set; }
        public Boolean W { get; set; }
        public string PartMaterialCost { get; set; }
        public string PartMatCharges { get; set; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartsView : ContentView
    {
        public PartsView()
        {
            InitializeComponent();
        }

        public PartsView(PartsAccordionView detail)
        {
            InitializeComponent();

            PartQty.Text = detail.PartQty;
            PartNumber.Text = detail.PartNumber;
            PartSupplierRef.Text = detail.PartSupplierRef;
            PartDescription.Text = detail.PartDescription;
            PartU.IsChecked = detail.U;
            PartW.IsChecked = detail.W;
            PartMaterialCost.Text = detail.PartMaterialCost;
            PartMatCharges.Text = detail.PartMatCharges;
        }

        private void DeleteRowButton_Clicked(object sender, System.EventArgs e)
        {
            PartQty.Text = null;
            PartNumber.Text = null;
            PartSupplierRef.Text = null;
            PartDescription.Text = null;
            PartU.IsChecked = false;
            PartW.IsChecked = false;
            PartMaterialCost.Text = null;
            PartMatCharges.Text = null;
            PartsParent.Children.Clear();
        }

        public PartsAccordionView GetPart()
        {
            return new PartsAccordionView
            {
                PartQty = PartQty.Text == null ? "" : PartQty.Text,
                PartNumber = PartNumber.Text == null ? "" : PartNumber.Text,
                PartSupplierRef = PartSupplierRef.Text == null ? "" : PartSupplierRef.Text,
                PartDescription = PartDescription.Text == null ? "" : PartDescription.Text,
                U = PartU.IsChecked,
                W = PartW.IsChecked,
                PartMaterialCost = PartMaterialCost.Text == null ? "" : PartMaterialCost.Text,
                PartMatCharges = PartMatCharges.Text == null ? "" : PartMatCharges.Text,
            };
        }
    }
}
