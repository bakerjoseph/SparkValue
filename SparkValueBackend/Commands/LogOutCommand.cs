using SparkValueBackend.Services;
using SparkValueBackend.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueBackend.Commands
{
    public class LogOutCommand : CommandBase
    {
        private readonly NavigationService _signInViewNavigationService;

        private readonly UserStore _userStore;

        public LogOutCommand(NavigationService signInViewNavigationService, UserStore userStore)
        {
            _signInViewNavigationService = signInViewNavigationService;

            _userStore = userStore;
        }

        public override void Execute(object? parameter)
        {
            // Sign out the current logged in user
            _userStore.LoggedInUser = new Models.UserAccountModel();

            _signInViewNavigationService.Navigate();
        }
    }
}
