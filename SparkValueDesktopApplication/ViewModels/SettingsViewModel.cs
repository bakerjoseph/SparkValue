using SparkValueDesktopApplication.Commands;
using SparkValueDesktopApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SparkValueDesktopApplication.ViewModels
{
    public class SettingsViewModel : ViewModelBase
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

        private ViewModelBase _currentSettingViewModel;
        public ViewModelBase CurrentSettingViewModel
        {
            get 
            { 
                return _currentSettingViewModel; 
            }
            set 
            { 
                _currentSettingViewModel = value; 
                OnPropertyChanged(nameof(CurrentSettingViewModel)); 
            }
        }

        public ICommand SwitchToGeneralCommand { get; }
        public ICommand SwitchToAccountCommand { get; }
        public ICommand MenuNavigateCommand { get; }

        /// <summary>
        /// Used in conjunction with UserSettingsView.xaml
        /// </summary>
        public SettingsViewModel(NavigationService dashboardViewNavigationService, List<NavigationService> generalNavigationServices, List<NavigationService> accountNavigationServices, string username, string currentSettingViewModel)
        {
            _username = username;
            _currentSettingViewModel = (currentSettingViewModel.Equals("General"))? new SettingsGeneralViewModel(generalNavigationServices) : 
                                       (currentSettingViewModel.Equals("Account")) ? new SettingsAccountViewModel(accountNavigationServices) : throw new Exception($"{currentSettingViewModel} is not a recognized settings view model");

            MenuNavigateCommand = new NavigateCommand(dashboardViewNavigationService);
            SwitchToGeneralCommand = new SwitchSettingsCommand("General", this, generalNavigationServices);
            SwitchToAccountCommand = new SwitchSettingsCommand("Account", this, accountNavigationServices);
        }
    }
}
