using SparkValueBackend.Commands;
using SparkValueBackend.Services;
using SparkValueBackend.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace SparkValueBackend.ViewModels
{
    public class ResetPasswordViewModel : ViewModelBase
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

        private string _passwordVerification;
        public string PasswordVerification
        {
            get 
            { 
                return _passwordVerification; 
            }
            set
            {
                _passwordVerification = value;
                OnPropertyChanged(nameof(PasswordVerification));
            }
        }

        private SecureString _newPassword;
        public SecureString NewPassword
        {
            get
            {
                return _newPassword;
            }
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
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

        /// <summary>
        /// Used in conjunction with PasswordChangeView.xaml
        /// </summary>
        public ResetPasswordViewModel(UserStore userStore, NavigationService userSettingsViewNavigationService)
        {
            // Use the email from the logged in user
            _username = (userStore.LoggedInUser != null) ? userStore.LoggedInUser.Username : string.Empty;
            MessageBox.Show("A email was sent to the address in your account. Use the code in that email to continue on the next screen");

            CancelCommand = new NavigateCommand(userSettingsViewNavigationService);
            ResetPasswordCommand = new ChangePasswordCommand(this, userSettingsViewNavigationService, userStore);
        }

        public ResetPasswordViewModel(UserStore userStore, NavigationService userSettingsViewNavigationService, string username, string email)
        {
            // Use the email from the previous screen
            _username = username;
            MessageBox.Show("A email was sent to the address in your account. Use the code in that email to continue on the next screen");

            CancelCommand = new NavigateCommand(userSettingsViewNavigationService);
            ResetPasswordCommand = new ChangePasswordCommand(this, userSettingsViewNavigationService, userStore);
        }
    }
}
