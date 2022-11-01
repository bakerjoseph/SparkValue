using SparkValueBackend.Models;
using SparkValueBackend.Services;
using SparkValueBackend.Stores;
using SparkValueBackend.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SparkValueBackend.Commands
{
    public class NewAccountCommand : AsyncCommandBase
    {
        private readonly NavigationService _signInViewNavigationService;
        private readonly SecurityService _securityService;

        private readonly NewAccountViewModel _newAccountViewModel;

        private readonly UserStore _userStore;

        public NewAccountCommand(NewAccountViewModel accountVM, UserStore userStore, NavigationService signInViewNavigationService, SecurityService security)
        {
            _signInViewNavigationService = signInViewNavigationService;
            _securityService = security;

            _newAccountViewModel = accountVM;
            accountVM.PropertyChanged += OnViewModelPropertyChanged;

            _userStore = userStore;
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_newAccountViewModel.Username) && 
                !string.IsNullOrEmpty(_newAccountViewModel.EmailAddress) && 
                _newAccountViewModel.SecurePassword != null && 
                _newAccountViewModel.SecurePassword.Length > 0 && 
                base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            // Create account with the password paramter and the passed in view model values
            (string salt, string hashed) outcome = _securityService.ProtectPassword(_newAccountViewModel.SecurePassword);
            UserAccountModel newUser = new UserAccountModel(_newAccountViewModel.Username, outcome.hashed, _newAccountViewModel.EmailAddress, outcome.salt);
            await _userStore.CreateUser(newUser);

            _signInViewNavigationService.Navigate();
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(NewAccountViewModel.Username) || e.PropertyName == nameof(NewAccountViewModel.EmailAddress) || e.PropertyName == nameof(NewAccountViewModel.SecurePassword))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
