using SparkValueDesktopApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

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
            if (parameter != null && parameter is Canvas)
            {
                Canvas breadboard = parameter as Canvas;

                // Get all elements that are of type Line
                List<Line> wires = new List<Line>();
                foreach (var item in breadboard.Children)
                {
                    if (item is Line line) wires.Add(line);
                }

                // For each Line, toggle it's visibility
                foreach (Line wire in wires)
                {
                    wire.Visibility = (wire.Visibility == Visibility.Visible) ? Visibility.Hidden : Visibility.Visible;
                }
            }
        }
    }
}
