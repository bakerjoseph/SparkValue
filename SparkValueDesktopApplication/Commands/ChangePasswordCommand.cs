using SparkValueDesktopApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueDesktopApplication.Commands
{
    public class ChangePasswordCommand : CommandBase
    {
        private readonly NavigationService _userSettingsViewNavigationService;

        public ChangePasswordCommand(NavigationService userSettingsViewNavigationService)
        {
            _userSettingsViewNavigationService = userSettingsViewNavigationService;
        }

        public override void Execute(object? parameter)
        {
            // Change a user's password here!!

            _userSettingsViewNavigationService.Navigate();
        }
    }
}
