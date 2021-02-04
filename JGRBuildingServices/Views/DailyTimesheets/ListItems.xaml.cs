using JGRBuildingServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices.Views.DailyTimesheets
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListItems : ContentView
	{
        public ListItems()
        {
            InitializeComponent();
        }

        public ListItems(DailyTimesheetLineItemViewModel model)
        {
            InitializeComponent();

            JobNumber.Text = model.JobNumber?.ToString();
            SiteAddress.Text = model.SiteAddress;
            StartJob.Time = model.StartJob.Value;
            ArriveSite.Time = model.ArriveSite.Value;
            LeaveSite.Time = model.LeaveSite.Value;
            FinishJob.Time = model.FinishJob.Value;
            Complete.SelectedItem = model.Complete == true ? Complete.SelectedItem = "Yes" : Complete.SelectedItem = "No";
            Comment.Text = model.Comment;
            JobHours.Text = model.JobHours?.ToString();
        }

        public DailyTimesheetLineItemViewModel GetItems()
        {
            return new DailyTimesheetLineItemViewModel
            {
                JobNumber = JobNumber.Text != null ? Convert.ToInt32(JobNumber.Text) : 0,
                SiteAddress = SiteAddress.Text,
                StartJob = StartJob.Time,
                ArriveSite = ArriveSite.Time,
                LeaveSite = LeaveSite.Time,
                FinishJob = FinishJob.Time,
                Complete = Complete.SelectedIndex != -1 ? (Complete.SelectedItem.ToString() == "Yes" ? true : false) : false,
                Comment = Comment.Text,
                JobHours = JobHours.Text != null ? Convert.ToDecimal(JobHours.Text) : 0
            };
        }

        private void Remove_Clicked(object sender, EventArgs e)
        {
            var timeSpan = new TimeSpan(0, 0, 0, 0, 0);

            JobNumber.Text = null;
            SiteAddress.Text = null;
            StartJob.Time = timeSpan;
            ArriveSite.Time = timeSpan;
            LeaveSite.Time = timeSpan;
            FinishJob.Time = timeSpan;
            Complete.SelectedIndex = -1;
            Comment.Text = null;
            JobHours.Text = null;

            ListItemsStackLayout.Children.Clear();
        }
    }
}