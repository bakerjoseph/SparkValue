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
        public SettingsViewModel()
        {
            _username = "username";
            _currentSettingViewModel = new SettingsGeneralViewModel();


        }
    }
}
