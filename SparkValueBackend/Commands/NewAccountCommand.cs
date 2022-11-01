using SparkValueBackend.Models;
using SparkValueBackend.Services;
using SparkValueBackend.Stores;
using SparkValueBackend.ViewModels;
using System;
using System.Collections.Generic;
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

            _userStore = userStore;
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            if (parameter != null && parameter is PasswordBox)
            {
                // Create account with the password paramter and the passed in view model values
                (string salt, string hashed) outcome = _securityService.ProtectPassword(((PasswordBox)parameter).Password);
                UserAccountModel newUser = new UserAccountModel(_newAccountViewModel.Username, outcome.hashed, _newAccountViewModel.EmailAddress, outcome.salt);
                await _userStore.CreateUser(newUser);
            }

            _signInViewNavigationService.Navigate();
        }
    }
}
