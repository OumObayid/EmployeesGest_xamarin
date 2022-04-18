using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmployeeGest.MasterPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPageDetail : ContentPage
    {
        public MasterPageDetail()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            PopulateEmployeeList();
        }
        public void PopulateEmployeeList()
        {
            EmployeeList.ItemsSource = null;
            EmployeeList.ItemsSource = DependencyService.Get<ISQLite>().GetEmployees();
        }

        private void AddEmployee(object sender, EventArgs e)
        {
            _ = Navigation.PushAsync(new AddEmployeePage(), true);
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Employee details = e.SelectedItem as Employee;
            if (details != null)
            {
                _ = Navigation.PushAsync(new ViewEmployeePage(details), true);
            }
        }


        private void TapGestureRecognizer_Tapped_Edit(object sender, EventArgs e)
        {


            TappedEventArgs tappedEventArgs = (TappedEventArgs)e;
            Employee details = DependencyService.Get<ISQLite>().GetEmployees().Where(emp => emp.Id == (int)tappedEventArgs.Parameter).FirstOrDefault();
            _ = Navigation.PushAsync(new EditEmployeePage(details), true);

        }
        private async void TapGestureRecognizer_Tapped_Remove(object sender, EventArgs e)
        {
            bool res = await DisplayAlert("Message", "Do you want to delete employee?", "Ok", "Cancel");
            if (res)
            {
                TappedEventArgs tappedEventArgs = (TappedEventArgs)e;
                Employee employee = DependencyService.Get<ISQLite>().GetEmployees().Where(emp => emp.Id == (int)tappedEventArgs.Parameter).FirstOrDefault();

                DependencyService.Get<ISQLite>().DeleteEmployee(employee.Id);
                PopulateEmployeeList();
            }
        }




        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            //thats all you need to make a search  

            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                EmployeeList.ItemsSource = DependencyService.Get<ISQLite>().GetEmployees();
            }

            else
            {
                EmployeeList.ItemsSource = DependencyService.Get<ISQLite>().GetEmployees().Where(x => x.Name.ToLower().Contains(e.NewTextValue.ToLower()) || x.PhoneNumber.ToLower().Contains(e.NewTextValue.ToLower()));
            }
        }

    }
}