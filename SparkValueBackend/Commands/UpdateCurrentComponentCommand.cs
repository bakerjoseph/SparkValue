using SparkValueBackend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SparkValueBackend.Commands
{
    public class UpdateCurrentComponentCommand : CommandBase
    {
        private readonly BreadboardViewModel _breadboard;

        public UpdateCurrentComponentCommand(BreadboardViewModel breadboard)
        {
            _breadboard = breadboard;
        }

        public override void Execute(object? parameter)
        {
            // Update the fields for the current component in the view model from the parameter component model
            if (parameter != null && parameter is Image)
            {
                Image comp = parameter as Image;
                ComponentViewModel compVM = comp.DataContext as ComponentViewModel;
                _breadboard.SelectedComponentName = compVM?.Name ?? string.Empty;
                _breadboard.SelectedComponentDescription = compVM?.Description ?? string.Empty;
            }
            else
            {
                _breadboard.SelectedComponentName = string.Empty;
                _breadboard.SelectedComponentDescription = string.Empty;
            }
        }
    }
}
