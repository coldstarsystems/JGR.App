using JGRBuildingServices.ViewModels;
using JGRBuildingServices.Views.JobSheets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PartsTimeMiscAccordion : ContentView
    {
        public PartsTimeMiscAccordion()
        {
            InitializeComponent();
            IsBodyVisible = false;
        }

        #region Bindable Properties
        public static BindableProperty HeaderBackgroundColorProperty =
            BindableProperty.Create(nameof(HeaderBackgroundColor),
                typeof(Color),
                typeof(PartsTimeMiscAccordion),
                defaultValue: Color.Transparent,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((PartsTimeMiscAccordion)bindable).UpdateHeaderBackgroundColor();
                });

        public static BindableProperty HeaderOpenedBackgroundColorProperty =
            BindableProperty.Create(nameof(HeaderOpenedBackgroundColor),
                typeof(Color),
                typeof(PartsTimeMiscAccordion),
                defaultValue: Color.Transparent,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((PartsTimeMiscAccordion)bindable).UpdateHeaderBackgroundColor();
                });

        public static BindableProperty HeaderTextColorProperty =
            BindableProperty.Create(nameof(HeaderTextColor),
                typeof(Color),
                typeof(PartsTimeMiscAccordion),
                defaultValue: Color.Black,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((PartsTimeMiscAccordion)bindable).UpdateHeaderTextColor((Color)newVal);
                });

        public static BindableProperty HeaderTextProperty =
            BindableProperty.Create(nameof(HeaderTextProperty),
                typeof(string),
                typeof(PartsTimeMiscAccordion),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((PartsTimeMiscAccordion)bindable).UpdateHeaderText((string)newVal);
                });

        public static BindableProperty BodyTextColorProperty =
            BindableProperty.Create(nameof(BodyTextColor),
                typeof(Color),
                typeof(PartsTimeMiscAccordion),
                defaultValue: Color.Black,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((PartsTimeMiscAccordion)bindable).UpdateBodyTextColor((Color)newVal);
                });

        public static BindableProperty BodyTextProperty =
            BindableProperty.Create(nameof(BodyText),
                typeof(string),
                typeof(PartsTimeMiscAccordion),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((PartsTimeMiscAccordion)bindable).UpdateBodyText((string)newVal);
                });


        public static BindableProperty IsBodyVisibleProperty =
            BindableProperty.Create(nameof(IsBodyVisible),
                typeof(bool),
                typeof(PartsTimeMiscAccordion),
                defaultValue: true,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((PartsTimeMiscAccordion)bindable).UpdateBodyVisibility((bool)newVal);
                });
        #endregion

        #region Public Properties
        public Color HeaderBackgroundColor
        {
            get
            {
                return (Color)GetValue(HeaderBackgroundColorProperty);
            }
            set
            {
                SetValue(HeaderBackgroundColorProperty, value);
            }
        }
        public Color HeaderOpenedBackgroundColor
        {
            get
            {
                return (Color)GetValue(HeaderOpenedBackgroundColorProperty);
            }
            set
            {
                SetValue(HeaderOpenedBackgroundColorProperty, value);
            }
        }
        public Color HeaderTextColor
        {
            get
            {
                return (Color)GetValue(HeaderTextColorProperty);
            }
            set
            {
                SetValue(HeaderTextColorProperty, value);
            }
        }
        public string HeaderText
        {
            get
            {
                return (string)GetValue(HeaderTextProperty);
            }
            set
            {
                SetValue(HeaderTextProperty, value);
            }
        }
        public Color BodyTextColor
        {
            get
            {
                return (Color)GetValue(BodyTextColorProperty);
            }
            set
            {
                SetValue(BodyTextColorProperty, value);
            }
        }
        public string BodyText
        {
            get
            {
                return (string)GetValue(BodyTextProperty);
            }
            set
            {
                SetValue(BodyTextProperty, value);
            }
        }

        public bool IsBodyVisible
        {
            get
            {
                return (bool)GetValue(IsBodyVisibleProperty);
            }
            set
            {
                SetValue(IsBodyVisibleProperty, value);
            }
        }

        #endregion

        int Timecount = 1;

        /// <param name="color">Color.</param>
        public void UpdateHeaderBackgroundColor(Color color)
        {
            HeaderView.BackgroundColor = color;
        }

        public void UpdateHeaderBackgroundColor()
        {
            if (IsBodyVisible)
            {
                HeaderView.BackgroundColor = HeaderOpenedBackgroundColor;
                BodyView.BackgroundColor = Color.White;
            }
            else
            {
                HeaderView.BackgroundColor = HeaderBackgroundColor;
            }
        }

        /// <param name="color">Color.</param>
        public void UpdateHeaderTextColor(Color color)
        {
            HeaderLabel.TextColor = color;
        }


        /// <param name="color">Color.</param>
        public void UpdateBodyTextColor(Color color)
        {

        }

        /// <param name="text">Text.</param>
        public void UpdateHeaderText(string text)
        {
            HeaderLabel.Text = text;
        }

        /// <param name="text">Text.</param>
        public void UpdateBodyText(string text)
        {

        }

        public void UpdateBodyVisibility(bool isVisible)
        {
            BodyView.IsVisible = isVisible;
            IndicatorLabel.Text = "+";
            if (isVisible)
            {
                IndicatorLabel.RotateTo(45, 100);
            }
            else
            {
                IndicatorLabel.RotateTo(0, 100);
            }
        }

        private void Handle_Tapped(object sender, System.EventArgs e)
        {
            IsBodyVisible = !IsBodyVisible;
            UpdateHeaderBackgroundColor();
        }

        private void AddMoreParts_Handle_Tapped(object sender, System.EventArgs e)
        {
            AddMoreParts.Children.Add(new PartsView()
            {

            });
        }

        private void AddMoreTime_Handle_Tapped(object sender, System.EventArgs e)
        {
            Timecount++;

            AddMoreTimeSection.Children.Add(new TimeSheetAccordionView()
            {
                HeaderText = $"Time Sheet {Timecount.ToString()}"
            });
        }

        public SectionFGas GetSectionFGas()
        {

            SectionFGas fgas = new SectionFGas
            {
                TempOnArrival = (TempOnArrival.Text == null ? "" : TempOnArrival.Text),
                TempOnDeparture = (TempOnDeparture.Text == null ? "" : TempOnDeparture.Text),
                PlantLeftOperational = (PlantLeftOperational.SelectedItem == null ? "" : PlantLeftOperational.SelectedItem.ToString()),
                JobComplete = (JobComplete.SelectedItem == null ? "" : JobComplete.SelectedItem.ToString()),
            };

            foreach (PartsView view in AddMoreParts.Children)
            {
                fgas.PartsList.Add(view.GetPart());
            }

            foreach (TimeSheetAccordionView view in AddMoreTimeSection.Children)
            {
                fgas.TimeSheetList.Add(view.GetTimeSheet());
            }

            return fgas;
        }

        public void SetSectionPartsTimeMisc(FGasMiscViewModel job)
        {
            TempOnArrival.Text = job.TemperatureOnArrival;
            TempOnDeparture.Text = job.TemperatureOnDepart;
            PlantLeftOperational.SelectedItem = job.PlantLeftOperational;
            JobComplete.SelectedItem = job.JobComplete;

            if (PlantLeftOperational.SelectedItem.ToString() == "true")
            {
                PlantLeftOperational.SelectedIndex = 0;
            }
            else
            {
                PlantLeftOperational.SelectedIndex = 1;
            }

            if (JobComplete.SelectedItem.ToString() == "true")
            {
                JobComplete.SelectedIndex = 0;
            }
            else
            {
                JobComplete.SelectedIndex = 1;
            }
        }

        public void SetSectionParts(JobSheetObjectViewModel job)
        {
            AddMoreParts.Children.Clear();

            foreach (var child in job.MaterialsUsed)
            {
                var detail = new PartsAccordionView();


                detail.PartQty = child.Quantity;
                detail.PartNumber = child.PartNumber;
                detail.PartSupplierRef = child.SupplierReference;
                detail.PartDescription = child.DescriptionOfMaterials;
                detail.U = child.U;
                detail.W = child.W;
                detail.PartMaterialCost = child.MaterialCost;
                detail.PartMatCharges = child.MaterialCharges;
                AddMoreParts.Children.Add(new PartsView(detail));
            }
        }

        public void SetSectionTime(JobSheetObjectViewModel job)
        {
            AddMoreTimeSection.Children.Clear();
            var timeSheetCount = 0;

            foreach (var timeSheet in job.JobTimesheet)
            {
                var text = "Time Sheet" + timeSheetCount;
                var detail = new TimeSheet();

                detail.Engineer = timeSheet.Engineer;
                detail.Mate = timeSheet.Mate;
                detail.Date = timeSheet.Date;
                detail.StartTime = timeSheet.StartTime;
                detail.TimeArrive = timeSheet.TimeArrive;
                detail.TimeDepart = timeSheet.TimeDepart;
                detail.HoursOnSite = timeSheet.HoursOnSite;
                detail.HoursTravel = timeSheet.HoursTravel;
                detail.TotalHours = timeSheet.TotalHours;
                detail.NormalTime = timeSheet.NormalTime;
                detail.NormalTimeAtRateOf = timeSheet.NormalTimeAtRateOf;
                detail.Overtime = timeSheet.Overtime;
                detail.OvertimeAtRateOf = timeSheet.OvertimeAtRateOf;
                detail.LabourChargeEngineer = timeSheet.LabourChargeEngineer;
                detail.LabourChargeMate = timeSheet.LabourChargeMate;
                timeSheetCount++;

                AddMoreTimeSection.Children.Add(new TimeSheetAccordionView(detail));
            }
        }
    }      
}