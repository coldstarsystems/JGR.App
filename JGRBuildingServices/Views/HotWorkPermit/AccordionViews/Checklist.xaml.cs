using JGRBuildingServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices.Views.HotWorkPermit.AccordionViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Checklist : ContentView
    {
        #region Bindable Properties
        public static BindableProperty HeaderBackgroundColorProperty =
            BindableProperty.Create(nameof(HeaderBackgroundColor),
                typeof(Color),
                typeof(Checklist),
                defaultValue: Color.Transparent,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((Checklist)bindable).UpdateHeaderBackgroundColor();
                });

        public static BindableProperty HeaderOpenedBackgroundColorProperty =
            BindableProperty.Create(nameof(HeaderOpenedBackgroundColor),
                typeof(Color),
                typeof(Checklist),
                defaultValue: Color.Transparent,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((Checklist)bindable).UpdateHeaderBackgroundColor();
                });

        public static BindableProperty HeaderTextColorProperty =
            BindableProperty.Create(nameof(HeaderTextColor),
                typeof(Color),
                typeof(Checklist),
                defaultValue: Color.Black,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((Checklist)bindable).UpdateHeaderTextColor((Color)newVal);
                });

        public static BindableProperty HeaderTextProperty =
            BindableProperty.Create(nameof(HeaderTextProperty),
                typeof(string),
                typeof(Checklist),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((Checklist)bindable).UpdateHeaderText((string)newVal);
                });

        public static BindableProperty BodyTextColorProperty =
            BindableProperty.Create(nameof(BodyTextColor),
                typeof(Color),
                typeof(Checklist),
                defaultValue: Color.Black,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((Checklist)bindable).UpdateBodyTextColor((Color)newVal);
                });

        public static BindableProperty BodyTextProperty =
            BindableProperty.Create(nameof(BodyText),
                typeof(string),
                typeof(Checklist),
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((Checklist)bindable).UpdateBodyText((string)newVal);
                });


        public static BindableProperty IsBodyVisibleProperty =
            BindableProperty.Create(nameof(IsBodyVisible),
                typeof(bool),
                typeof(Checklist),
                defaultValue: true,
                propertyChanged: (bindable, oldVal, newVal) =>
                {
                    ((Checklist)bindable).UpdateBodyVisibility((bool)newVal);
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


        public Checklist()
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

        public List<ViewModels.PermitChecklist> GetSection()
        {
            var check = new List<PermitChecklist>();

            #region Checks Validation
            if (Question1Picker.SelectedIndex == -1)
            {
                Question1Picker.SelectedItem = "Select";
            }
            if (Question2Picker.SelectedIndex == -1)
            {
                Question2Picker.SelectedItem = "Select";
            }
            if (Question3Picker.SelectedIndex == -1)
            {
                Question3Picker.SelectedItem = "Select";
            }
            if (Question4Picker.SelectedIndex == -1)
            {
                Question4Picker.SelectedItem = "Select";
            }
            if (Question5Picker.SelectedIndex == -1)
            {
                Question5Picker.SelectedItem = "Select";
            }
            if (Question6Picker.SelectedIndex == -1)
            {
                Question6Picker.SelectedItem = "Select";
            }
            if (Question7Picker.SelectedIndex == -1)
            {
                Question7Picker.SelectedItem = "Select";
            }
            if (Question8Picker.SelectedIndex == -1)
            {
                Question8Picker.SelectedItem = "Select";
            }
            if (Question9Picker.SelectedIndex == -1)
            {
                Question9Picker.SelectedItem = "Select";
            }
            if (Question10Picker.SelectedIndex == -1)
            {
                Question10Picker.SelectedItem = "Select";
            }
            if (Question11Picker.SelectedIndex == -1)
            {
                Question11Picker.SelectedItem = "Select";
            }
            if (Question12Picker.SelectedIndex == -1)
            {
                Question12Picker.SelectedItem = "Select";
            }
            if (Question13Picker.SelectedIndex == -1)
            {
                Question13Picker.SelectedItem = "Select";
            }
            if (Question14Picker.SelectedIndex == -1)
            {
                Question14Picker.SelectedItem = "Select";
            }
            if (Question15Picker.SelectedIndex == -1)
            {
                Question15Picker.SelectedItem = "Select";
            }
            if (Question16Picker.SelectedIndex == -1)
            {
                Question16Picker.SelectedItem = "Select";
            }
            if (Question17Picker.SelectedIndex == -1)
            {
                Question17Picker.SelectedItem = "Select";
            }
            if (Question18Picker.SelectedIndex == -1)
            {
                Question18Picker.SelectedItem = "Select";
            }
            if (Question19Picker.SelectedIndex == -1)
            {
                Question19Picker.SelectedItem = "Select";
            }
            #endregion

            #region Checklist Answers
            check.Add(new PermitChecklist()
            {
                Id = Question1Id.Text,
                Question = Question1.Text,
                InPlace = Question1Picker.SelectedItem.ToString(),
                Comments = Question1Comments.Text
            });

            check.Add(new PermitChecklist()
            {
                Id = Question2Id.Text,
                Question = Question2.Text,
                InPlace = Question2Picker.SelectedItem.ToString(),
                Comments = Question2Comments.Text
            });

            check.Add(new PermitChecklist()
            {
                Id = Question3Id.Text,
                Question = Question3.Text,
                InPlace = Question3Picker.SelectedItem.ToString(),
                Comments = Question3Comments.Text
            });

            check.Add(new PermitChecklist()
            {
                Id = Question4Id.Text,
                Question = Question4.Text,
                InPlace = Question4Picker.SelectedItem.ToString(),
                Comments = Question4Comments.Text
            });

            check.Add(new PermitChecklist()
            {
                Id = Question5Id.Text,
                Question = Question5.Text,
                InPlace = Question5Picker.SelectedItem.ToString(),
                Comments = Question5Comments.Text
            });

            check.Add(new PermitChecklist()
            {
                Id = Question6Id.Text,
                Question = Question6.Text,
                InPlace = Question6Picker.SelectedItem.ToString(),
                Comments = Question6Comments.Text
            });

            check.Add(new PermitChecklist()
            {
                Id = Question7Id.Text,
                Question = Question7.Text,
                InPlace = Question7Picker.SelectedItem.ToString(),
                Comments = Question7Comments.Text
            });

            check.Add(new PermitChecklist()
            {
                Id = Question8Id.Text,
                Question = Question8.Text,
                InPlace = Question8Picker.SelectedItem.ToString(),
                Comments = Question8Comments.Text
            });

            check.Add(new PermitChecklist()
            {
                Id = Question9Id.Text,
                Question = Question9.Text,
                InPlace = Question9Picker.SelectedItem.ToString(),
                Comments = Question9Comments.Text
            });

            check.Add(new PermitChecklist()
            {
                Id = Question10Id.Text,
                Question = Question10.Text,
                InPlace = Question10Picker.SelectedItem.ToString(),
                Comments = Question10Comments.Text
            });

            check.Add(new PermitChecklist()
            {
                Id = Question11Id.Text,
                Question = Question11.Text,
                InPlace = Question11Picker.SelectedItem.ToString(),
                Comments = Question11Comments.Text
            });

            check.Add(new PermitChecklist()
            {
                Id = Question12Id.Text,
                Question = Question12.Text,
                InPlace = Question12Picker.SelectedItem.ToString(),
                Comments = Question12Comments.Text
            });

            check.Add(new PermitChecklist()
            {
                Id = Question13Id.Text,
                Question = Question13.Text,
                InPlace = Question13Picker.SelectedItem.ToString(),
                Comments = Question13Comments.Text
            });

            check.Add(new PermitChecklist()
            {
                Id = Question14Id.Text,
                Question = Question14.Text,
                InPlace = Question14Picker.SelectedItem.ToString(),
                Comments = Question14Comments.Text
            });

            check.Add(new PermitChecklist()
            {
                Id = Question15Id.Text,
                Question = Question15.Text,
                InPlace = Question15Picker.SelectedItem.ToString(),
                Comments = Question15Comments.Text
            });

            check.Add(new PermitChecklist()
            {
                Id = Question16Id.Text,
                Question = Question16.Text,
                InPlace = Question16Picker.SelectedItem.ToString(),
                Comments = Question16Comments.Text
            });

            check.Add(new PermitChecklist()
            {
                Id = Question17Id.Text,
                Question = Question17.Text,
                InPlace = Question17Picker.SelectedItem.ToString(),
                Comments = Question17Comments.Text
            });

            check.Add(new PermitChecklist()
            {
                Id = Question18Id.Text,
                Question = Question18.Text,
                InPlace = Question18Picker.SelectedItem.ToString(),
                Comments = Question18Comments.Text
            });

            check.Add(new PermitChecklist()
            {
                Id = Question19Id.Text,
                Question = Question19.Text,
                InPlace = Question19Picker.SelectedItem.ToString(),
                Comments = Question19Comments.Text
            });
            #endregion

            return check;
        }

        public void SetSection(ViewModels.PermitChecklist checklist)
        {
            switch (checklist.Id)
            {
                case "Question1":
                    Question1.Text = checklist.Question;
                    Question1Picker.SelectedItem = checklist.InPlace;
                    Question1Comments.Text = checklist.Comments;
                    break;
                case "Question2":
                    Question2.Text = checklist.Question;
                    Question2Picker.SelectedItem = checklist.InPlace;
                    Question2Comments.Text = checklist.Comments;
                    break;

                case "Question3":
                    Question3.Text = checklist.Question;
                    Question3Picker.SelectedItem = checklist.InPlace;
                    Question3Comments.Text = checklist.Comments;
                    break;
                case "Question4":
                    Question4.Text = checklist.Question;
                    Question4Picker.SelectedItem = checklist.InPlace;
                    Question4Comments.Text = checklist.Comments;
                    break;
                case "Question5":
                    Question5.Text = checklist.Question;
                    Question5Picker.SelectedItem = checklist.InPlace;
                    Question5Comments.Text = checklist.Comments;
                    break;
                case "Question6":
                    Question6.Text = checklist.Question;
                    Question6Picker.SelectedItem = checklist.InPlace;
                    Question6Comments.Text = checklist.Comments;
                    break;
                case "Question7":
                    Question7.Text = checklist.Question;
                    Question7Picker.SelectedItem = checklist.InPlace;
                    Question7Comments.Text = checklist.Comments;
                    break;
                case "Question8":
                    Question8.Text = checklist.Question;
                    Question8Picker.SelectedItem = checklist.InPlace;
                    Question8Comments.Text = checklist.Comments;
                    break;
                case "Question9":
                    Question9.Text = checklist.Question;
                    Question9Picker.SelectedItem = checklist.InPlace;
                    Question9Comments.Text = checklist.Comments;
                    break;
                case "Question10":
                    Question10.Text = checklist.Question;
                    Question10Picker.SelectedItem = checklist.InPlace;
                    Question10Comments.Text = checklist.Comments;
                    break;
                case "Question11":
                    Question11.Text = checklist.Question;
                    Question11Picker.SelectedItem = checklist.InPlace;
                    Question11Comments.Text = checklist.Comments;
                    break;
                case "Question12":
                    Question12.Text = checklist.Question;
                    Question12Picker.SelectedItem = checklist.InPlace;
                    Question12Comments.Text = checklist.Comments;
                    break;
                case "Question13":
                    Question13.Text = checklist.Question;
                    Question13Picker.SelectedItem = checklist.InPlace;
                    Question13Comments.Text = checklist.Comments;
                    break;
                case "Question14":
                    Question14.Text = checklist.Question;
                    Question14Picker.SelectedItem = checklist.InPlace;
                    Question14Comments.Text = checklist.Comments;
                    break;
                case "Question15":
                    Question15.Text = checklist.Question;
                    Question15Picker.SelectedItem = checklist.InPlace;
                    Question15Comments.Text = checklist.Comments;
                    break;
                case "Question16":
                    Question16.Text = checklist.Question;
                    Question16Picker.SelectedItem = checklist.InPlace;
                    Question16Comments.Text = checklist.Comments;
                    break;
                case "Question17":
                    Question17.Text = checklist.Question;
                    Question17Picker.SelectedItem = checklist.InPlace;
                    Question17Comments.Text = checklist.Comments;
                    break;
                case "Question18":
                    Question18.Text = checklist.Question;
                    Question18Picker.SelectedItem = checklist.InPlace;
                    Question18Comments.Text = checklist.Comments;
                    break;
                case "Question19":
                    Question19.Text = checklist.Question;
                    Question19Picker.SelectedItem = checklist.InPlace;
                    Question19Comments.Text = checklist.Comments;
                    break;
            }
        }
    }
}