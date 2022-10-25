using SparkValueBackend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueBackend.Commands
{
    public class WipeAccountCommand : CommandBase
    {
        private readonly NavigationService _dashboardViewNavigationService;

        public WipeAccountCommand(NavigationService dashboardViewNavigationService)
        {
            _dashboardViewNavigationService = dashboardViewNavigationService;
        }

        public override void Execute(object? parameter)
        {
            // Wipe an account logic here!!

            _dashboardViewNavigationService.Navigate();
        }
    }
}
