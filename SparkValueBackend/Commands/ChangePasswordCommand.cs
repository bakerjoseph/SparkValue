using SparkValueBackend.Services;
using SparkValueBackend.Stores;
using SparkValueBackend.ViewModels;
using System.ComponentModel;
using System.Security;

namespace SparkValueBackend.Commands
{
    public class ChangePasswordCommand : CommandBase
    {
        private readonly ResetPasswordViewModel _resetPasswordViewModel;
        
        private readonly NavigationService _userSettingsViewNavigationService;

        private readonly UserStore _userStore;

        public ChangePasswordCommand(ResetPasswordViewModel resetPasswordViewModel, NavigationService userSettingsViewNavigationService, UserStore userStore)
        {
            _resetPasswordViewModel = resetPasswordViewModel;
            resetPasswordViewModel.PropertyChanged += OnViewModelPropertyChanged;

            _userSettingsViewNavigationService = userSettingsViewNavigationService;

            _userStore = userStore;
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_resetPasswordViewModel.Username) && 
                !string.IsNullOrEmpty(_resetPasswordViewModel.PasswordVerification) &&
                _resetPasswordViewModel.NewPassword != null &&
                _resetPasswordViewModel.NewPassword.Length > 0 &&
                base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            // Change a user's password here!!

            _userSettingsViewNavigationService.Navigate();
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ResetPasswordViewModel.Username) || e.PropertyName == nameof(ResetPasswordViewModel.PasswordVerification) || e.PropertyName == nameof(ResetPasswordViewModel.NewPassword))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
