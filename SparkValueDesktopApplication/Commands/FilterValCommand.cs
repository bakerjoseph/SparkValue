using SparkValueDesktopApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

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
            if (parameter != null && parameter is Canvas)
            {
                Canvas breadboard = parameter as Canvas;

                // Get all elements that are of type Image
                List<Image> components = new List<Image>();
                foreach (var item in breadboard.Children)
                {
                    if (item is Image image) components.Add(image);
                }

                // For each Image, if the tooltip is open, close it, otherwise open it
                foreach (Image image in components)
                {
                    ((ToolTip)image.ToolTip).Placement = PlacementMode.Bottom;
                    ((ToolTip)image.ToolTip).PlacementTarget = image;
                    ((ToolTip)image.ToolTip).IsOpen = !((ToolTip)image.ToolTip).IsOpen;
                }
            }
        }
    }
}
