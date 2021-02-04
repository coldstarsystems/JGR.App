using JGRBuildingServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices
{
    public class SectionFGas
    {
        public DateTime? LTDate { get; set; }
        public TimeSpan? LTStartTime { get; set; }
        public TimeSpan? LTFinishTime { get; set; }
        public string LTSystemNumber { get; set; }
        public bool? LTLeakFoundYes { get; set; }
        public bool? LTLeakFoundNo { get; set; }
        public string LTLocationOfLeak { get; set; }
        public string LTDetailsOfRepair { get; set; }
        public string LTActionTaken { get; set; }

        public DateTime? RLTDate { get; set; }
        public TimeSpan? RLTStartTime { get; set; }
        public TimeSpan? RLTFinishTime { get; set; }
        public string RLTSystemNumber { get; set; }
        public bool? RLTLeakFoundYes { get; set; }
        public bool? RLTLeakFoundNo { get; set; }
        public string RLTDetails { get; set; }

        public string TempOnArrival { get; set; }
        public string TempOnDeparture { get; set; }
        public string PlantLeftOperational { get; set; }
        public string JobComplete { get; set; }

        public List<PartsAccordionView> PartsList { get; set; }

        public List<TimeSheet> TimeSheetList { get; set; }

        public SectionFGas()
        {
            PartsList = new List<PartsAccordionView>();
            TimeSheetList = new List<TimeSheet>();
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FGasAccordionView : ContentView
    {
        #region Bindable Properties
        public static BindableProperty HeaderBackgroundColorProperty =
            BindableProperty.Create(nameof(HeaderBackgroundColor),
                typeof(Color),
                typeof(FGasAccordionView),
                defaultValue: Color.Transparent,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((FGasAccordionView)bindable).UpdateHeaderBackgroundColor();
                });

        public static BindableProperty HeaderOpenedBackgroundColorProperty =
            BindableProperty.Create(nameof(HeaderOpenedBackgroundColor),
                typeof(Color),
                typeof(FGasAccordionView),
                defaultValue: Color.Transparent,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((FGasAccordionView)bindable).UpdateHeaderBackgroundColor();
                });

        public static BindableProperty HeaderTextColorProperty =
            BindableProperty.Create(nameof(HeaderTextColor),
                typeof(Color),
                typeof(FGasAccordionView),
                defaultValue: Color.Black,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((FGasAccordionView)bindable).UpdateHeaderTextColor((Color)newVal);
                });

        public static BindableProperty HeaderTextProperty =
            BindableProperty.Create(nameof(HeaderTextProperty),
                typeof(string),
                typeof(FGasAccordionView),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((FGasAccordionView)bindable).UpdateHeaderText((string)newVal);
                });

        public static BindableProperty BodyTextColorProperty =
            BindableProperty.Create(nameof(BodyTextColor),
                typeof(Color),
                typeof(FGasAccordionView),
                defaultValue: Color.Black,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((FGasAccordionView)bindable).UpdateBodyTextColor((Color)newVal);
                });

        public static BindableProperty BodyTextProperty =
            BindableProperty.Create(nameof(BodyText),
                typeof(string),
                typeof(FGasAccordionView),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((FGasAccordionView)bindable).UpdateBodyText((string)newVal);
                });


        public static BindableProperty IsBodyVisibleProperty =
            BindableProperty.Create(nameof(IsBodyVisible),
                typeof(bool),
                typeof(FGasAccordionView),
                defaultValue: true,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((FGasAccordionView)bindable).UpdateBodyVisibility((bool)newVal);
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

        public FGasAccordionView()
        {
            InitializeComponent();
            IsBodyVisible = false;
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

        public SectionFGas GetSectionFGas()
        {
            var fgas = new SectionFGas
            {
                LTDate = LeakTestDate.NullableDate,
                LTStartTime = LeakTestStartTime.NullableTime,
                LTFinishTime = LeakTestFinishTime.NullableTime,
                LTSystemNumber = (LeakTestSystemNo.Text == null ? "" : LeakTestSystemNo.Text),
                LTLeakFoundYes = LeakTestLeakFoundYes.IsChecked,
                LTLeakFoundNo = LeakTestLeakFoundNo.IsChecked,
                LTLocationOfLeak = (LeakTestLocationOfLeak.Text == null ? "" : LeakTestLocationOfLeak.Text),
                LTDetailsOfRepair = (LeakTestDetailsOfRepair.Text == null ? "" : LeakTestDetailsOfRepair.Text),
                LTActionTaken = (LeakTestActionsToPreventLeak.Text == null ? "" : LeakTestActionsToPreventLeak.Text),

                RLTDate = ReturnLeakDate.NullableDate,
                RLTStartTime = ReturnLeakStartTime.NullableTime,
                RLTFinishTime = ReturnLeakFinishTime.NullableTime,
                RLTSystemNumber = (ReturnLeakSystemNo.Text == null ? "" : ReturnLeakSystemNo.Text),
                RLTLeakFoundYes = ReturnLeakLeakFoundYes.IsChecked,
                RLTLeakFoundNo = ReturnLeakLeakFoundNo.IsChecked,
                RLTDetails = (ReturnLeakDetails.Text == null ? "" : ReturnLeakDetails.Text),
            };

            return fgas;
        }

        public void SetSectionFGas(SectionFGas fgas)
        {
            LeakTestDate.NullableDate = fgas.LTDate;
            LeakTestStartTime.NullableTime = fgas.LTStartTime;
            LeakTestFinishTime.NullableTime = fgas.LTFinishTime;
            LeakTestSystemNo.Text = fgas.LTSystemNumber;
            LeakTestLeakFoundYes.IsChecked = fgas.LTLeakFoundYes ?? false;
            LeakTestLeakFoundNo.IsChecked = fgas.LTLeakFoundNo ?? false;
            LeakTestLocationOfLeak.Text = fgas.LTLocationOfLeak;
            LeakTestDetailsOfRepair.Text = fgas.LTDetailsOfRepair;
            LeakTestActionsToPreventLeak.Text = fgas.LTActionTaken;

            ReturnLeakDate.NullableDate = fgas.RLTDate;
            ReturnLeakStartTime.NullableTime = fgas.RLTStartTime;
            ReturnLeakFinishTime.NullableTime = fgas.RLTFinishTime;
            ReturnLeakSystemNo.Text = fgas.RLTSystemNumber;
            ReturnLeakLeakFoundYes.IsChecked = fgas.RLTLeakFoundYes ?? false;
            ReturnLeakLeakFoundNo.IsChecked = fgas.RLTLeakFoundNo ?? false;
            ReturnLeakDetails.Text = fgas.RLTDetails;
        }
    }
}