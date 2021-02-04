using JGRBuildingServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices.Views.PurchaseOrders
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListItems : ContentView
	{
		public ListItems()
		{
			InitializeComponent();
		}

        public ListItems(PurchaseOrderLineItemViewModel model)
        {
            InitializeComponent();

            Quantity.Text = model.Quantity?.ToString();
            Item.Text = model.Item;
            Description.Text = model.Description;
            Job.Text = model.Job;
            UnitPrice.Text = model.UnitPrice?.ToString();
            LineTotal.Text = model.LineTotal?.ToString();
        }

        public PurchaseOrderLineItemViewModel GetItems()
        {
            return new PurchaseOrderLineItemViewModel
            {
                Quantity = Quantity.Text != null ? Convert.ToDecimal(Quantity.Text) : 0,
                Item = Item.Text,
                Description = Description.Text,
                Job = Job.Text,
                UnitPrice = UnitPrice.Text != null ? Convert.ToDecimal(UnitPrice.Text) : 0,
                LineTotal = LineTotal.Text != null ? Convert.ToDecimal(LineTotal.Text) : 0
            };
        }

        private void Remove_Clicked(object sender, EventArgs e)
        {
            Quantity.Text = null;
            Item.Text = null;
            Description.Text = null;
            Job.Text = null;
            UnitPrice.Text = null;
            LineTotal.Text = null;

            ListItemsStackLayout.Children.Clear();
        }
    }
}