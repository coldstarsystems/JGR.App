using JGRBuildingServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices.Views.JobSheets
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class JobSheetsSentToOffice : ContentPage
    {
        public JobSheetsSentToOffice()
        {
            InitializeComponent();

            JobSheets_ListView.IsVisible = false;
            NoResults_StackLayout.IsVisible = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var jobSheetsList = new ObservableCollection<JobSheetsListViewModel>();

            JobSheets_ListView.ItemsSource = jobSheetsList;

            var jobSheets = App.JobSheetsDatabase.GetAllJobSheetsSentToOffice();

            if (jobSheets.Count > 0)
            {
                JobSheets_ListView.IsVisible = true;

                foreach (var item in jobSheets)
                {
                    jobSheetsList.Add(new JobSheetsListViewModel()
                    {
                        Id = item.Id,
                        CreatedDate = item.CreatedDate,
                        CustomerName = item.CustomerName
                    });
                }
            }
            else
            {
                NoResults_StackLayout.IsVisible = true;
            }
        }

        private void JobSheetsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var jobSheetId = ((JobSheetsListViewModel)e.SelectedItem).Id;

            Navigation.PushAsync(new JobSheet(jobSheetId));
        }

        private async void Create_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new JobSheet());
        }
    }
}