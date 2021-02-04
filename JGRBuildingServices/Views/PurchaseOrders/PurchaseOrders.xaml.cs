using JGRBuildingServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices.Views.PurchaseOrders
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PurchaseOrders : ContentPage
    {
        public PurchaseOrders()
        {
            InitializeComponent();

            PurchaseOrders_ListView.IsVisible = false;
            NoResults_StackLayout.IsVisible = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var purchaseOrdersList = new ObservableCollection<PurchaseOrdersListViewModel>();

            PurchaseOrders_ListView.ItemsSource = purchaseOrdersList;

            var purchaseOrders = App.PurchaseOrdersDatabase.GetAllPurchaseOrders();

            if (purchaseOrders.Count > 0)
            {
                PurchaseOrders_ListView.IsVisible = true;

                foreach (var item in purchaseOrders)
                {
                    purchaseOrdersList.Add(new PurchaseOrdersListViewModel()
                    {
                        Id = item.Id,
                        CreatedDate = item.CreatedDate,
                        Number = item.Number
                    });
                }
            }
            else
            {
                NoResults_StackLayout.IsVisible = true;
            }
        }

        private void PurchaseOrdersList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var purchaseOrderId = ((PurchaseOrdersListViewModel)e.SelectedItem).Id;

            Navigation.PushAsync(new PurchaseOrder(purchaseOrderId));
        }

        private async void Create_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PurchaseOrder());
        }
    }
}