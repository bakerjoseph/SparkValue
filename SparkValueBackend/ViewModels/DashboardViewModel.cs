using SparkValueBackend.Commands;
using SparkValueBackend.Models;
using SparkValueBackend.Services;
using SparkValueBackend.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SparkValueBackend.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        private string _username;
        public string Username
        {
            get 
            { 
                return _username; 
            }
            set 
            { 
                _username = value; 
                OnPropertyChanged(nameof(Username));
            }
        }
        private readonly ObservableCollection<UnitViewModel> _units;

        public ICommand BreadboardNavigateCommand { get; }
        public ICommand SettingsNavigateCommand { get; }
        public ICommand LogOutCommand { get; }
        public IEnumerable<UnitViewModel> Units => _units;

        /// <summary>
        /// Used in conjunction with DashboardView.xaml
        /// </summary>
        public DashboardViewModel(
            NavigationStore navigationStore,
            UnitStore unitStore,
            UserStore userStore,
            NavigationService breadboardViewNavigationService, 
            NavigationService userSettingsViewNavigationService,
            NavigationService signInViewNavigationService,
            NavigationService dashboardViewNavigationService)
        {
            Username = userStore.LoggedInUser.Username;

            _units = new ObservableCollection<UnitViewModel>();
            foreach (UnitModel unit in unitStore.Units)
            {
                _units.Add(new UnitViewModel(navigationStore, dashboardViewNavigationService, userSettingsViewNavigationService, unit, userStore.LoggedInUser));
            }

            BreadboardNavigateCommand = new NavigateCommand(breadboardViewNavigationService);
            SettingsNavigateCommand = new NavigateCommand(userSettingsViewNavigationService);
            LogOutCommand = new LogOutCommand(signInViewNavigationService, userStore);
        }
    }
}
