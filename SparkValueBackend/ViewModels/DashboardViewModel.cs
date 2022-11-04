using SparkValueBackend.Commands;
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
            NavigationService breadboardViewNavigationService, 
            NavigationService userSettingsViewNavigationService,
            NavigationService signInViewNavigationService,
            NavigationService dashboardViewNavigationService,
            string username)
        {
            Username = username;
            // Is not currently working, needs to be mapped from the list of units with lessons to viewmodels for units and partial lessons
            _units = new ObservableCollection<UnitViewModel>((IEnumerable<UnitViewModel>)unitStore.Units);

            BreadboardNavigateCommand = new NavigateCommand(breadboardViewNavigationService);
            SettingsNavigateCommand = new NavigateCommand(userSettingsViewNavigationService);
            LogOutCommand = new LogOutCommand(signInViewNavigationService);
        }
    }
}
