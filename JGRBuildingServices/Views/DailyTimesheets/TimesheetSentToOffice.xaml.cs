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
    public partial class TimesheetSentToOffice : ContentPage
    {
        public TimesheetSentToOffice()
        {
            InitializeComponent();

            TimeSheets_ListView.IsVisible = false;
            NoResults_StackLayout.IsVisible = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var timeSheetsList = new ObservableCollection<DailyTimesheetsListViewModel>();

            TimeSheets_ListView.ItemsSource = timeSheetsList;

            var timeSheets = App.DailyTimesheetsDatabase.GetAllDailyTimesheetsSentToOffice();

            if (timeSheets.Count > 0)
            {
                TimeSheets_ListView.IsVisible = true;

                foreach (var item in timeSheets)
                {
                    timeSheetsList.Add(new DailyTimesheetsListViewModel()
                    {
                        Id = item.Id,
                        Date = item.Date, 
                    });
                }
            }
            else
            {
                NoResults_StackLayout.IsVisible = true;
            }
        }

        private void TimeSheetsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var timeSheetId = ((DailyTimesheetsListViewModel)e.SelectedItem).Id;

            Navigation.PushAsync(new DailyTimesheet(timeSheetId));
        }

        private async void Create_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DailyTimesheet());
        }
    }
}