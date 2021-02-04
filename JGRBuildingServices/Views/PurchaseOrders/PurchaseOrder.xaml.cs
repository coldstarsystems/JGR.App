using JGRBuildingServices.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace JGRBuildingServices.Views.PurchaseOrders
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PurchaseOrder : ContentPage
    {
        static readonly String _url = "https://api.jgr1.com";

        public PurchaseOrder()
        {
            InitializeComponent();
        }

        public PurchaseOrder(Int32 id)
        {
            InitializeComponent();

            GetData(id);

            Delete.IsVisible = true;
            SendToOffice.IsVisible = true;
        }

        private void GetData(Int32 id)
        {
            var purchaseOrder = App.PurchaseOrdersDatabase.GetPurchaseOrderById(id);

            Id.Text = id.ToString();
            Number.Text = purchaseOrder.Number;
            PurchaseOrderNumber_Label.Text = "P.O No. " + purchaseOrder.Number;

            PurchaseOrderNumber_Label.IsVisible = true;

            if (purchaseOrder.DeliveryDate != null)
            {
                DeliveryDate_DatePicker.Date = Convert.ToDateTime(purchaseOrder.DeliveryDate);
            }

            Supplier_Editor.Text = purchaseOrder.Supplier;
            ShipTo_Editor.Text = purchaseOrder.ShipTo;

            ShippingMethod_Entry.Text = purchaseOrder.ShippingMethod;
            ShippingTerms_Entry.Text = purchaseOrder.ShippingTerms;

            SubTotal_Entry.Text = purchaseOrder.SubTotal.ToString();
            VAT_Entry.Text = purchaseOrder.VAT.ToString();
            Total_Entry.Text = purchaseOrder.Total.ToString();

            var lineItems = JsonConvert.DeserializeObject<PurchaseOrderLineItemsViewModel>(purchaseOrder.LineItems);

            if (lineItems.LineItems != null)
            {
                ListItems.Children.Clear();

                foreach (var item in lineItems.LineItems)
                {
                    ListItems.Children.Add(new ListItems(item)
                    {

                    });
                }
            }
        }

        private void AddMoreButton_Clicked(object sender, EventArgs e)
        {
            ListItems.Children.Add(new ListItems()
            {

            });
        }

        private void Save_Clicked(object sender, EventArgs e)
        {
            SavePurchaseOrderToDatabase(true);
        }

        private async void SavePurchaseOrderToDatabase(Boolean displayAlert)
        {
            try
            {
                ApplyActivityIndicator();

                var staff = App.StaffDatabase.GetStaffById(Convert.ToInt32(App.SettingsDatabase.GetSetting("AssignedStaffId").Value));
                var purchaseOrder = App.PurchaseOrdersDatabase.GetPurchaseOrderById(Convert.ToInt32(Id.Text));

                var m = new Models.PurchaseOrders()
                {
                    CreatedDate = DateTime.Now,
                    InvoiceAmount = null,
                    VAT = null,
                    DeliveryDate = DeliveryDate_DatePicker?.Date,
                    InvoiceNumber = null,
                    IsDeleted = false,
                    LineItems = null,
                    OfficeId = null,
                    SentToOffice = false,
                    ShippingMethod = ShippingMethod_Entry.Text,
                    ShippingTerms = ShippingTerms_Entry.Text,
                    ShipTo = ShipTo_Editor.Text,
                    StaffId = staff.Id,
                    SubTotal = null,
                    Supplier = Supplier_Editor.Text,
                    Total = null
                };

                var lineItems = new List<PurchaseOrderLineItemViewModel>();

                Decimal? subTotal = 0;

                foreach (var item in ListItems.Children)
                {
                    if (item.GetType() == typeof(ListItems))
                    {
                        var items = (ListItems)item;

                        var section = items.GetItems();

                        if (section.Quantity != 0)
                        {
                            var lineTotal = (section.UnitPrice * section.Quantity);

                            lineItems.Add(new PurchaseOrderLineItemViewModel()
                            {
                                Quantity = section.Quantity,
                                Item = section.Item,
                                Description = section.Description,
                                Job = section.Job,
                                UnitPrice = section.UnitPrice,
                                LineTotal = lineTotal
                            });

                            subTotal = subTotal + lineTotal;
                        }
                    }
                }

                m.SubTotal = subTotal;
                m.VAT = subTotal * (Decimal)0.20;
                m.Total = subTotal + m.VAT;

                m.LineItems = JsonConvert.SerializeObject(new
                {
                    LineItems = lineItems
                });

                if (!String.IsNullOrEmpty(Id.Text))
                {
                    m.Id = Convert.ToInt32(Id.Text);
                    m.Number = Number.Text;

                    App.PurchaseOrdersDatabase.UpdatePurchaseOrder(m);

                    if (displayAlert)
                    {
                        ResetActivityIndicator();

                        SubTotal_Entry.Text = m.SubTotal.ToString();
                        VAT_Entry.Text = m.VAT.ToString();
                        Total_Entry.Text = m.Total.ToString();

                        await DisplayAlert("Saved", "This purchase order has been saved successfully.", "Acknowledge");
                    }
                }
                else
                {
                    m.Number = staff.PurchaseOrderIdentifier + String.Format("{0:D3}", staff.NextPurchaseOrderNumber);

                    App.PurchaseOrdersDatabase.InsertPurchaseOrder(m);

                    App.StaffDatabase.UpdateStaffPurchaseOrderNumber(staff.Id);

                    await Navigation.PushAsync(new PurchaseOrder(m.Id));

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
            SavePurchaseOrderToDatabase(false);

            var isConfirmed = false;

            var helpers = new Helpers();

            if (helpers.IsDeviceOnline())
            {
                isConfirmed = await DisplayAlert("Confirm", "Are you sure you want to submit this purchase order?", "Yes", "Cancel");
            }
            else
            {
                ResetActivityIndicator();

                await DisplayAlert("No Internet Connection", "You need to be connected to the internet in order to submit this purchase order.", "Acknowledge");
            }

            if (isConfirmed)
            {
                var purchaseOrder = App.PurchaseOrdersDatabase.GetPurchaseOrderById(Convert.ToInt32(Id.Text));

                var client = new HttpClient
                {
                    BaseAddress = new Uri(_url),
                };

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var ma = new PurchaseOrderViewModel()
                {
                    InvoiceAmount = purchaseOrder.InvoiceAmount,
                    VAT = purchaseOrder.VAT,
                    CreatedDate = purchaseOrder.CreatedDate,
                    DeliveryDate = purchaseOrder.DeliveryDate,
                    InvoiceNumber = purchaseOrder.InvoiceNumber,
                    IsDeleted = false,
                    LineItems = purchaseOrder.LineItems,
                    Number = purchaseOrder.Number,
                    OfficeId = purchaseOrder.OfficeId,
                    SentToOffice = purchaseOrder.SentToOffice,
                    ShippingMethod = purchaseOrder.ShippingMethod,
                    ShippingTerms = purchaseOrder.ShippingTerms,
                    ShipTo = purchaseOrder.ShipTo,
                    StaffId = purchaseOrder.StaffId,
                    SubTotal = purchaseOrder.SubTotal,
                    Supplier = purchaseOrder.Supplier,
                    Total = purchaseOrder.Total
                };

                var jsonObject = JsonConvert.SerializeObject(ma).ToString();

                var response = await client.PostAsync("PostPurchaseOrder", new StringContent(jsonObject, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    var contents = await response.Content.ReadAsStringAsync();

                    var jsonResponse = JsonConvert.DeserializeObject<CreatePurchaseOrderResponseViewModel>(contents);

                    App.PurchaseOrdersDatabase.PurchaseOrderSentToOffice(purchaseOrder.Id, jsonResponse.Id);

                    await DisplayAlert("Success", "Your purchase order has been successfully sent to the office.", "Back to Purchase Orders");

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

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            ApplyActivityIndicator();

            var confirmed = await DisplayAlert("Confirm", "Are you sure want to delete this purchase order?", "Yes", "No");

            if (confirmed)
            {
                App.PurchaseOrdersDatabase.DeletePurchaseOrder(Convert.ToInt32(Id.Text));

                await Navigation.PopAsync(true);
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