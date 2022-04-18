using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        private async void btnSend_Clicked(object sender, EventArgs e)
        {
            try
            {

                List<string> List = new List<string>();
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

        public async Task SendSms(string messageText, string[] recipients)
        {
            try
            {
                var message = new SmsMessage(messageText, recipients);
                await Xamarin.Essentials.Sms.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException ex)
            {
                // Sms is not supported on this device.
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }


    }
}