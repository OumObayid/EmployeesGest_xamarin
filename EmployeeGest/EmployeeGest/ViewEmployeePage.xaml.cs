using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

using Plugin.Messaging;


namespace EmployeeGest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewEmployeePage : ContentPage
    {
        Employee employeeDetails;
        public ViewEmployeePage(Employee details)
        {
            InitializeComponent();
            if (details != null)
            {
                employeeDetails = details;
                PopulateDetails(employeeDetails);
            }


        }
        private void PopulateDetails(Employee details)
        {
            name.Text = details.Name;
            address.Text = details.Address;
            phoneNumber.Text = details.PhoneNumber;
            email.Text = details.Email;
            Image.Source = details.ImagePath;
           
            Title = "Employee Details";
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        public void btnCall_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(phoneNumber.Text))
            {
                Call(phoneNumber.Text);
            }
        }
         
        public async void btnSms_Clicked(object sender, EventArgs e)
        {
            await Xamarin.Essentials.Sms.ComposeAsync(new SmsMessage("", phoneNumber.Text));


        }
       

        public void btnEmail_Clicked(object sender, EventArgs e)
        {
            if (email.Text != "")
                Email.ComposeAsync("", "", email.Text);
            else DisplayAlert("Alert", "Employee does not have an email","OK");
        }

        public async void Call(string number)
        {            
            try
            {
                PhoneDialer.Open(number);
            }
            catch (NullReferenceException e)
            {
                await DisplayAlert("Alert", e.Message, "Ok");
            }
            catch (ArgumentNullException anEx)
            {
                await DisplayAlert("Alert", anEx.Message, "Ok");
            }
            catch (FeatureNotSupportedException ex)
            {
                await DisplayAlert("Alert", ex.Message, "Ok");
                // Phone Dialer is not supported on this device.  
            }
            catch (Exception ex)
            {
                await DisplayAlert("Alert", ex.Message, "Ok");
            }
        }

       
    }
}