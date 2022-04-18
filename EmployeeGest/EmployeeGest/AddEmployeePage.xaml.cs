using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmployeeGest
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEmployeePage : ContentPage
    {
        public string path;
        Employee employeeDetails;
        public AddEmployeePage()
        {
            InitializeComponent();

            path = "";
          
        }



        private async void BtnTakePhoto_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photo not supported", ":( Access rights are restricted.", "Very well ..");
                return;
            }
            Plugin.Media.Abstractions.MediaFile file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.MaxWidthHeight,

            });

            if (file == null)
                return;
            
            path = file.Path;
            Image.Source = ImageSource.FromStream(() =>
            {
                Stream stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }
        private void SaveEmployee(object sender, EventArgs e)
        {
            if (name.Text == "" || phoneNumber.Text == "")
            { _ = DisplayAlert("Message", "Name and phone required", "Ok"); }
            else
            {
                if (saveBtn.Text == "Save")
                {

                    Employee employee = new Employee
                    {
                        Name = name.Text,
                        Address = address.Text,
                        PhoneNumber = phoneNumber.Text,
                        Email = email.Text,
                        ImagePath = path,
                    };

                    bool res = DependencyService.Get<ISQLite>().SaveEmployee(employee);
                    if (res)
                    {
                        Navigation.PopAsync();
                    }
                    else
                    {
                        DisplayAlert("Message", "Data Failed To Save", "Ok");
                    }

                }
                if (saveBtn.Text != "Save")
                {
                    // update employee
                    employeeDetails.Name = name.Text;
                    employeeDetails.Address = address.Text;
                    employeeDetails.PhoneNumber = phoneNumber.Text;
                    employeeDetails.Email = email.Text;
                    employeeDetails.ImagePath = path;
                    bool res = DependencyService.Get<ISQLite>().UpdateEmployee(employeeDetails);
                    if (res)
                    {
                        Navigation.PopAsync();
                    }
                    else
                    {
                        DisplayAlert("Message", "Data Failed To Update", "Ok");
                    }
                }
            }
        }
    }
}