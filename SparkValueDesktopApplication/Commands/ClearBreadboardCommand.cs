using SparkValueDesktopApplication.Models;
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
            if (parameter != null && parameter is Canvas)
            {
                Canvas breadboard = parameter as Canvas;
                if (_breadboard.PlacedComponents.Any()) _breadboard.PlacedComponents = Enumerable.Empty<ComponentViewModel>();
                if (_breadboard.PlacedWires.Any()) _breadboard.PlacedWires = Enumerable.Empty<WireModel>();
                breadboard.Children.RemoveRange(7, breadboard.Children.Count - 7);
            }
        }
    }
}
