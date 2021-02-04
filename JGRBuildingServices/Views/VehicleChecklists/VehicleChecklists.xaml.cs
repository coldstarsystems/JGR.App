using JGRBuildingServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices.Views.VehicleChecklists
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VehicleChecklists : ContentPage
    {
        public VehicleChecklists()
        {
            InitializeComponent();

            VehicleChecklists_ListView.IsVisible = false;
            NoResults_StackLayout.IsVisible = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var vehicleChecklistsList = new ObservableCollection<VehicleChecklistsListViewModel>();

            VehicleChecklists_ListView.ItemsSource = vehicleChecklistsList;

            var vehicleChecklists = App.VehicleChecklistsDatabase.GetAllVehicleChecklists();

            if (vehicleChecklists.Count > 0)
            {
                VehicleChecklists_ListView.IsVisible = true;

                foreach (var item in vehicleChecklists)
                {
                    vehicleChecklistsList.Add(new VehicleChecklistsListViewModel()
                    {
                        Id = item.Id,
                        CreatedDate = item.CreatedDate,
                        Registration = item.Registration,
                        Mileage = item.Mileage
                    });
                }
            }
            else
            {
                NoResults_StackLayout.IsVisible = true;
            }
        }

        private void VehicleChecklistsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var vehicleChecklistId = ((VehicleChecklistsListViewModel)e.SelectedItem).Id;

            Navigation.PushAsync(new VehicleChecklist(vehicleChecklistId));
        }

        private async void Create_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new VehicleChecklist());
        }
    }
}