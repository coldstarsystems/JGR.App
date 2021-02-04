using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices
{
    public class HazardList
    {
        public List<HazardAccordionView> Hazards { get; set; }

        public HazardList()
        {
            Hazards = new List<HazardAccordionView>();
        }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdditionalHazardsAccordionView : ContentView
    {
        #region Bindable Properties
        public static BindableProperty HeaderBackgroundColorProperty =
            BindableProperty.Create(nameof(HeaderBackgroundColor),
                typeof(Color),
                typeof(AdditionalHazardsAccordionView),
                defaultValue: Color.Transparent,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((AdditionalHazardsAccordionView)bindable).UpdateHeaderBackgroundColor();
                });

        public static BindableProperty HeaderOpenedBackgroundColorProperty =
            BindableProperty.Create(nameof(HeaderOpenedBackgroundColor),
                typeof(Color),
                typeof(AdditionalHazardsAccordionView),
                defaultValue: Color.Transparent,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((AdditionalHazardsAccordionView)bindable).UpdateHeaderBackgroundColor();
                });

        public static BindableProperty HeaderTextColorProperty =
            BindableProperty.Create(nameof(HeaderTextColor),
                typeof(Color),
                typeof(AdditionalHazardsAccordionView),
                defaultValue: Color.Black,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((AdditionalHazardsAccordionView)bindable).UpdateHeaderTextColor((Color)newVal);
                });

        public static BindableProperty HeaderTextProperty =
            BindableProperty.Create(nameof(HeaderTextProperty),
                typeof(string),
                typeof(AdditionalHazardsAccordionView),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((AdditionalHazardsAccordionView)bindable).UpdateHeaderText((string)newVal);
                });

        public static BindableProperty BodyTextColorProperty =
            BindableProperty.Create(nameof(BodyTextColor),
                typeof(Color),
                typeof(AdditionalHazardsAccordionView),
                defaultValue: Color.Black,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((AdditionalHazardsAccordionView)bindable).UpdateBodyTextColor((Color)newVal);
                });

        public static BindableProperty BodyTextProperty =
            BindableProperty.Create(nameof(BodyText),
                typeof(string),
                typeof(AdditionalHazardsAccordionView),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((AdditionalHazardsAccordionView)bindable).UpdateBodyText((string)newVal);
                });


        public static BindableProperty IsBodyVisibleProperty =
            BindableProperty.Create(nameof(IsBodyVisible),
                typeof(bool),
                typeof(AdditionalHazardsAccordionView),
                defaultValue: true,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((AdditionalHazardsAccordionView)bindable).UpdateBodyVisibility((bool)newVal);
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


        public AdditionalHazardsAccordionView()
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

        private void AddMoreHazards_Handle_Tapped(object sender, System.EventArgs e)
        {
            AddMoreHazards.Children.Add(new HazardView()
            {

            });
        }

        public HazardList GetHazardList()
        {
            HazardList list = new HazardList();

            for (int i = 0; i < AddMoreHazards.Children.Count; i++)
            {
                HazardView view = (HazardView)AddMoreHazards.Children.ElementAt(i);
                list.Hazards.Add(view.GetHazard());
            }

            return list;
        }

        public void SetHazardList(HazardList list)
        {
            AddMoreHazards.Children.Clear();

            foreach (HazardAccordionView hazard in list.Hazards)
            {
                AddMoreHazards.Children.Add(new HazardView(hazard));
            }
        }
    }
}
