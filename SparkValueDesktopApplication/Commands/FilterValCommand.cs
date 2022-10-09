using SparkValueDesktopApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueDesktopApplication.Commands
{
    public class FilterValCommand : CommandBase
    {
        private readonly BreadboardViewModel _breadboard;

        public FilterValCommand(BreadboardViewModel breadboard)
        {
            _breadboard = breadboard;
        }

        public override void Execute(object? parameter)
        {
            // Show all component values on breadboard
            throw new NotImplementedException();
        }
    }
}
