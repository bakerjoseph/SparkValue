using SparkValueBackend.Models;
using SparkValueBackend.Services;
using SparkValueBackend.Stores;
using SparkValueBackend.ViewModels;
using System.ComponentModel;
using System.Diagnostics;
using System.Security;

namespace SparkValueBackend.Commands
{
    public class ChangePasswordCommand : AsyncCommandBase
    {
        private readonly UserAccountModel _userAccount;

        private readonly ResetPasswordViewModel _resetPasswordViewModel;
        
        private readonly NavigationService _userSettingsViewNavigationService;
        private readonly SecurityService _securityService;

        private readonly UserStore _userStore;
        private readonly EmailStatusStore _emailStatusStore;

        private readonly string _verificationString;

        public ChangePasswordCommand(ResetPasswordViewModel resetPasswordViewModel, string verifyString, NavigationService userSettingsViewNavigationService, SecurityService securityService, UserStore userStore, EmailStatusStore emailStatusStore, UserAccountModel? user)
        {
            _userAccount = (user == null) ? userStore.LoggedInUser : user ;
            
            _resetPasswordViewModel = resetPasswordViewModel;
            resetPasswordViewModel.PropertyChanged += OnViewModelPropertyChanged;

            _userSettingsViewNavigationService = userSettingsViewNavigationService;
            _securityService = securityService;

            _userStore = userStore;
            _emailStatusStore = emailStatusStore;

            _verificationString = verifyString;
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_resetPasswordViewModel.PasswordVerification) &&
                _resetPasswordViewModel.NewPassword != null &&
                _resetPasswordViewModel.NewPassword.Length > 0 &&
                _emailStatusStore.Status &&
                base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                // Does the verification string match the password verification?
                if (_verificationString != _resetPasswordViewModel.PasswordVerification)
                {
                    _resetPasswordViewModel.ErrorText = "The reset verification string is incorrect, please check the email again.";
                    return;
                }

                // Change a user's password
                await _userStore.UpdateUsersPassword(_userAccount, _securityService.ProtectPassword(_resetPasswordViewModel.NewPassword));

                _userSettingsViewNavigationService.Navigate();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex, "Password Change");
                _resetPasswordViewModel.ErrorText = "Problem occured while trying to change your password, please try again later.";
            }
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ResetPasswordViewModel.PasswordVerification) || e.PropertyName == nameof(ResetPasswordViewModel.NewPassword))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
