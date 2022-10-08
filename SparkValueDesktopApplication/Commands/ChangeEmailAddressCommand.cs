using SparkValueDesktopApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueDesktopApplication.Commands
{
    public class ChangeEmailAddressCommand : CommandBase
    {
        private readonly NavigationService _userSettingsViewNavigationService;

        public ChangeEmailAddressCommand(NavigationService userSettingsViewNavigationService)
        {
            _userSettingsViewNavigationService = userSettingsViewNavigationService;
        }

        public override void Execute(object? parameter)
        {
            // Edit a user's email address here!!

            _userSettingsViewNavigationService.Navigate();
        }
    }
}
