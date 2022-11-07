using SparkValueBackend.Commands;
using SparkValueBackend.Models;
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
        /// Constructor for the settings page
        /// </summary>
        public ResetPasswordViewModel(UserStore userStore, EmailService emailService, NavigationService userSettingsViewNavigationService, SecurityService securityService)
        {
            string verificationString = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 10);

            // Use the email from the logged in user
            SendResetEmail(emailService, userStore.LoggedInUser, verificationString);
            MessageBox.Show("A email was sent to the address in your account. Use the code in that email to continue on the next screen");

            CancelCommand = new NavigateCommand(userSettingsViewNavigationService);
            ResetPasswordCommand = new ChangePasswordCommand(this, verificationString, userSettingsViewNavigationService, securityService, userStore, null);
        }

        /// <summary>
        /// Used in conjunction with PasswordChangeView.xaml
        /// Constructor for the log in page
        /// </summary>
        public ResetPasswordViewModel(UserStore userStore, EmailService emailService, NavigationService logInViewNavigationService, SecurityService securityService, UserAccountModel user)
        {
            string verificationString = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 10);

            // Use the details from the previous screen
            SendResetEmail(emailService, user, verificationString);
            MessageBox.Show("A email was sent to the address in your account. Use the code in that email to continue on the next screen");

            CancelCommand = new NavigateCommand(logInViewNavigationService);
            ResetPasswordCommand = new ChangePasswordCommand(this, verificationString, logInViewNavigationService, securityService, userStore, user);
        }

        private async void SendResetEmail(EmailService emailService, UserAccountModel user, string verificationString)
        {
            await emailService.SendPasswordResetEmail(user, verificationString);
        }
    }
}
