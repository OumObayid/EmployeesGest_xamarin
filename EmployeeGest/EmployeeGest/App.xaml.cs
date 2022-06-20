using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using System.Threading.Tasks;

namespace EmployeeGest
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Get<ISQLite>().GetConnectionWithCreateDatabase();
         
           // MainPage = new NavigationPage(new MainPage());
            MainPage = new MasterPage.MasterPage();

            

        }

       


        protected override async void OnStart()
        {
            var result = await CrossFingerprint.Current.IsAvailableAsync();

            if (result)
            {
                var dialogConfig = new AuthenticationRequestConfiguration
                ("Login using fingerprint", "Confirm login with your fingerprint")
                {
                    FallbackTitle = "Use Password",
                    AllowAlternativeAuthentication = true,
                };

                var auth = await CrossFingerprint.Current.AuthenticateAsync(dialogConfig);
                if (auth.Authenticated)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Application.Current.MainPage = new MasterPage.MasterPage();
                    });

                }
                else
                {
                    await DisplayAlert("Authentication Failed", "Fingerprint authentication failed", "CLOSE");
                }
            }


        }

        private Task DisplayAlert(string v1, string v2, string v3)
        {
            throw new NotImplementedException();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
