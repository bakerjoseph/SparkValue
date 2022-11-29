using SparkValueBackend.Services;
using SparkValueBackend.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueBackend.Commands
{
    public class NavigateAwayFromBreadboardCommand : CommandBase
    {
        private readonly UserStore _userStore;

        private readonly NavigationService _logInNavigationService;
        private readonly NavigationService _dashboardNavigationService;

        public NavigateAwayFromBreadboardCommand(UserStore userStore, NavigationService logInNavigationService, NavigationService dashboardNavigationService)
        {
            _userStore = userStore;
            _logInNavigationService = logInNavigationService;
            _dashboardNavigationService = dashboardNavigationService;
        }

        public override void Execute(object? parameter)
        {
            if (_userStore.LoggedInUser != null && !string.IsNullOrEmpty(_userStore.LoggedInUser.Username)) _dashboardNavigationService.Navigate();
            else _logInNavigationService.Navigate();
        }
    }
}
