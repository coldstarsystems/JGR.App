﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
//using PCLStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JGRBuildingServices;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices
{
    public class SectionDescription
    {
        public string Description { get; set; }
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DescriptionAccordionView : ContentView
    {
        #region Bindable Properties
        public static BindableProperty HeaderBackgroundColorProperty =
            BindableProperty.Create(nameof(HeaderBackgroundColor),
                typeof(Color),
                typeof(DescriptionAccordionView),
                defaultValue: Color.Transparent,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((DescriptionAccordionView)bindable).UpdateHeaderBackgroundColor();
                });

        public static BindableProperty HeaderOpenedBackgroundColorProperty =
            BindableProperty.Create(nameof(HeaderOpenedBackgroundColor),
                typeof(Color),
                typeof(DescriptionAccordionView),
                defaultValue: Color.Transparent,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((DescriptionAccordionView)bindable).UpdateHeaderBackgroundColor();
                });

        public static BindableProperty HeaderTextColorProperty =
            BindableProperty.Create(nameof(HeaderTextColor),
                typeof(Color),
                typeof(DescriptionAccordionView),
                defaultValue: Color.Black,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((DescriptionAccordionView)bindable).UpdateHeaderTextColor((Color)newVal);
                });

        public static BindableProperty HeaderTextProperty =
            BindableProperty.Create(nameof(HeaderTextProperty),
                typeof(string),
                typeof(DescriptionAccordionView),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((DescriptionAccordionView)bindable).UpdateHeaderText((string)newVal);
                });

        public static BindableProperty AccordionIDProperty =
            BindableProperty.Create(nameof(AccordionIDProperty),
                typeof(string),
                typeof(DescriptionAccordionView),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((DescriptionAccordionView)bindable).UpdateAccordionID((string)newVal);
                });

        public static BindableProperty BodyTextColorProperty =
            BindableProperty.Create(nameof(BodyTextColor),
                typeof(Color),
                typeof(DescriptionAccordionView),
                defaultValue: Color.Black,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((DescriptionAccordionView)bindable).UpdateBodyTextColor((Color)newVal);
                });

        public static BindableProperty BodyTextProperty =
            BindableProperty.Create(nameof(BodyText),
                typeof(string),
                typeof(DescriptionAccordionView),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((DescriptionAccordionView)bindable).UpdateBodyText((string)newVal);
                });


        public static BindableProperty IsBodyVisibleProperty =
            BindableProperty.Create(nameof(IsBodyVisible),
                typeof(bool),
                typeof(DescriptionAccordionView),
                defaultValue: true,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((DescriptionAccordionView)bindable).UpdateBodyVisibility((bool)newVal);
                });

        public static BindableProperty FormIdProperty = BindableProperty.Create(nameof(FormId), typeof(String), typeof(DescriptionAccordionView), default(String));

        public String FormId
        {
            get => (String)GetValue(FormIdProperty);
            set => SetValue(FormIdProperty, value);
        }
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

        public string AccordionIDText
        {
            get
            {
                return (string)GetValue(AccordionIDProperty);
            }
            set
            {
                SetValue(AccordionIDProperty, value);
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


        public DescriptionAccordionView()
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

        public void UpdateAccordionID(string text)
        {
            //AccordionIDLabel.Text = text;
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

        public String GetSectionDescription()
        {
            return DescriptionEditor.Text == null ? "" : DescriptionEditor.Text;
        }

        public void SetSectionDescription(SectionDescription description)
        {
            DescriptionEditor.Text = description.Description;
        }
    }
}
