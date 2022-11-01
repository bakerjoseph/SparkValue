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
    public class NewAccountViewModel : ViewModelBase
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

        public ICommand CancelCommand { get; }
        public ICommand CreateAccountCommand { get; }

        /// <summary>
        /// Used in conjunction with NewAccountView.xaml
        /// </summary>
        public NewAccountViewModel(UserStore userStore, NavigationService signInViewNavigationService, SecurityService securityService)
        {
            _username = string.Empty;
            _emailAddress = string.Empty;

            CancelCommand = new NavigateCommand(signInViewNavigationService);
            CreateAccountCommand = new NewAccountCommand(this, userStore, signInViewNavigationService, securityService);
        }
    }
}
