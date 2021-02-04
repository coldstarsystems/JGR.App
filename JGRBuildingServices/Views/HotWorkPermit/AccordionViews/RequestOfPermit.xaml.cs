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
    public partial class RequestOfPermit : ContentView
    {
        #region Bindable Properties
        public static BindableProperty HeaderBackgroundColorProperty =
            BindableProperty.Create(nameof(HeaderBackgroundColor),
                typeof(Color),
                typeof(RequestOfPermit),
                defaultValue: Color.Transparent,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((RequestOfPermit)bindable).UpdateHeaderBackgroundColor();
                });

        public static BindableProperty HeaderOpenedBackgroundColorProperty =
            BindableProperty.Create(nameof(HeaderOpenedBackgroundColor),
                typeof(Color),
                typeof(RequestOfPermit),
                defaultValue: Color.Transparent,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((RequestOfPermit)bindable).UpdateHeaderBackgroundColor();
                });

        public static BindableProperty HeaderTextColorProperty =
            BindableProperty.Create(nameof(HeaderTextColor),
                typeof(Color),
                typeof(RequestOfPermit),
                defaultValue: Color.Black,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((RequestOfPermit)bindable).UpdateHeaderTextColor((Color)newVal);
                });

        public static BindableProperty HeaderTextProperty =
            BindableProperty.Create(nameof(HeaderTextProperty),
                typeof(string),
                typeof(RequestOfPermit),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((RequestOfPermit)bindable).UpdateHeaderText((string)newVal);
                });

        public static BindableProperty BodyTextColorProperty =
            BindableProperty.Create(nameof(BodyTextColor),
                typeof(Color),
                typeof(RequestOfPermit),
                defaultValue: Color.Black,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((RequestOfPermit)bindable).UpdateBodyTextColor((Color)newVal);
                });

        public static BindableProperty BodyTextProperty =
            BindableProperty.Create(nameof(BodyText),
                typeof(string),
                typeof(RequestOfPermit),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((RequestOfPermit)bindable).UpdateBodyText((string)newVal);
                });


        public static BindableProperty IsBodyVisibleProperty =
            BindableProperty.Create(nameof(IsBodyVisible),
                typeof(bool),
                typeof(RequestOfPermit),
                defaultValue: true,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((RequestOfPermit)bindable).UpdateBodyVisibility((bool)newVal);
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


        public RequestOfPermit()
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

        public async Task<ViewModels.RequestOfPermit> GetSection(Models.HotWorksPermit hotWorksPermit)
        {
            var requestTime = RequestTime.NullableTime;

            if (requestTime == null)
            {
                requestTime = TimeSpan.Zero;
            }

            ViewModels.RequestOfPermit request = new ViewModels.RequestOfPermit
            {
                RequestMinutes = RequestMinutesEntry.Text,
                RequestPrintName = RequestName.Text,
                RequestCompany = RequestCompany.Text,
                RequestJobTitle = RequestJobTitle.Text,
                RequestDate = RequestDatePicker.Date,
                RequestTime = requestTime
            };

            if (hotWorksPermit != null)
            {
                var worksPermitObject = JsonConvert.DeserializeObject<HotWorkPermitViewModel>(hotWorksPermit.Content);

                if (worksPermitObject.RequestOfPermit.RequestSigned != null)
                {
                    request.RequestSigned = worksPermitObject.RequestOfPermit.RequestSigned;
                } 
                else
                {
                    var image = await RequestSignature.GetImageStreamAsync(SignatureImageFormat.Png);

                    if (image != null)
                    {
                        byte[] arr;

                        using (var ms = new MemoryStream())
                        {
                            await image.CopyToAsync(ms);

                            arr = ms.ToArray();
                        }

                        request.RequestSigned = arr;
                    }
                }
            }
            else
            {
                var image = await RequestSignature.GetImageStreamAsync(SignatureImageFormat.Png);

                if (image != null)
                {
                    byte[] arr;

                    using (var ms = new MemoryStream())
                    {
                        await image.CopyToAsync(ms);

                        arr = ms.ToArray();
                    }

                    request.RequestSigned = arr;
                }
            }

            return request;
        }

        public void SetSection(ViewModels.RequestOfPermit request)
        {
            RequestMinutesEntry.Text = request.RequestMinutes;
            RequestName.Text = request.RequestPrintName;
            RequestCompany.Text = request.RequestCompany;
            RequestJobTitle.Text = request.RequestJobTitle;

            var date = DateTime.Parse(request.RequestDate.ToString());
            RequestDatePicker.NullableDate = date;

            var time1 = TimeSpan.Parse(request.RequestTime.ToString());
            RequestTime.Time = time1;

            if (request.RequestSigned != null)
            {
                RequestSignatureImage.Source = ImageSource.FromStream(() => new MemoryStream(request.RequestSigned));
                RequestSignatureImage.IsVisible = true;

                RequestSignature.IsVisible = false;
            } 
            else
            {
                RequestSignature.IsVisible = true;
            }
        }
    }
}