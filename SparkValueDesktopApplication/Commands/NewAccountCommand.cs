using SparkValueDesktopApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueDesktopApplication.Commands
{
    public class NewAccountCommand : CommandBase
    {
        private readonly NavigationService _signInViewNavigationService;

        public NewAccountCommand(NavigationService signInViewNavigationService)
        {
            _signInViewNavigationService = signInViewNavigationService;
        }

        public override void Execute(object? parameter)
        {
            // Create account logic goes here!

            _signInViewNavigationService.Navigate();
        }
    }
}
