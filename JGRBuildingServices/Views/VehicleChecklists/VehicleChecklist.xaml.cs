using JGRBuildingServices.ViewModels;
using Newtonsoft.Json;
using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices.Views.VehicleChecklists
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VehicleChecklist : ContentPage
	{
        static readonly String _url = "https://api.jgr1.com";

        public VehicleChecklist()
		{
			InitializeComponent();
		}

        public VehicleChecklist(Int32 id)
        {
            InitializeComponent();

            GetData(id);

            Delete.IsVisible = true;
            SendToOffice.IsVisible = true;
        }

        private void GetData(Int32 id)
        {
            var vehicleChecklist = App.VehicleChecklistsDatabase.GetVehicleChecklistById(id);

            Id.Text = id.ToString();
            CreatedDate_DatePicker.Date = vehicleChecklist.CreatedDate;
            Comments_Entry.Text = vehicleChecklist.Comments;
            Registration_Entry.Text = vehicleChecklist.Registration;
            Mileage_Entry.Text = vehicleChecklist.Mileage;

            StaffSign_StackLayout.IsVisible = false;
            StaffSignImage_StackLayout.IsVisible = false;

            if (vehicleChecklist.StaffSignature != null)
            {
                StaffSignImage_StackLayout.IsVisible = true;
                StaffSignImage.Source = ImageSource.FromStream(() => new MemoryStream(vehicleChecklist.StaffSignature));
            }
            else
            {
                StaffSign_StackLayout.IsVisible = true;
            }

            if (!String.IsNullOrEmpty(vehicleChecklist.Information))
            {
                var information = JsonConvert.DeserializeObject<VehicleChecklistIssuesObjectViewModel>(vehicleChecklist.Information);

                OCFL_Comments.Text = information.Issues.Where(c => c.IssueName == OCFL_Label.Text).Select(c => c.Comments).Single();
                OCFL_Picker.SelectedItem = information.Issues.Where(c => c.IssueName == OCFL_Label.Text).Select(c => c.Condition).Single();

                VLWC_Comments.Text = information.Issues.Where(c => c.IssueName == VLWC_Label.Text).Select(c => c.Comments).Single();
                VLWC_Picker.SelectedItem = information.Issues.Where(c => c.IssueName == VLWC_Label.Text).Select(c => c.Condition).Single();

                WW_Comments.Text = information.Issues.Where(c => c.IssueName == WW_Label.Text).Select(c => c.Comments).Single();
                WW_Picker.SelectedItem = information.Issues.Where(c => c.IssueName == WW_Label.Text).Select(c => c.Condition).Single();

                CWM_Comments.Text = information.Issues.Where(c => c.IssueName == CWM_Label.Text).Select(c => c.Comments).Single();
                CWM_Picker.SelectedItem = information.Issues.Where(c => c.IssueName == CWM_Label.Text).Select(c => c.Condition).Single();

                H_Comments.Text = information.Issues.Where(c => c.IssueName == H_Label.Text).Select(c => c.Comments).Single();
                H_Picker.SelectedItem = information.Issues.Where(c => c.IssueName == H_Label.Text).Select(c => c.Condition).Single();

                HB_Comments.Text = information.Issues.Where(c => c.IssueName == HB_Label.Text).Select(c => c.Comments).Single();
                HB_Picker.SelectedItem = information.Issues.Where(c => c.IssueName == HB_Label.Text).Select(c => c.Condition).Single();

                TP_Comments.Text = information.Issues.Where(c => c.IssueName == TP_Label.Text).Select(c => c.Comments).Single();
                TP_Picker.SelectedItem = information.Issues.Where(c => c.IssueName == TP_Label.Text).Select(c => c.Condition).Single();

                TC_Comments.Text = information.Issues.Where(c => c.IssueName == TC_Label.Text).Select(c => c.Comments).Single();
                TC_Picker.SelectedItem = information.Issues.Where(c => c.IssueName == TC_Label.Text).Select(c => c.Condition).Single();

                CVN_Comments.Text = information.Issues.Where(c => c.IssueName == CVN_Label.Text).Select(c => c.Comments).Single();
                CVN_Picker.SelectedItem = information.Issues.Where(c => c.IssueName == CVN_Label.Text).Select(c => c.Condition).Single();

                SW_Comments.Text = information.Issues.Where(c => c.IssueName == SW_Label.Text).Select(c => c.Comments).Single();
                SW_Picker.SelectedItem = information.Issues.Where(c => c.IssueName == SW_Label.Text).Select(c => c.Condition).Single();

                CCV_Comments.Text = information.Issues.Where(c => c.IssueName == CCV_Label.Text).Select(c => c.Comments).Single();
                CCV_Picker.SelectedItem = information.Issues.Where(c => c.IssueName == CCV_Label.Text).Select(c => c.Condition).Single();

                ISS_Comments.Text = information.Issues.Where(c => c.IssueName == ISS_Label.Text).Select(c => c.Comments).Single();
                ISS_Picker.SelectedItem = information.Issues.Where(c => c.IssueName == ISS_Label.Text).Select(c => c.Condition).Single();

                FE_Comments.Text = information.Issues.Where(c => c.IssueName == FE_Label.Text).Select(c => c.Comments).Single();
                FE_Picker.SelectedItem = information.Issues.Where(c => c.IssueName == FE_Label.Text).Select(c => c.Condition).Single();

                FA_Comments.Text = information.Issues.Where(c => c.IssueName == FA_Label.Text).Select(c => c.Comments).Single();
                FA_Picker.SelectedItem = information.Issues.Where(c => c.IssueName == FA_Label.Text).Select(c => c.Condition).Single();
            }
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            ApplyActivityIndicator();

            var confirmed = await DisplayAlert("Confirm", "Are you sure want to delete this vehicle checklist?", "Yes", "No");

            if (confirmed)
            {
                App.VehicleChecklistsDatabase.DeleteVehicleChecklist(Convert.ToInt32(Id.Text));

                await Navigation.PopAsync(true);
            }
            else
            {
                ResetActivityIndicator();
            }
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            var errors = new List<ValidationErrors>();

            if (String.IsNullOrEmpty(Registration_Entry.Text))
            {
                errors.Add(new ValidationErrors()
                {
                    Message = "Please enter your Vehicle Registration"
                });
            }

            if (String.IsNullOrEmpty(Mileage_Entry.Text))
            {
                errors.Add(new ValidationErrors()
                {
                    Message = "Please enter your Mileage"
                });
            }

            #region Pickers

            if (OCFL_Picker.SelectedIndex == -1)
            {
                errors.Add(new ValidationErrors()
                {
                    Message = "Please choose a value for " + OCFL_Label.Text
                });
            }

            if (VLWC_Picker.SelectedIndex == -1)
            {
                errors.Add(new ValidationErrors()
                {
                    Message = "Please choose a value for " + VLWC_Label.Text
                });
            }

            if (WW_Picker.SelectedIndex == -1)
            {
                errors.Add(new ValidationErrors()
                {
                    Message = "Please choose a value for " + WW_Label.Text
                });
            }

            if (CWM_Picker.SelectedIndex == -1)
            {
                errors.Add(new ValidationErrors()
                {
                    Message = "Please choose a value for " + CWM_Label.Text
                });
            }

            if (H_Picker.SelectedIndex == -1)
            {
                errors.Add(new ValidationErrors()
                {
                    Message = "Please choose a value for " + H_Label.Text
                });
            }

            if (HB_Picker.SelectedIndex == -1)
            {
                errors.Add(new ValidationErrors()
                {
                    Message = "Please choose a value for " + HB_Label.Text
                });
            }

            if (TP_Picker.SelectedIndex == -1)
            {
                errors.Add(new ValidationErrors()
                {
                    Message = "Please choose a value for " + TP_Label.Text
                });
            }

            if (TC_Picker.SelectedIndex == -1)
            {
                errors.Add(new ValidationErrors()
                {
                    Message = "Please choose a value for " + TC_Label.Text
                });
            }

            if (CVN_Picker.SelectedIndex == -1)
            {
                errors.Add(new ValidationErrors()
                {
                    Message = "Please choose a value for " + CVN_Label.Text
                });
            }

            if (SW_Picker.SelectedIndex == -1)
            {
                errors.Add(new ValidationErrors()
                {
                    Message = "Please choose a value for " + SW_Label.Text
                });
            }

            if (CCV_Picker.SelectedIndex == -1)
            {
                errors.Add(new ValidationErrors()
                {
                    Message = "Please choose a value for " + CCV_Label.Text
                });
            }

            if (ISS_Picker.SelectedIndex == -1)
            {
                errors.Add(new ValidationErrors()
                {
                    Message = "Please choose a value for " + ISS_Label.Text
                });
            }

            if (FE_Picker.SelectedIndex == -1)
            {
                errors.Add(new ValidationErrors()
                {
                    Message = "Please choose a value for " + FE_Label.Text
                });
            }

            if (FA_Picker.SelectedIndex == -1)
            {
                errors.Add(new ValidationErrors()
                {
                    Message = "Please choose a value for " + FA_Label.Text
                });
            }

            #endregion

            if (errors.Count == 0)
            {
                SaveVehicleChecklistToDatabase(true);
            }
            else
            {
                var sb = new StringBuilder();

                var lastItem = errors.Last();

                foreach (var item in errors)
                {
                    if (item.Equals(lastItem))
                    {
                        sb.Append(item.Message);
                    }
                    else
                    {
                        sb.Append(item.Message + Environment.NewLine);
                    }
                }

                DisplayAlert("Validation Errors", sb.ToString(), "Acknowledge");
            }
        }

        private async void SaveVehicleChecklistToDatabase(Boolean displayAlert)
        {
            try
            {
                ApplyActivityIndicator();

                var staff = App.StaffDatabase.GetStaffById(Convert.ToInt32(App.SettingsDatabase.GetSetting("AssignedStaffId").Value));
                var vehicleChecklist = App.VehicleChecklistsDatabase.GetVehicleChecklistById(Convert.ToInt32(Id.Text));

                var m = new Models.VehicleChecklists()
                {
                    CreatedDate = CreatedDate_DatePicker.Date,
                    IsDeleted = false,
                    Information = null,
                    OfficeId = null,
                    SentToOffice = false,
                    StaffId = staff.Id,
                    StaffSignature = null,
                    Comments = Comments_Entry.Text,
                    Mileage = Mileage_Entry.Text,
                    Registration = Registration_Entry.Text.ToUpper()
                };

                #region Line Items

                var issues = new List<VehicleChecklistIssuesViewModel>
                {
                    new VehicleChecklistIssuesViewModel()
                    {
                        IssueName = OCFL_Label.Text,
                        Comments = OCFL_Comments.Text,
                        Condition = OCFL_Picker.SelectedIndex != -1 ? OCFL_Picker.SelectedItem.ToString() : "N/A",
                        IssueCategory = "Vehicle"
                    },
                    new VehicleChecklistIssuesViewModel()
                    {
                        IssueName = VLWC_Label.Text,
                        Comments = VLWC_Comments.Text,
                        Condition = VLWC_Picker.SelectedIndex != -1 ? VLWC_Picker.SelectedItem.ToString() : "N/A",
                        IssueCategory = "Vehicle"
                    },
                    new VehicleChecklistIssuesViewModel()
                    {
                        IssueName = WW_Label.Text,
                        Comments = WW_Comments.Text,
                        Condition = WW_Picker.SelectedIndex != -1 ? WW_Picker.SelectedItem.ToString() : "N/A",
                        IssueCategory = "Vehicle"
                    },
                    new VehicleChecklistIssuesViewModel()
                    {
                        IssueName = CWM_Label.Text,
                        Comments = CWM_Comments.Text,
                        Condition = CWM_Picker.SelectedIndex != -1 ? CWM_Picker.SelectedItem.ToString() : "N/A",
                        IssueCategory = "Vehicle"
                    },
                    new VehicleChecklistIssuesViewModel()
                    {
                        IssueName = H_Label.Text,
                        Comments = H_Comments.Text,
                        Condition = H_Picker.SelectedIndex != -1 ? H_Picker.SelectedItem.ToString() : "N/A",
                        IssueCategory = "Vehicle"
                    },
                    new VehicleChecklistIssuesViewModel()
                    {
                        IssueName = HB_Label.Text,
                        Comments = HB_Comments.Text,
                        Condition = HB_Picker.SelectedIndex != -1 ? HB_Picker.SelectedItem.ToString() : "N/A",
                        IssueCategory = "Vehicle"
                    },
                    new VehicleChecklistIssuesViewModel()
                    {
                        IssueName = TP_Label.Text,
                        Comments = TP_Comments.Text,
                        Condition = TP_Picker.SelectedIndex != -1 ? TP_Picker.SelectedItem.ToString() : "N/A",
                        IssueCategory = "Vehicle"
                    },
                    new VehicleChecklistIssuesViewModel()
                    {
                        IssueName = TC_Label.Text,
                        Comments = TC_Comments.Text,
                        Condition = TC_Picker.SelectedIndex != -1 ? TC_Picker.SelectedItem.ToString() : "N/A",
                        IssueCategory = "Vehicle"
                    },
                    new VehicleChecklistIssuesViewModel()
                    {
                        IssueName = CVN_Label.Text,
                        Comments = CVN_Comments.Text,
                        Condition = CVN_Picker.SelectedIndex != -1 ? CVN_Picker.SelectedItem.ToString() : "N/A",
                        IssueCategory = "Vehicle"
                    },
                    new VehicleChecklistIssuesViewModel()
                    {
                        IssueName = SW_Label.Text,
                        Comments = SW_Comments.Text,
                        Condition = SW_Picker.SelectedIndex != -1 ? SW_Picker.SelectedItem.ToString() : "N/A",
                        IssueCategory = "Vehicle"
                    },
                    new VehicleChecklistIssuesViewModel()
                    {
                        IssueName = CCV_Label.Text,
                        Comments = CCV_Comments.Text,
                        Condition = CCV_Picker.SelectedIndex != -1 ? CCV_Picker.SelectedItem.ToString() : "N/A",
                        IssueCategory = "Vehicle"
                    },
                    new VehicleChecklistIssuesViewModel()
                    {
                        IssueName = ISS_Label.Text,
                        Comments = ISS_Comments.Text,
                        Condition = ISS_Picker.SelectedIndex != -1 ? ISS_Picker.SelectedItem.ToString() : "N/A",
                        IssueCategory = "Vehicle"
                    },
                    new VehicleChecklistIssuesViewModel()
                    {
                        IssueName = FE_Label.Text,
                        Comments = FE_Comments.Text,
                        Condition = FE_Picker.SelectedIndex != -1 ? FE_Picker.SelectedItem.ToString() : "N/A",
                        IssueCategory = "Vehicle"
                    },
                    new VehicleChecklistIssuesViewModel()
                    {
                        IssueName = FA_Label.Text,
                        Comments = FA_Comments.Text,
                        Condition = FA_Picker.SelectedIndex != -1 ? FA_Picker.SelectedItem.ToString() : "N/A",
                        IssueCategory = "Vehicle"
                    }
                };

                m.Information = JsonConvert.SerializeObject(new
                {
                    Issues = issues
                });

                #endregion

                if (vehicleChecklist != null)
                {
                    if (vehicleChecklist.StaffSignature != null)
                    {
                        m.StaffSignature = vehicleChecklist.StaffSignature;
                    }
                    else
                    {
                        if (StaffSign != null)
                        {
                            var image = await StaffSign.GetImageStreamAsync(SignatureImageFormat.Png);

                            if (image != null)
                            {
                                byte[] arr;

                                using (var ms = new MemoryStream())
                                {
                                    await image.CopyToAsync(ms);

                                    arr = ms.ToArray();
                                }

                                m.StaffSignature = arr;
                            }
                        }
                    }
                }
                else
                {
                    if (StaffSign != null)
                    {
                        var image = await StaffSign.GetImageStreamAsync(SignatureImageFormat.Png);

                        if (image != null)
                        {
                            byte[] arr;

                            using (var ms = new MemoryStream())
                            {
                                await image.CopyToAsync(ms);

                                arr = ms.ToArray();
                            }

                            m.StaffSignature = arr;
                        }
                    }
                }

                if (!String.IsNullOrEmpty(Id.Text))
                {
                    m.Id = Convert.ToInt32(Id.Text);

                    App.VehicleChecklistsDatabase.UpdateVehicleChecklist(m);

                    if (displayAlert)
                    {
                        ResetActivityIndicator();

                        await DisplayAlert("Saved", "This vehicle checklist has been saved successfully.", "Acknowledge");
                    }
                }
                else
                {
                    App.VehicleChecklistsDatabase.InsertVehicleChecklist(m);

                    await Navigation.PushAsync(new VehicleChecklist(m.Id));

                    Navigation.RemovePage(this);
                }
            }
            catch (Exception ex)
            {
                ResetActivityIndicator();

                await DisplayAlert("Error", "An error has occurred. " + ex.Message, "Acknowledge");
            }
        }

        private async void SendToOffice_Clicked(object sender, EventArgs e)
        {
            SaveVehicleChecklistToDatabase(false);

            var isConfirmed = false;

            var helpers = new Helpers();

            if (helpers.IsDeviceOnline())
            {
                isConfirmed = await DisplayAlert("Confirm", "Are you sure you want to submit this vehicle checklist?", "Yes", "Cancel");
            }
            else
            {
                ResetActivityIndicator();

                await DisplayAlert("No Internet Connection", "You need to be connected to the internet in order to submit this vehicle checklist.", "Acknowledge");
            }

            if (isConfirmed)
            {
                var vehicleChecklist = App.VehicleChecklistsDatabase.GetVehicleChecklistById(Convert.ToInt32(Id.Text));

                var client = new HttpClient
                {
                    BaseAddress = new Uri(_url),
                };

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var ma = new VehicleChecklistViewModel()
                {
                    CreatedDate = vehicleChecklist.CreatedDate,
                    Information = vehicleChecklist.Information,
                    Comments = vehicleChecklist.Comments,
                    IsDeleted = false,
                    Mileage = vehicleChecklist.Mileage,
                    Registration = vehicleChecklist.Registration,
                    StaffId = vehicleChecklist.StaffId,
                    StaffSignature = vehicleChecklist.StaffSignature
                };

                var jsonObject = JsonConvert.SerializeObject(ma).ToString();

                var response = await client.PostAsync("PostVehicleChecklist", new StringContent(jsonObject, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var contents = await response.Content.ReadAsStringAsync();

                    var jsonResponse = JsonConvert.DeserializeObject<CreateVehicleChecklistResponseViewModel>(contents);

                    App.VehicleChecklistsDatabase.VehicleChecklistSentToOffice(vehicleChecklist.Id, jsonResponse.Id);

                    await DisplayAlert("Success", "Your vehicle checklist has been successfully sent to the office.", "Back to Vehicle Checklists");

                    await Navigation.PopAsync(true);
                }
                else
                {
                    ResetActivityIndicator();

                    await DisplayAlert("Error", "An error has occurred.", "Acknowledge");
                }
            }
            else
            {
                ResetActivityIndicator();
            }
        }

        private void ApplyActivityIndicator()
        {
            ActivityIndicator_StackLayout.IsVisible = true;
            CallToActions_StackLayout.IsVisible = false;
        }

        private void ResetActivityIndicator()
        {
            ActivityIndicator_StackLayout.IsVisible = false;
            CallToActions_StackLayout.IsVisible = true;
        }
    }
}