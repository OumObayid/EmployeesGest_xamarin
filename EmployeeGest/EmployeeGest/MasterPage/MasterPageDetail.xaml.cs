using System;
using System.Linq;
using Xamarin.Essentials;
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

        // To display the list of employers in the listView (EmployeeList)
        public void PopulateEmployeeList()
        {
            EmployeeList.ItemsSource = null;
            EmployeeList.ItemsSource = DependencyService.Get<ISQLite>().GetEmployees();
        }

        // navigation by click() to the page (AddEmployeePage) for adding an employee
        private void AddEmployee(object sender, EventArgs e)
        {
            _ = Navigation.PushAsync(new AddEmployeePage(), true);
        }

        // navigation by tap() on the list item to the page (ViewEmployeePage) for displaying employee details
        //  the information of the selected item is sent to the page ViewEmployeePage(details)
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

            Employee details = e.SelectedItem as Employee;


            if (details != null)
            {
                _ = Navigation.PushAsync(new ViewEmployeePage(details), true);
            }
        }

        // navigation by tap() on the icon update to the page (EditEmployeePage) for updating employee details
        //  the information of the selected item is sent to the page EditEmployeePage(details)
        private void TapGestureRecognizer_Tapped_Edit(object sender, EventArgs e)
        {
            TappedEventArgs tappedEventArgs = (TappedEventArgs)e;
            Employee details = DependencyService.Get<ISQLite>().GetEmployees().Where(emp => emp.Id == (int)tappedEventArgs.Parameter).FirstOrDefault();
            _ = Navigation.PushAsync(new EditEmployeePage(details), true);
        }

        // to delete employee after confirmation
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



        // search barre
        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {          
            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                EmployeeList.ItemsSource = DependencyService.Get<ISQLite>().GetEmployees();
            }
            else
            {
                EmployeeList.ItemsSource = DependencyService.Get<ISQLite>().GetEmployees().Where(x => x.Name.ToLower().Contains(e.NewTextValue.ToLower()) || x.PhoneNumber.ToLower().Contains(e.NewTextValue.ToLower()));
            }
        }

        // code for sharing
        private void Share_Clicked(object sender, EventArgs e)
        {
            Share.RequestAsync(new ShareTextRequest
            {
                Text = "I want to share this application with you, Try it please",
                Title = "Share App"
            });
        }

        //code for exiting application
        private void Exit_Clicked(object sender, EventArgs e)
        {
            Environment.Exit(0);

        }

    }
}