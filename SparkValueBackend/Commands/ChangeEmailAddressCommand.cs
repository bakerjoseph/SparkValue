using SparkValueBackend.Models;
using SparkValueBackend.Services;
using SparkValueBackend.Stores;
using SparkValueBackend.ViewModels;
using System.ComponentModel;
using System.Diagnostics;

namespace SparkValueBackend.Commands
{
    public class ChangeEmailAddressCommand : AsyncCommandBase
    {
        private readonly UserAccountModel _userAccount;

        private readonly ResetEmailAddressViewModel _resetEmailAddressViewModel;

        private readonly NavigationService _userSettingsViewNavigationService;

        private readonly UserStore _userStore;

        public ChangeEmailAddressCommand(ResetEmailAddressViewModel resetEmailAddressViewModel, NavigationService userSettingsViewNavigationService, UserStore userStore)
        {
            _userAccount = userStore.LoggedInUser;

            _resetEmailAddressViewModel = resetEmailAddressViewModel;
            resetEmailAddressViewModel.PropertyChanged += OnViewModelPropertyChanged;

            _userSettingsViewNavigationService = userSettingsViewNavigationService;

            _userStore = userStore;
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_resetEmailAddressViewModel.EmailAddress) && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                // Change a user's email address
                await _userStore.UpdateUsersEmailAddress(_userAccount, _resetEmailAddressViewModel.EmailAddress);

                _userSettingsViewNavigationService.Navigate();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex, "Email Change");
                _resetEmailAddressViewModel.ErrorText = "Problem occured while trying to change your email address, please try again later.";
            }
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ResetEmailAddressViewModel.EmailAddress))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
