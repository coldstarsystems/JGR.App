using JGRBuildingServices.ViewModels;
using Newtonsoft.Json;
using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices.Views.HotWorkPermit.AccordionViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IssueOfPermit : ContentView
    {
        #region Bindable Properties
        public static BindableProperty HeaderBackgroundColorProperty =
            BindableProperty.Create(nameof(HeaderBackgroundColor),
                typeof(Color),
                typeof(IssueOfPermit),
                defaultValue: Color.Transparent,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((IssueOfPermit)bindable).UpdateHeaderBackgroundColor();
                });

        public static BindableProperty HeaderOpenedBackgroundColorProperty =
            BindableProperty.Create(nameof(HeaderOpenedBackgroundColor),
                typeof(Color),
                typeof(IssueOfPermit),
                defaultValue: Color.Transparent,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((IssueOfPermit)bindable).UpdateHeaderBackgroundColor();
                });

        public static BindableProperty HeaderTextColorProperty =
            BindableProperty.Create(nameof(HeaderTextColor),
                typeof(Color),
                typeof(IssueOfPermit),
                defaultValue: Color.Black,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((IssueOfPermit)bindable).UpdateHeaderTextColor((Color)newVal);
                });

        public static BindableProperty HeaderTextProperty =
            BindableProperty.Create(nameof(HeaderTextProperty),
                typeof(string),
                typeof(IssueOfPermit),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((IssueOfPermit)bindable).UpdateHeaderText((string)newVal);
                });

        public static BindableProperty BodyTextColorProperty =
            BindableProperty.Create(nameof(BodyTextColor),
                typeof(Color),
                typeof(IssueOfPermit),
                defaultValue: Color.Black,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((IssueOfPermit)bindable).UpdateBodyTextColor((Color)newVal);
                });

        public static BindableProperty BodyTextProperty =
            BindableProperty.Create(nameof(BodyText),
                typeof(string),
                typeof(IssueOfPermit),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((IssueOfPermit)bindable).UpdateBodyText((string)newVal);
                });


        public static BindableProperty IsBodyVisibleProperty =
            BindableProperty.Create(nameof(IsBodyVisible),
                typeof(bool),
                typeof(IssueOfPermit),
                defaultValue: true,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((IssueOfPermit)bindable).UpdateBodyVisibility((bool)newVal);
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


        public IssueOfPermit()
        {
            InitializeComponent();
            IsBodyVisible = false;

        }

        /// <summary>
        /// Updates the color of the header background.
        /// </summary>
        /// <param name="color">Color.</param>
        public void UpdateHeaderBackgroundColor(Color color)
        {
            HeaderView.BackgroundColor = color;
        }

        /// <summary>
        /// Updates the color of the header background.
        /// </summary>
        public void UpdateHeaderBackgroundColor()
        {
            if (IsBodyVisible)
            {
                HeaderView.BackgroundColor = HeaderOpenedBackgroundColor;
                //BodyView.BackgroundColor = HeaderOpenedBackgroundColor;
            }
            else
            {
                HeaderView.BackgroundColor = HeaderBackgroundColor;
            }
        }

        /// <summary>
        /// Updates the color of the header text.
        /// </summary>
        /// <param name="color">Color.</param>
        public void UpdateHeaderTextColor(Color color)
        {
            HeaderLabel.TextColor = color;
        }

        /// <summary>
        /// Updates the color of the body text.
        /// </summary>
        /// <param name="color">Color.</param>
        public void UpdateBodyTextColor(Color color)
        {
            //BodyLabel.TextColor = color;
        }

        /// <summary>
        /// Updates the header text.
        /// </summary>
        /// <param name="text">Text.</param>
        public void UpdateHeaderText(string text)
        {
            HeaderLabel.Text = text;
        }

        /// <summary>
        /// Updates the body text.
        /// </summary>
        /// <param name="text">Text.</param>
        public void UpdateBodyText(string text)
        {
            //BodyLabel.Text = text;
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

        public async Task<ViewModels.IssueOfPermit> GetSection(Models.HotWorksPermit hotWorksPermit)
        {
            var issueTime = IssueTime.NullableTime;
            var issuePermitTime = IssuePermitTime.NullableTime;
            var issueExpiryTime = IssueExpiryTime.NullableTime;

            if (issueTime == null)
            {
                issueTime = TimeSpan.Zero;
            }

            if (issuePermitTime == null)
            {
                issuePermitTime = TimeSpan.Zero;
            }

            if (issueExpiryTime == null)
            {
                issueExpiryTime = TimeSpan.Zero;
            }

            ViewModels.IssueOfPermit Issue = new ViewModels.IssueOfPermit
            {
                IssuePrintName = IssueName.Text,
                IssueCompany = IssueCompany.Text,
                IssueJobTitle = IssueJobTitle.Text,
                IssueDate = IssueDatePicker.Date,
                IssueTime = issueTime,
                IssueTimePermit = issuePermitTime,
                IssueExpiryTimePermit = issueExpiryTime
            };

            if (hotWorksPermit != null)
            {
                var worksPermitObject = JsonConvert.DeserializeObject<HotWorkPermitViewModel>(hotWorksPermit.Content);

                if (worksPermitObject.IssueOfPermit.IssueSigned != null)
                {
                    Issue.IssueSigned = worksPermitObject.IssueOfPermit.IssueSigned;
                }
                else
                {
                    var image = await IssueSignature.GetImageStreamAsync(SignatureImageFormat.Png);

                    if (image != null)
                    {
                        byte[] arr;

                        using (var ms = new MemoryStream())
                        {
                            await image.CopyToAsync(ms);

                            arr = ms.ToArray();
                        }

                        Issue.IssueSigned = arr;
                    }
                }
            } 
            else
            {
                var image = await IssueSignature.GetImageStreamAsync(SignatureImageFormat.Png);

                if (image != null)
                {
                    byte[] arr;

                    using (var ms = new MemoryStream())
                    {
                        await image.CopyToAsync(ms);

                        arr = ms.ToArray();
                    }

                    Issue.IssueSigned = arr;
                }
            }

            return Issue;
        }

        public void SetSection(ViewModels.IssueOfPermit issue)
        {
            IssueName.Text = issue.IssuePrintName;
            IssueCompany.Text = issue.IssueCompany;
            IssueJobTitle.Text = issue.IssueJobTitle;

            var date = DateTime.Parse(issue.IssueDate.ToString());
            IssueDatePicker.Date = date;

            var time1 = TimeSpan.Parse(issue.IssueTime.ToString());
            IssueTime.Time = time1;

            var time2 = TimeSpan.Parse(issue.IssueTimePermit.ToString());
            IssuePermitTime.Time = time2;

            var time3 = TimeSpan.Parse(issue.IssueExpiryTimePermit.ToString());
            IssueExpiryTime.Time = time3;

            if (issue.IssueSigned != null)
            {
                IssueSignatureImage.Source = ImageSource.FromStream(() => new MemoryStream(issue.IssueSigned));
                IssueSignatureImage.IsVisible = true;

                IssueSignature.IsVisible = false;
            }
            else
            {
                IssueSignature.IsVisible = true;
            }
        }
    }
}