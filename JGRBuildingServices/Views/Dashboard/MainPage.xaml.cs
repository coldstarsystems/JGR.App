using JGRBuildingServices.ViewModels;
using System;
using Xamarin.Forms;

namespace JGRBuildingServices.Views.Dashboard
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();

            masterPage.listView.ItemSelected += OnItemSelected;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItemsViewModel;

            if (item != null)
            {
                Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType));

                masterPage.listView.SelectedItem = null;

                IsPresented = false;
            }
        }
    }
}