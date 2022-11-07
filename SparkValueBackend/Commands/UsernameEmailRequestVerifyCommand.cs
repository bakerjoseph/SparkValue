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
    public class UsernameEmailRequestVerifyCommand : CommandBase
    {
        private readonly NavigationService _passwordChangeNavigationService;

        private readonly UserStore _userStore;

        private readonly UsernameEmailRequestViewModel _usernameEmailRequestViewModel;

        public UsernameEmailRequestVerifyCommand(UsernameEmailRequestViewModel usernameEmailRequestViewModel, UserStore userStore, NavigationService passwordChangeNavigationService)
        {
            _passwordChangeNavigationService = passwordChangeNavigationService;

            _userStore = userStore;

            _usernameEmailRequestViewModel = usernameEmailRequestViewModel;
            usernameEmailRequestViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_usernameEmailRequestViewModel.Username) && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            try
            {
                List<UserAccountModel> users = new List<UserAccountModel>(_userStore.Users);

                // Do we have a user with a matching username?
                UserAccountModel? targetUser = users.FirstOrDefault(x => x.Username == _usernameEmailRequestViewModel.Username);
                if (targetUser == null)
                {
                    // No account found
                    _usernameEmailRequestViewModel.ErrorText = "No user found with that username.";
                    return;
                }

                // Navigate to the next window, which handles actually reseting the password
                _passwordChangeNavigationService.Navigate();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex, "Get User");
                _usernameEmailRequestViewModel.ErrorText = "Problem occured while trying to look up your username, please try again later.";
            }
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(UsernameEmailRequestViewModel.Username))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
