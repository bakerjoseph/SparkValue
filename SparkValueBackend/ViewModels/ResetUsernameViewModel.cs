using SparkValueBackend.Commands;
using SparkValueBackend.Services;
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

        public ICommand CancelCommand { get; }
        public ICommand ResetUsernameCommand { get; }

        /// <summary>
        /// Used in conjunction with UsernameChangeView.xaml
        /// </summary>
        public ResetUsernameViewModel(NavigationService userSettingsViewNavigationService)
        {
            _username = string.Empty;

            CancelCommand = new NavigateCommand(userSettingsViewNavigationService);
            ResetUsernameCommand = new ChangeUsernameCommand(userSettingsViewNavigationService);
        }
    }
}
