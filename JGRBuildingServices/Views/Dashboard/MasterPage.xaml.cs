using System;
using Xamarin.Forms;

namespace JGRBuildingServices.Views.Dashboard
{
    public partial class MasterPage : ContentPage
    {
        public MasterPage()
        {
            InitializeComponent();

            LastSyncedLabel.Text = App.SettingsDatabase.GetSetting("LastSynced").LastSynced.ToString("MM/dd/yyyy HH:mm");

        }
    }
}