using SparkValueDesktopApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueDesktopApplication.Commands
{
    public class ChangeUsernameCommand : CommandBase
    {
        private readonly NavigationService _userSettingsViewNavigationService;

        public ChangeUsernameCommand(NavigationService userSettingsViewNavigationService)
        {
            _userSettingsViewNavigationService = userSettingsViewNavigationService;
        }

        public override void Execute(object? parameter)
        {
            // Change a user's username here!!

            _userSettingsViewNavigationService.Navigate();
        }
    }
}
