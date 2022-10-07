using SparkValueDesktopApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueDesktopApplication.Commands
{
    public class SignInCommand : CommandBase
    {
        private readonly NavigationService _signInViewNavigationService;

        public SignInCommand(NavigationService signInViewNavigationService)
        {
            _signInViewNavigationService = signInViewNavigationService;
        }

        public override void Execute(object? parameter)
        {
            // Logic to check if valid log in goes here!

            _signInViewNavigationService.Navigate();
        }
    }
}
