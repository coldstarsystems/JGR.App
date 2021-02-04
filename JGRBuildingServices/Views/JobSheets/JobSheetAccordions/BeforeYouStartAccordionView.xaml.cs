using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices
{
    public class SectionBeforeYouStart
    {
        public String Check1 { get; set; }

        public String Check1Value { get; set; }

        public String Check1Comments { get; set; }

        public String Check2 { get; set; }

        public String Check2Value { get; set; }

        public String Check2Comments { get; set; }

        public String Check3 { get; set; }

        public String Check3Value { get; set; }

        public String Check3Comments { get; set; }

        public String Check4 { get; set; }

        public String Check4Value { get; set; }

        public String Check4Comments { get; set; }

        public String Check5 { get; set; }

        public String Check5Value { get; set; }

        public String Check5Comments { get; set; }

        public String Check6 { get; set; }

        public String Check6Value { get; set; }

        public String Check6Comments { get; set; }

        public String Check7 { get; set; }

        public String Check7Value { get; set; }

        public String Check7Comments { get; set; }

        public String Check8 { get; set; }

        public String Check8Value { get; set; }

        public String Check8Comments { get; set; }

        public String Check9 { get; set; }

        public String Check9Value { get; set; }

        public String Check9Comments { get; set; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BeforeYouStartAccordionView : ContentView
    {
        #region Bindable Properties
        public static BindableProperty HeaderBackgroundColorProperty =
            BindableProperty.Create(nameof(HeaderBackgroundColor),
                typeof(Color),
                typeof(BeforeYouStartAccordionView),
                defaultValue: Color.Transparent,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((BeforeYouStartAccordionView)bindable).UpdateHeaderBackgroundColor();
                });

        public static BindableProperty HeaderOpenedBackgroundColorProperty =
            BindableProperty.Create(nameof(HeaderOpenedBackgroundColor),
                typeof(Color),
                typeof(BeforeYouStartAccordionView),
                defaultValue: Color.Transparent,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((BeforeYouStartAccordionView)bindable).UpdateHeaderBackgroundColor();
                });

        public static BindableProperty HeaderTextColorProperty =
            BindableProperty.Create(nameof(HeaderTextColor),
                typeof(Color),
                typeof(BeforeYouStartAccordionView),
                defaultValue: Color.Black,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((BeforeYouStartAccordionView)bindable).UpdateHeaderTextColor((Color)newVal);
                });

        public static BindableProperty HeaderTextProperty =
            BindableProperty.Create(nameof(HeaderTextProperty),
                typeof(string),
                typeof(BeforeYouStartAccordionView),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((BeforeYouStartAccordionView)bindable).UpdateHeaderText((string)newVal);
                });

        public static BindableProperty BodyTextColorProperty =
            BindableProperty.Create(nameof(BodyTextColor),
                typeof(Color),
                typeof(BeforeYouStartAccordionView),
                defaultValue: Color.Black,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((BeforeYouStartAccordionView)bindable).UpdateBodyTextColor((Color)newVal);
                });

        public static BindableProperty BodyTextProperty =
            BindableProperty.Create(nameof(BodyText),
                typeof(string),
                typeof(BeforeYouStartAccordionView),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((BeforeYouStartAccordionView)bindable).UpdateBodyText((string)newVal);
                });


        public static BindableProperty IsBodyVisibleProperty =
            BindableProperty.Create(nameof(IsBodyVisible),
                typeof(bool),
                typeof(BeforeYouStartAccordionView),
                defaultValue: true,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((BeforeYouStartAccordionView)bindable).UpdateBodyVisibility((bool)newVal);
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

        public BeforeYouStartAccordionView()
        {
            InitializeComponent();

            IsBodyVisible = false;
        }

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

        public void UpdateHeaderTextColor(Color color)
        {
            HeaderLabel.TextColor = color;
        }

        public void UpdateBodyTextColor(Color color)
        {

        }

        public void UpdateHeaderText(string text)
        {
            HeaderLabel.Text = text;
        }

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

        private void Handle_Tapped(object sender, EventArgs e)
        {
            IsBodyVisible = !IsBodyVisible;

            UpdateHeaderBackgroundColor();
        }

        public SectionBeforeYouStart GetSectionBefore()
        {
            var before = new SectionBeforeYouStart();

            #region Check 1

            before.Check1 = "Have you worked on this site before?";
            before.Check1Comments = Check1Comments.Text == null ? "" : Check1Comments.Text;

            switch (Check1Value.SelectedIndex)
            {
                case 0:
                    before.Check1Value = "Yes";
                    break;
                case 1:
                    before.Check1Value = "No";
                    break;
                case 2:
                    before.Check1Value = "Not Applicable";
                    break;
            }

            #endregion

            #region Check 2

            before.Check2 = "Have all checks and procedures been completed, as detailed in the Service Engineers Method Statement?";
            before.Check2Comments = Check2Comments.Text == null ? "" : Check2Comments.Text;

            switch (Check2Value.SelectedIndex)
            {
                case 0:
                    before.Check2Value = "Yes";
                    break;
                case 1:
                    before.Check2Value = "No";
                    break;
                case 2:
                    before.Check2Value = "Not Applicable";
                    break;
            }

            #endregion

            #region Check 3

            before.Check3 = "Have you done this type of job before?";
            before.Check3Comments = Check3Comments.Text == null ? "" : Check3Comments.Text;

            switch (Check3Value.SelectedIndex)
            {
                case 0:
                    before.Check3Value = "Yes";
                    break;
                case 1:
                    before.Check3Value = "No";
                    break;
                case 2:
                    before.Check3Value = "Not Applicable";
                    break;
            }

            #endregion

            #region Check 4

            before.Check4 = "Do you have the right tools for the job?";
            before.Check4Comments = Check4Comments.Text == null ? "" : Check4Comments.Text;

            switch (Check4Value.SelectedIndex)
            {
                case 0:
                    before.Check4Value = "Yes";
                    break;
                case 1:
                    before.Check4Value = "No";
                    break;
                case 2:
                    before.Check4Value = "Not Applicable";
                    break;
            }

            #endregion

            #region Check 5

            before.Check5 = "Where required, tools need calibrated and in date?";
            before.Check5Comments = Check5Comments.Text == null ? "" : Check5Comments.Text;

            switch (Check5Value.SelectedIndex)
            {
                case 0:
                    before.Check5Value = "Yes";
                    break;
                case 1:
                    before.Check5Value = "No";
                    break;
                case 2:
                    before.Check5Value = "Not Applicable";
                    break;
            }

            #endregion

            #region Check 6

            before.Check6 = "Do you have the right documentation for the job?";
            before.Check6Comments = Check6Comments.Text == null ? "" : Check6Comments.Text;

            switch (Check6Value.SelectedIndex)
            {
                case 0:
                    before.Check6Value = "Yes";
                    break;
                case 1:
                    before.Check6Value = "No";
                    break;
                case 2:
                    before.Check6Value = "Not Applicable";
                    break;
            }

            #endregion

            #region Check 7

            before.Check7 = "Do you have the right PPE for the job?";
            before.Check7Comments = Check7Comments.Text == null ? "" : Check7Comments.Text;

            switch (Check7Value.SelectedIndex)
            {
                case 0:
                    before.Check7Value = "Yes";
                    break;
                case 1:
                    before.Check7Value = "No";
                    break;
                case 2:
                    before.Check7Value = "Not Applicable";
                    break;
            }

            #endregion

            #region Check 8

            before.Check8 = "Are power tools and leads PAT tested?";
            before.Check8Comments = Check8Comments.Text == null ? "" : Check8Comments.Text;

            switch (Check8Value.SelectedIndex)
            {
                case 0:
                    before.Check8Value = "Yes";
                    break;
                case 1:
                    before.Check8Value = "No";
                    break;
                case 2:
                    before.Check8Value = "Not Applicable";
                    break;
            }

            #endregion

            #region Check 9

            before.Check9 = "Are scaffolds and ladders inspected?";
            before.Check9Comments = Check9Comments.Text == null ? "" : Check9Comments.Text;

            switch (Check9Value.SelectedIndex)
            {
                case 0:
                    before.Check9Value = "Yes";
                    break;
                case 1:
                    before.Check9Value = "No";
                    break;
                case 2:
                    before.Check9Value = "Not Applicable";
                    break;
            }

            #endregion
  
            return before;
        }

        public void SetSectionBefore(SectionBeforeYouStart before)
        {
            Check1.Text = before.Check1;

            switch (before.Check1Value)
            {
                case "Yes":
                    Check1Value.SelectedIndex = 0;
                    break;
                case "No":
                    Check1Value.SelectedIndex = 1;
                    break;
                case "Not Applicable":
                    Check1Value.SelectedIndex = 2;
                    break;
            }

            Check1Comments.Text = before.Check1Comments;

            Check2.Text = before.Check2;

            switch (before.Check2Value)
            {
                case "Yes":
                    Check2Value.SelectedIndex = 0;
                    break;
                case "No":
                    Check2Value.SelectedIndex = 1;
                    break;
                case "Not Applicable":
                    Check2Value.SelectedIndex = 2;
                    break;
            }

            Check2Comments.Text = before.Check2Comments;

            Check3.Text = before.Check3;

            switch (before.Check3Value)
            {
                case "Yes":
                    Check3Value.SelectedIndex = 0;
                    break;
                case "No":
                    Check3Value.SelectedIndex = 1;
                    break;
                case "Not Applicable":
                    Check3Value.SelectedIndex = 2;
                    break;
            }

            Check3Comments.Text = before.Check3Comments;

            Check4.Text = before.Check4;

            switch (before.Check4Value)
            {
                case "Yes":
                    Check4Value.SelectedIndex = 0;
                    break;
                case "No":
                    Check4Value.SelectedIndex = 1;
                    break;
                case "Not Applicable":
                    Check4Value.SelectedIndex = 2;
                    break;
            }

            Check4Comments.Text = before.Check4Comments;

            Check5.Text = before.Check5;

            switch (before.Check5Value)
            {
                case "Yes":
                    Check5Value.SelectedIndex = 0;
                    break;
                case "No":
                    Check5Value.SelectedIndex = 1;
                    break;
                case "Not Applicable":
                    Check5Value.SelectedIndex = 2;
                    break;
            }

            Check5Comments.Text = before.Check5Comments;

            Check6.Text = before.Check6;

            switch (before.Check6Value)
            {
                case "Yes":
                    Check6Value.SelectedIndex = 0;
                    break;
                case "No":
                    Check6Value.SelectedIndex = 1;
                    break;
                case "Not Applicable":
                    Check6Value.SelectedIndex = 2;
                    break;
            }

            Check6Comments.Text = before.Check6Comments;

            Check7.Text = before.Check7;

            switch (before.Check7Value)
            {
                case "Yes":
                    Check7Value.SelectedIndex = 0;
                    break;
                case "No":
                    Check7Value.SelectedIndex = 1;
                    break;
                case "Not Applicable":
                    Check7Value.SelectedIndex = 2;
                    break;
            }

            Check7Comments.Text = before.Check7Comments;

            Check8.Text = before.Check8;

            switch (before.Check8Value)
            {
                case "Yes":
                    Check8Value.SelectedIndex = 0;
                    break;
                case "No":
                    Check8Value.SelectedIndex = 1;
                    break;
                case "Not Applicable":
                    Check8Value.SelectedIndex = 2;
                    break;
            }

            Check8Comments.Text = before.Check8Comments;

            Check9.Text = before.Check9;

            switch (before.Check9Value)
            {
                case "Yes":
                    Check9Value.SelectedIndex = 0;
                    break;
                case "No":
                    Check9Value.SelectedIndex = 1;
                    break;
                case "Not Applicable":
                    Check9Value.SelectedIndex = 2;
                    break;
            }

            Check9Comments.Text = before.Check9Comments;

        }
    }
}
