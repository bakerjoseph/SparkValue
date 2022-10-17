using SparkValueDesktopApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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
            Canvas? breadboard = parameter as Canvas;
            if (breadboard != null) breadboard.Children.RemoveRange(7, breadboard.Children.Count - 7);
        }
    }
}
