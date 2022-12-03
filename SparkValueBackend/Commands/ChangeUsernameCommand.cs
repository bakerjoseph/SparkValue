using SparkValueBackend.Models;
using SparkValueBackend.Services;
using SparkValueBackend.Stores;
using SparkValueBackend.ViewModels;
using System.ComponentModel;
using System.Diagnostics;

namespace SparkValueBackend.Commands
{
    public class ChangeUsernameCommand : AsyncCommandBase
    {
        private readonly UserAccountModel _userAccount;

        private readonly ResetUsernameViewModel _resetUsernameViewModel;

        private readonly NavigationService _userSettingsViewNavigationService;

        private readonly UserStore _userStore;

        public ChangeUsernameCommand(ResetUsernameViewModel resetUsernameViewModel, NavigationService userSettingsViewNavigationService, UserStore userStore)
        {
            _userAccount = userStore.LoggedInUser;

            _resetUsernameViewModel = resetUsernameViewModel;
            resetUsernameViewModel.PropertyChanged += OnViewModelPropertyChanged;

            _userSettingsViewNavigationService = userSettingsViewNavigationService;

            _userStore = userStore;
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_resetUsernameViewModel.Username) && base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            try
            {
                // Change a user's username
                await _userStore.UpdateUsersUsername(_userAccount, _resetUsernameViewModel.Username);

                _userSettingsViewNavigationService.Navigate();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex, "Change Username");
                _resetUsernameViewModel.ErrorText = "Problem occured while trying to change your email address, please try again later.";
            }
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_resetUsernameViewModel.Username))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
