using SparkValueBackend.Models;
using SparkValueBackend.Services;
using SparkValueBackend.Stores;
using SparkValueBackend.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueBackend.Commands
{
    public class SignInCommand : CommandBase
    {
        private readonly NavigationService _signInViewNavigationService;
        private readonly SecurityService _securityService;

        private readonly SignInViewModel _signInViewModel;

        private readonly UserStore _userStore;

        public SignInCommand(SignInViewModel signInVM, UserStore userStore, NavigationService signInViewNavigationService, SecurityService security)
        {
            _signInViewNavigationService = signInViewNavigationService;
            _securityService = security;

            _signInViewModel = signInVM;
            signInVM.PropertyChanged += OnViewModelPropertyChanged; 

            _userStore = userStore;
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_signInViewModel.Username) &&
                _signInViewModel.SecurePassword != null &&
                _signInViewModel.SecurePassword.Length > 0 &&
                base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            try
            {
                List<UserAccountModel> users = new List<UserAccountModel>(_userStore.Users);

                // Do we have a user with a matching username?
                UserAccountModel? targetUser = users.FirstOrDefault(x => x.Username == _signInViewModel.Username);
                if (targetUser == null)
                {
                    _signInViewModel.ErrorText = "No user found with that username.";
                    return;
                }

                // From the supplied username and secure password can we log in?
                if (_securityService.PasswordMatch(_signInViewModel.SecurePassword, targetUser.SaltValue, targetUser.Password))
                {
                    // Log in
                    _userStore.LoggedInUser = targetUser;
                    _signInViewNavigationService.Navigate();
                }
                else
                {
                    _signInViewModel.ErrorText = "Incorrect password or ussername.";
                }
                
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex, "Log In");
                _signInViewModel.ErrorText = "Problem occured while trying to log in, please try again later.";
            }
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SignInViewModel.Username) || e.PropertyName == nameof(SignInViewModel.SecurePassword))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
