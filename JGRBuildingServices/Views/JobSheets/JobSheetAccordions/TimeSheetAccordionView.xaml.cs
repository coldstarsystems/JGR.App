using JGRBuildingServices.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices
{
    public class TimeAccordion
    {
        public List<TimeSheet> Time { get; set; }

        public TimeAccordion()
        {
            Time = new List<TimeSheet>();
        }
    }

    public class TimeSheet
    {
        public string Engineer { get; set; }
        public string Mate { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? TimeArrive { get; set; }
        public TimeSpan? TimeDepart { get; set; }
        public string HoursOnSite { get; set; }
        public string HoursTravel { get; set; }
        public string TotalHours { get; set; }
        public string NormalTime { get; set; }
        public string NormalTimeAtRateOf { get; set; }
        public string Overtime { get; set; }
        public string OvertimeAtRateOf { get; set; }
        public string LabourChargeEngineer { get; set; }
        public string LabourChargeMate { get; set; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimeSheetAccordionView : ContentView
    {
        #region Bindable Properties
        public static BindableProperty HeaderBackgroundColorProperty =
            BindableProperty.Create(nameof(HeaderBackgroundColor),
                typeof(Color),
                typeof(TimeSheetAccordionView),
                defaultValue: Color.Transparent,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((TimeSheetAccordionView)bindable).UpdateHeaderBackgroundColor();
                });

        public static BindableProperty HeaderOpenedBackgroundColorProperty =
            BindableProperty.Create(nameof(HeaderOpenedBackgroundColor),
                typeof(Color),
                typeof(TimeSheetAccordionView),
                defaultValue: Color.Transparent,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((TimeSheetAccordionView)bindable).UpdateHeaderBackgroundColor();
                });

        public static BindableProperty HeaderTextColorProperty =
            BindableProperty.Create(nameof(HeaderTextColor),
                typeof(Color),
                typeof(TimeSheetAccordionView),
                defaultValue: Color.Black,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((TimeSheetAccordionView)bindable).UpdateHeaderTextColor((Color)newVal);
                });

        public static BindableProperty HeaderTextProperty =
            BindableProperty.Create(nameof(HeaderTextProperty),
                typeof(string),
                typeof(TimeSheetAccordionView),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((TimeSheetAccordionView)bindable).UpdateHeaderText((string)newVal);
                });

        public static BindableProperty BodyTextColorProperty =
            BindableProperty.Create(nameof(BodyTextColor),
                typeof(Color),
                typeof(TimeSheetAccordionView),
                defaultValue: Color.Black,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((TimeSheetAccordionView)bindable).UpdateBodyTextColor((Color)newVal);
                });

        public static BindableProperty BodyTextProperty =
            BindableProperty.Create(nameof(BodyText),
                typeof(string),
                typeof(TimeSheetAccordionView),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((TimeSheetAccordionView)bindable).UpdateBodyText((string)newVal);
                });


        public static BindableProperty IsBodyVisibleProperty =
            BindableProperty.Create(nameof(IsBodyVisible),
                typeof(bool),
                typeof(TimeSheetAccordionView),
                defaultValue: true,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((TimeSheetAccordionView)bindable).UpdateBodyVisibility((bool)newVal);
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


        public TimeSheetAccordionView()
        {
            InitializeComponent();
            IsBodyVisible = false;
        }

        public TimeSheetAccordionView(TimeSheet timeSheet)
        {
            InitializeComponent();
            IsBodyVisible = false;

            HeaderText = "View Job Sheet";

            Engineer.Text = timeSheet.Engineer;
            Mate.Text = timeSheet.Mate;
            Date.NullableDate = timeSheet.Date;

            if (Date.NullableDate == null)
            {
                Date.NullableDate = DateTime.Today;
                Date.PlaceHolder = DateTime.Now.ToString();
            }

            StartTime.NullableTime = timeSheet.StartTime;
            TimeArrive.NullableTime = timeSheet.TimeArrive;
            TimeDepart.NullableTime = timeSheet.TimeDepart;
            HoursOnSite.Text = timeSheet.HoursOnSite;
            HoursTravel.Text = timeSheet.HoursTravel;
            TotalHoursEntry.Text = timeSheet.TotalHours;
            NormalTime.Text = timeSheet.NormalTime;
            NormalTimeAtRateOf.Text = timeSheet.NormalTimeAtRateOf;
            Overtime.Text = timeSheet.Overtime;
            OvertimeAtRateOf.Text = timeSheet.OvertimeAtRateOf;
            LabourCharges.Text = timeSheet.LabourChargeEngineer;
        }

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

        public TimeSheet GetTimeSheet()
        {
            int Timecount = 1;
            HeaderText = $"Time Sheet {Timecount.ToString()}";

            if (Date.NullableDate == null)
            {
                Date.NullableDate = DateTime.Today;
                Date.PlaceHolder = DateTime.Today.ToString();
            }

            return new TimeSheet
            {
                Engineer = Engineer.Text == null ? "" : Engineer.Text,
                Mate = Mate.Text == null ? "" : Mate.Text,
                Date = Date.NullableDate,
                StartTime = StartTime.NullableTime,
                TimeArrive = TimeArrive.NullableTime,
                TimeDepart = TimeDepart.NullableTime,
                HoursOnSite = HoursOnSite.Text == null ? "" : HoursOnSite.Text,
                HoursTravel = HoursTravel.Text == null ? "" : HoursTravel.Text,
                TotalHours = TotalHoursEntry.Text == null ? "" : TotalHoursEntry.Text,
                NormalTime = NormalTime.Text == null ? "" : NormalTime.Text,
                NormalTimeAtRateOf = NormalTimeAtRateOf.Text == null ? "" : NormalTimeAtRateOf.Text,
                Overtime = Overtime.Text == null ? "" : Overtime.Text,
                OvertimeAtRateOf = OvertimeAtRateOf.Text == null ? "" : OvertimeAtRateOf.Text,
                LabourChargeEngineer = LabourCharges.Text == null ? "": LabourCharges.Text,
            };
        }

        private void AddTotalHours()
        {
            if (!String.IsNullOrEmpty(TimeArrive.NullableTime.ToString()) && !String.IsNullOrEmpty(TimeDepart.NullableTime.ToString()))
            {
                TimeSpan StartandArrive = TimeArrive.Time - StartTime.Time;
                Double Startandarrive = StartandArrive.TotalHours;
                double StartMath = Math.Truncate(Startandarrive * 100) / 100;

                TimeSpan ArriveandDepart = TimeDepart.Time - TimeArrive.Time;
                Double Arriveanddepart = ArriveandDepart.TotalHours;

                double arriveMath = Math.Truncate(Arriveanddepart * 100) / 100;

                HoursOnSite.Text = arriveMath.ToString();

                if (HoursTravel.Text.Length > 0)
                {
                    char point = HoursTravel.Text[0];
                    string pointstring = point.ToString();
                    if (pointstring == ".")
                    {
                        var convert = HoursTravel.Text = "0" + HoursTravel.Text;
                        var totaldouble = Convert.ToDouble(convert);
                        var hoursTotal =  Arriveanddepart + totaldouble;
                        double hoursMath = Math.Truncate(hoursTotal * 100) / 100;

                        TotalHoursEntry.Text = hoursMath.ToString();
                    }
                    else 
                    {
                        var convert = HoursTravel.Text;
                        var totaldouble = Convert.ToDouble(convert);
                        var hoursTotal = Arriveanddepart + totaldouble;
                        double hoursMath = Math.Truncate(hoursTotal * 100) / 100;

                        TotalHoursEntry.Text = hoursMath.ToString();
                    }
                }
                else
                {
                    var hoursTotal = Arriveanddepart;
                    double hoursMath = Math.Truncate(hoursTotal * 100) / 100;

                    TotalHoursEntry.Text = hoursMath.ToString();
                }
            }
            else 
            {

            }
        }

        private void TimeDepart_PropertyChanged(object sender, EventArgs e)
        {
            AddTotalHours();
        }

        private void TimeArrive_PropertyChanged(object sender, EventArgs e)
        {
            AddTotalHours();
        }

        private void StartTime_PropertyChanged(object sender, EventArgs e)
        {
            AddTotalHours();
        }

        private void HoursTravel_TextChanged(object sender, EventArgs e)
        {
            AddTotalHours();
        }

        private void HoursOnSite_TextChanged(object sender, EventArgs e)
        {
            AddTotalHours();
        }
    }
}
