using SparkValueBackend.Commands;
using SparkValueBackend.Models;
using SparkValueBackend.Services;
using SparkValueBackend.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SparkValueBackend.ViewModels
{
    public class UsernameEmailRequestViewModel : ViewModelBase
    {
        private string _username;
        public string Username
        {
            get { return _username; }
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
        public ICommand ResetPasswordCommand { get; }

        private readonly UserStore _userStore;
        private readonly EmailStatusStore _emailStatusStore;
        private readonly EmailService _emailService;
        private readonly SecurityService _securityService;
        private readonly NavigationService _logInNavigationService;

        /// <summary>
        /// Used in conjunction with UsernameEmailRequestView.xaml
        /// </summary>
        public UsernameEmailRequestViewModel(NavigationStore navigationStore, EmailStatusStore emailStatusStore, UserStore userStore, EmailService emailService, NavigationService logInViewNavigationService, SecurityService securityService)
        {
            _userStore = userStore;
            _emailStatusStore = emailStatusStore;
            _emailService = emailService;
            _logInNavigationService = logInViewNavigationService;
            _securityService = securityService;

            CancelCommand = new NavigateCommand(logInViewNavigationService);
            ResetPasswordCommand = new UsernameEmailRequestVerifyCommand(this, userStore, new NavigationService(navigationStore, CreateResetPasswordViewModel));
        }

        public ResetPasswordViewModel CreateResetPasswordViewModel()
        {
            UserAccountModel user = _userStore.GetUserByUsername(_username);
            return new ResetPasswordViewModel(_userStore, _emailStatusStore, _emailService, _logInNavigationService, _securityService, user);
        }
    }
}
