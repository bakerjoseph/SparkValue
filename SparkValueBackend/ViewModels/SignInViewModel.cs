using SparkValueBackend.Commands;
using SparkValueBackend.Services;
using SparkValueBackend.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SparkValueBackend.ViewModels
{
    public class SignInViewModel : ViewModelBase
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

        private SecureString _securePassword;
        public SecureString SecurePassword
        {
            get
            {
                return _securePassword;
            }
            set
            {
                _securePassword = value;
                OnPropertyChanged(nameof(SecurePassword));
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

        private bool _emailingStatus;
        public bool EmailingStatus
        {
            get { return _emailingStatus; }
            set
            {
                _emailingStatus = value;
                OnPropertyChanged(nameof(EmailingStatus));
            }
        }

        public ICommand CreateAccountCommand { get; }
        public ICommand BreadboardNavigateCommand { get; }
        public ICommand SignInCommand { get; }
        public ICommand ForgotPasswordNavigateCommand { get; }

        /// <summary>
        /// Used in conjunction with SignInView.xaml
        /// </summary>
        public SignInViewModel(UserStore userStore,
                            EmailStatusStore emailStatusStore,
                            NavigationService breadboardViewNavigationService,
                            NavigationService newAccountViewNavigationService,
                            NavigationService dashboardViewNavigationService,
                            NavigationService passwordChangeViewNavigationService,
                            SecurityService security)
        {
            _username = string.Empty;

            EmailingStatus = emailStatusStore.Status;

            BreadboardNavigateCommand = new NavigateCommand(breadboardViewNavigationService);
            CreateAccountCommand = new NavigateCommand(newAccountViewNavigationService);
            SignInCommand = new SignInCommand(this, userStore, dashboardViewNavigationService, security);
            ForgotPasswordNavigateCommand = new NavigateCommand(passwordChangeViewNavigationService);
        }
    }
}
