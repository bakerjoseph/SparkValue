using SparkValueBackend.Models;
using SparkValueBackend.Services;
using SparkValueBackend.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueBackend.Commands
{
    public class WipeAccountCommand : AsyncCommandBase
    {
        private readonly NavigationService _dashboardViewNavigationService;

        private readonly UserStore _userStore;

        public WipeAccountCommand(NavigationService dashboardViewNavigationService, UserStore userStore)
        {
            _dashboardViewNavigationService = dashboardViewNavigationService;

            _userStore = userStore; 
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            // Wipe an account's progress on all units and lessons
            UserAccountModel user = _userStore.LoggedInUser;
            await _userStore.ResetAllUserProgress(user);

            _dashboardViewNavigationService.Navigate();
        }
    }
}
