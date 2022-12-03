using SparkValueBackend.Commands;
using SparkValueBackend.Services;
using SparkValueBackend.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SparkValueBackend.ViewModels
{
    public class ResetUsernameViewModel : ViewModelBase
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

        private string _errorText;
        public string ErrorText
        {
            get
            {
                return _errorText;
            }
            set
            {
                _errorText = value;
                OnPropertyChanged(nameof(ErrorText));
            }
        }

        public ICommand CancelCommand { get; }
        public ICommand ResetUsernameCommand { get; }

        /// <summary>
        /// Used in conjunction with UsernameChangeView.xaml
        /// </summary>
        public ResetUsernameViewModel(UserStore userStore, NavigationService userSettingsViewNavigationService)
        {
            _username = string.Empty;

            CancelCommand = new NavigateCommand(userSettingsViewNavigationService);
            ResetUsernameCommand = new ChangeUsernameCommand(this, userSettingsViewNavigationService, userStore);
        }
    }
}
