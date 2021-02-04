using JGRBuildingServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices.Views.DailyTimesheets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DailyTimesheets : ContentPage
    {
        public DailyTimesheets()
        {
            InitializeComponent();

            DailyTimesheets_ListView.IsVisible = false;
            NoResults_StackLayout.IsVisible = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var dailyTimesheetsList = new ObservableCollection<DailyTimesheetsListViewModel>();

            DailyTimesheets_ListView.ItemsSource = dailyTimesheetsList;

            var dailyTimesheets = App.DailyTimesheetsDatabase.GetAllDailyTimesheets();

            if (dailyTimesheets.Count > 0)
            {
                DailyTimesheets_ListView.IsVisible = true;

                foreach (var item in dailyTimesheets)
                {
                    dailyTimesheetsList.Add(new DailyTimesheetsListViewModel()
                    {
                        Id = item.Id,
                        Date = item.Date
                    });
                }
            }
            else
            {
                NoResults_StackLayout.IsVisible = true;
            }
        }

        private void DailyTimesheetsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var dailyTimesheetId = ((DailyTimesheetsListViewModel)e.SelectedItem).Id;

            Navigation.PushAsync(new DailyTimesheet(dailyTimesheetId));
        }

        private async void Create_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DailyTimesheet());
        }
    }
}