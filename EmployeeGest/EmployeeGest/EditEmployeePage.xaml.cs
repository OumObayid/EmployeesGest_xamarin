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
    public partial class EditEmployeePage : ContentPage
    {
        // to stock photo path from mobile storage
        public string path;

        // to receive the employer information sent by the parent page
        Employee employeeDetails;

        // "Employee details" as parametre to receive the employer information sent by the parent page
        public EditEmployeePage(Employee details)
        {
            InitializeComponent();

            path = "";
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
            Photo.Source = details.ImagePath;
            path = details.ImagePath;
            saveBtn.Text = "UPDATE";
            TakePhotoBtn.Text = "CHANGE PHOTO ";
            Title = "Edit Employee";
        }

        // to select a photo from mobile storage
        // for this we must install the paquage Plugin.Media;
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
            Photo.Source = ImageSource.FromStream(() =>
            {
                Stream stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }


        private void UpdateEmployee(object sender, EventArgs e)
        {
            if (name.Text == "" || phoneNumber.Text == "")
            { _ = DisplayAlert("Message", "Name and phone required", "Ok"); }
            else
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
                    //navigate back
                    _ = Navigation.PopAsync();
                }
                else
                {
                    _ = DisplayAlert("Message", "Data Failed To Update", "Ok");
                }                
            }
        }
    }
}