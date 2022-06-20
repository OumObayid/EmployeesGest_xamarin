using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;


namespace EmployeeGest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewEmployeePage : ContentPage
    {
        // to receive the employer information sent by the parent page
        Employee employeeDetails;

        // "Employee details" as parametre to receive the employer information sent by the parent page
        public ViewEmployeePage(Employee details)
        {
            InitializeComponent();
            if (details != null)
            {
                employeeDetails = details;
                PopulateDetails(employeeDetails);
            }
        }

        //to display employee information in the fields
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

        //To call
        public void btnCall_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(phoneNumber.Text))
            {
                //apply to call method
                Call(phoneNumber.Text);
            }
        }
         
        //to send sms
        public async void btnSms_Clicked(object sender, EventArgs e)
        {
            if (phoneNumber.Text != "")
            {
                await Xamarin.Essentials.Sms.ComposeAsync(new SmsMessage("", phoneNumber.Text));
            }
            else
            {
                _ = DisplayAlert("Alert", "Employee does not have an phoneNumber", "OK");
            }
        }

       // to send email
        public void BtnEmail_Clicked(object sender, EventArgs e)
        {
            if (email.Text != "")
            {
                _ = Email.ComposeAsync("", "", email.Text);
            }
            else
            {
                _ = DisplayAlert("Alert", "Employee does not have an email", "OK");
            }
        }

        //method call
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