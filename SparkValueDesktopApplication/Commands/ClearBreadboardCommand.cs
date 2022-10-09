using SparkValueDesktopApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueDesktopApplication.Commands
{
    public class ClearBreadboardCommand : CommandBase
    {
        private readonly BreadboardViewModel _breadboard;

        public ClearBreadboardCommand(BreadboardViewModel breadboard)
        {
            _breadboard = breadboard;
        }

        public override void Execute(object? parameter)
        {
            // Clear the canvas and all components on it
            throw new NotImplementedException();
        }
    }
}
