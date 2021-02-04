using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices
{
    public class SectionRisk
    {
        public string Check1 { get; set; }
        public bool Check1Value { get; set; }
        public string Check2 { get; set; }
        public bool Check2Value { get; set; }
        public string Check3 { get; set; }
        public bool Check3Value { get; set; }
        public string Check4 { get; set; }
        public bool Check4Value { get; set; }
        public string Check5 { get; set; }
        public bool Check5Value { get; set; }
        public string Check6 { get; set; }
        public bool Check6Value { get; set; }
        public string Check7 { get; set; }
        public bool Check7Value { get; set; }
        public string Check8 { get; set; }
        public bool Check8Value { get; set; }
        public string Check9 { get; set; }
        public bool Check9Value { get; set; }
        public string Check10 { get; set; }
        public bool Check10Value { get; set; }
        public string Check11 { get; set; }
        public bool Check11Value { get; set; }
        public string Check12 { get; set; }
        public bool Check12Value { get; set; }
        public string Check13 { get; set; }
        public bool Check13Value { get; set; }
        public string Check14 { get; set; }
        public bool Check14Value { get; set; }
        public string Check15 { get; set; }
        public bool Check15Value { get; set; }
        public string Check16 { get; set; }
        public bool Check16Value { get; set; }
        public string Check17 { get; set; }
        public bool Check17Value { get; set; }
        public string Check18 { get; set; }
        public bool Check18Value { get; set; }
        public string Check19 { get; set; }
        public bool Check19Value { get; set; }
        public string Check20 { get; set; }
        public bool Check20Value { get; set; }
        public string Check21 { get; set; }
        public bool Check21Value { get; set; }
        public string Check22 { get; set; }
        public bool Check22Value { get; set; }
        public string Check23 { get; set; }
        public bool Check23Value { get; set; }
        public string Check24 { get; set; }
        public bool Check24Value { get; set; }

        public string Check25 { get; set; }
        public bool Check25Value { get; set; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RiskAssessmentAccordionView : ContentView
    {
        #region Bindable Properties
        public static BindableProperty HeaderBackgroundColorProperty =
            BindableProperty.Create(nameof(HeaderBackgroundColor),
                typeof(Color),
                typeof(RiskAssessmentAccordionView),
                defaultValue: Color.Transparent,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((RiskAssessmentAccordionView)bindable).UpdateHeaderBackgroundColor();
                });

        public static BindableProperty HeaderOpenedBackgroundColorProperty =
            BindableProperty.Create(nameof(HeaderOpenedBackgroundColor),
                typeof(Color),
                typeof(RiskAssessmentAccordionView),
                defaultValue: Color.Transparent,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((RiskAssessmentAccordionView)bindable).UpdateHeaderBackgroundColor();
                });

        public static BindableProperty HeaderTextColorProperty =
            BindableProperty.Create(nameof(HeaderTextColor),
                typeof(Color),
                typeof(RiskAssessmentAccordionView),
                defaultValue: Color.Black,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((RiskAssessmentAccordionView)bindable).UpdateHeaderTextColor((Color)newVal);
                });

        public static BindableProperty HeaderTextProperty =
            BindableProperty.Create(nameof(HeaderTextProperty),
                typeof(string),
                typeof(RiskAssessmentAccordionView),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((RiskAssessmentAccordionView)bindable).UpdateHeaderText((string)newVal);
                });

        public static BindableProperty BodyTextColorProperty =
            BindableProperty.Create(nameof(BodyTextColor),
                typeof(Color),
                typeof(RiskAssessmentAccordionView),
                defaultValue: Color.Black,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((RiskAssessmentAccordionView)bindable).UpdateBodyTextColor((Color)newVal);
                });

        public static BindableProperty BodyTextProperty =
            BindableProperty.Create(nameof(BodyText),
                typeof(string),
                typeof(RiskAssessmentAccordionView),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((RiskAssessmentAccordionView)bindable).UpdateBodyText((string)newVal);
                });


        public static BindableProperty IsBodyVisibleProperty =
            BindableProperty.Create(nameof(IsBodyVisible),
                typeof(bool),
                typeof(RiskAssessmentAccordionView),
                defaultValue: true,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((RiskAssessmentAccordionView)bindable).UpdateBodyVisibility((bool)newVal);
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


        public RiskAssessmentAccordionView()
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

        public SectionRisk GetSectionRisk()
        {
            SectionRisk risk = new SectionRisk
            {
                Check1 = Check1.Text,
                Check1Value = Check1Checkbox.IsChecked,

                Check2 = Check2.Text,
                Check2Value = Check2Checkbox.IsChecked,

                Check3 = Check3.Text,
                Check3Value = Check3Checkbox.IsChecked,

                Check4 = Check4.Text,
                Check4Value = Check4Checkbox.IsChecked,

                Check5 = Check5.Text,
                Check5Value = Check5Checkbox.IsChecked,

                Check6 = Check6.Text,
                Check6Value = Check6Checkbox.IsChecked,

                Check7 = Check7.Text,
                Check7Value = Check7Checkbox.IsChecked,

                Check8 = Check8.Text,
                Check8Value = Check8Checkbox.IsChecked,

                Check9 = Check9.Text,
                Check9Value = Check9Checkbox.IsChecked,

                Check10 = Check10.Text,
                Check10Value = Check10Checkbox.IsChecked,

                Check11 = Check11.Text,
                Check11Value = Check11Checkbox.IsChecked,

                Check12 = Check12.Text,
                Check12Value = Check12Checkbox.IsChecked,

                Check13 = Check13.Text,
                Check13Value = Check13Checkbox.IsChecked,

                Check14 = Check14.Text,
                Check14Value = Check14Checkbox.IsChecked,

                Check15 = Check15.Text,
                Check15Value = Check15Checkbox.IsChecked,

                Check16 = Check16.Text,
                Check16Value = Check16Checkbox.IsChecked,

                Check17 = Check17.Text,
                Check17Value = Check17Checkbox.IsChecked,

                Check18 = Check18.Text,
                Check18Value = Check18Checkbox.IsChecked,

                Check19 = Check19.Text,
                Check19Value = Check19Checkbox.IsChecked,

                Check20 = Check20.Text,
                Check20Value = Check20Checkbox.IsChecked,

                Check21 = Check21.Text,
                Check21Value = Check21Checkbox.IsChecked,

                Check22 = Check22.Text,
                Check22Value = Check22Checkbox.IsChecked,

                Check23 = Check23.Text,
                Check23Value = Check23Checkbox.IsChecked,

                Check24 = Check24.Text,
                Check24Value = Check24Checkbox.IsChecked,

                Check25 = Check25.Text,
                Check25Value = Check25Checkbox.IsChecked,
            };

            return risk;
        }

        public void SetSectionRisk(SectionRisk risk)
        {
            Check1.Text = risk.Check1;
            Check1Checkbox.IsChecked = risk.Check1Value;

            Check2.Text = risk.Check2;
            Check2Checkbox.IsChecked = risk.Check2Value;

            Check3.Text = risk.Check3;
            Check3Checkbox.IsChecked = risk.Check3Value;

            Check4.Text = risk.Check4;
            Check4Checkbox.IsChecked = risk.Check4Value;

            Check5.Text = risk.Check5;
            Check5Checkbox.IsChecked = risk.Check5Value;

            Check6.Text = risk.Check6;
            Check6Checkbox.IsChecked = risk.Check6Value;

            Check7.Text = risk.Check7;
            Check7Checkbox.IsChecked = risk.Check7Value;

            Check8.Text = risk.Check8;
            Check8Checkbox.IsChecked = risk.Check8Value;

            Check9.Text = risk.Check9;
            Check9Checkbox.IsChecked = risk.Check9Value;

            Check10.Text = risk.Check10;
            Check10Checkbox.IsChecked = risk.Check10Value;

            Check11.Text = risk.Check11;
            Check11Checkbox.IsChecked = risk.Check11Value;

            Check12.Text = risk.Check12;
            Check12Checkbox.IsChecked = risk.Check12Value;

            Check13.Text = risk.Check13;
            Check13Checkbox.IsChecked = risk.Check13Value;

            Check14.Text = risk.Check14;
            Check14Checkbox.IsChecked = risk.Check14Value;

            Check15.Text = risk.Check15;
            Check15Checkbox.IsChecked = risk.Check15Value;

            Check16.Text = risk.Check16;
            Check16Checkbox.IsChecked = risk.Check16Value;

            Check17.Text = risk.Check17;
            Check17Checkbox.IsChecked = risk.Check17Value;

            Check18.Text = risk.Check18;
            Check18Checkbox.IsChecked = risk.Check18Value;

            Check19.Text = risk.Check19;
            Check19Checkbox.IsChecked = risk.Check19Value;

            Check20.Text = risk.Check20;
            Check20Checkbox.IsChecked = risk.Check20Value;

            Check21.Text = risk.Check21;
            Check21Checkbox.IsChecked = risk.Check21Value;

            Check22.Text = risk.Check22;
            Check22Checkbox.IsChecked = risk.Check22Value;

            Check23.Text = risk.Check23;
            Check23Checkbox.IsChecked = risk.Check23Value;

            Check24.Text = risk.Check24;
            Check24Checkbox.IsChecked = risk.Check24Value;

            Check25.Text = risk.Check25;
            Check25Checkbox.IsChecked = risk.Check25Value;
        }
    }
}
