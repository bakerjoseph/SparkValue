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
    public class ResetEmailAddressViewModel : ViewModelBase
    {
        private string _emailAddress;
        public string EmailAddress
        {
            get 
            { 
                return _emailAddress; 
            }
            set 
            { 
                _emailAddress = value; 
                OnPropertyChanged(nameof(EmailAddress));
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
        public ICommand ResetEmailAddressCommand { get; }

        /// <summary>
        /// Used in conjunction with EmailChangeView.xaml
        /// </summary>
        public ResetEmailAddressViewModel(UserStore userStore, NavigationService userSettingsViewNavigationService)
        {
            _emailAddress = string.Empty;

            CancelCommand = new NavigateCommand(userSettingsViewNavigationService);
            ResetEmailAddressCommand = new ChangeEmailAddressCommand(this, userSettingsViewNavigationService, userStore);
        }
    }
}
