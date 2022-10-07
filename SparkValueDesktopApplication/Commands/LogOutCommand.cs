using SparkValueDesktopApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueDesktopApplication.Commands
{
    public class LogOutCommand : CommandBase
    {
        private readonly NavigationService _signInViewNavigationService;

        public LogOutCommand(NavigationService signInViewNavigationService)
        {
            _signInViewNavigationService = signInViewNavigationService;
        }

        public override void Execute(object? parameter)
        {
            // Sign out logic goes here!

            _signInViewNavigationService.Navigate();
        }
    }
}
