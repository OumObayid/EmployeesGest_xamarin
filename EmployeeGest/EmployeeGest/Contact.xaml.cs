using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmployeeGest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Contact : ContentPage
    {
        public Contact()
        {
            InitializeComponent();
        }

        private async void btnSend_Clicked(object sender, EventArgs e)
        {
            try
            {

                List<string> EmailList = new List<string>();
                List<Employee> Employees = DependencyService.Get<ISQLite>().GetEmployees();


                foreach (Employee employee in Employees)
                {
                    if (employee.Email != "")
                    EmailList.Add(employee.Email);
                }

                if (EmailList != null)
                {
                    await SendEmail(txtSubject.Text, txtBody.Text, EmailList);
                   
                    txtBody.Text = "";
                    txtSubject.Text = "";
                }
                else await DisplayAlert("Alert", "mailList is empty", "OK");

               
                 
            }
            catch (Exception ex)
            {
               await DisplayAlert("Faild", ex.Message, "OK");
            }
        }

        public async Task SendEmail(string subject, string body, List<string> recipients)
        {
            try
            {
                var message = new EmailMessage
                {
                    Subject = subject,
                    Body = body,
                    To = recipients,
                    //Cc = ccRecipients,
                    //Bcc = bccRecipients
                };
                await Email.ComposeAsync(message);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
               await DisplayAlert("Faild", fbsEx.Message, "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Faild", ex.Message, "OK");
            }
        }
    }
}