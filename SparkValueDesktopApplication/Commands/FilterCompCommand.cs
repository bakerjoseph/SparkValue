using SparkValueDesktopApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueDesktopApplication.Commands
{
    public class FilterCompCommand : CommandBase
    {
        private readonly BreadboardViewModel _breadboard;

        public FilterCompCommand(BreadboardViewModel breadboard)
        {
            _breadboard = breadboard;
        }

        public override void Execute(object? parameter)
        {
            // Show only the components on the breadboard, hide the wires/make them transparent
            throw new NotImplementedException();
        }
    }
}
