using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EmployeeGest.MasterPage
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPageFlyout : ContentPage
    {
        public ListView ListView;

        public MasterPageFlyout()
        {
            InitializeComponent();

            BindingContext = new MasterPageFlyoutViewModel();
            ListView = MenuItemsListView;
        }

        class MasterPageFlyoutViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterPageFlyoutMenuItem> MenuItems { get; set; }

            public MasterPageFlyoutViewModel()
            {              
                MenuItems = new ObservableCollection<MasterPageFlyoutMenuItem>(new[]
                {
                    // to add an other menu
                    new MasterPageFlyoutMenuItem { Id = 0, Icon="employee", Title = "Employees",TargetType=typeof(MasterPageDetail) },
                    new MasterPageFlyoutMenuItem { Id = 1, Icon="email", Title = "Contact All", TargetType = typeof(Contact) },
                    new MasterPageFlyoutMenuItem { Id = 2, Icon="sms", Title = "Sms All", TargetType = typeof(Sms) },
                    new MasterPageFlyoutMenuItem { Id = 3, Icon="share",  Title = "Share",  },
                    new MasterPageFlyoutMenuItem { Id = 4, Icon="about", Title = "About" ,TargetType=typeof(About) },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion

           
        }
    }
}