using JGRBuildingServices.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices.Views.HotWorkPermit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PermitsSentToOffice : ContentPage
    {
        public PermitsSentToOffice()
        {
            InitializeComponent();

            Permits_ListView.IsVisible = false;
            NoResults_StackLayout.IsVisible = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var hotWorksPermitList = new ObservableCollection<HotWorkPermitListViewModel>();

            Permits_ListView.ItemsSource = hotWorksPermitList;

            var hotWorksPermit = App.HotWorksPermitDatabase.GetAllHotWorksPermitsSentToOffice();

            if (hotWorksPermit.Count > 0)
            {
                Permits_ListView.IsVisible = true;

                foreach (var item in hotWorksPermit)
                {
                    var worksPermitObject = JsonConvert.DeserializeObject<HotWorkPermitViewModel>(item.Content);

                    hotWorksPermitList.Add(new HotWorkPermitListViewModel()
                    {
                        Id = item.Id,
                        PermitNo = worksPermitObject.PermitNo,
                        SiteName = worksPermitObject.SiteName,
                        CreatedDate = item.CreatedDate
                    });
                }
            }
            else
            {
                NoResults_StackLayout.IsVisible = true;
            }
        }

        private void Permits_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var hotWorksPermitId = ((HotWorkPermitListViewModel)e.SelectedItem).Id;

            Navigation.PushAsync(new Permit(hotWorksPermitId));
        }

        private async void Create_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Permit());
        }
    }
}