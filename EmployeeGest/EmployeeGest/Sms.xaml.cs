using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace EmployeeGest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Sms : ContentPage
    {
        public Sms()
        {
            InitializeComponent();
        }

        //to send sms to all employees
        private async void BtnSend_Clicked(object sender, EventArgs e)
        {
            try
            {
                // for stocking employees sms
                List<string> List = new List<string>();

                // select all employees
                List<Employee> Employees = DependencyService.Get<ISQLite>().GetEmployees();
                

                foreach (Employee employee in Employees)
                {
                    if (employee.PhoneNumber != "")
                        List.Add(employee.PhoneNumber);
                }


                string[] SmsList = List.ToArray();


                if (SmsList != null)
                {
                    await SendSms(txtSms.Text, SmsList);

                   
                    txtSms.Text = "";
                }
                else await DisplayAlert("Alert", " smsList is empty", "OK");



            }
            catch (Exception ex)
            {
                await DisplayAlert("Faild", ex.Message, "OK");
            }
        }

        //method send sms
        public async Task SendSms(string messageText, string[] recipients)
        {
            try
            {
                var message = new SmsMessage(messageText, recipients);
                await Xamarin.Essentials.Sms.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException ex)
            {
                await DisplayAlert("Faild", ex.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Faild", ex.Message, "OK");
            }
        }


    }
}