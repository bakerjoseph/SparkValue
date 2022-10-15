using SparkValueDesktopApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SparkValueDesktopApplication.Commands
{
    public class UpdateBreadboardComponentsCommand : CommandBase
    {
        public UpdateBreadboardComponentsCommand()
        {

        }

        public override void Execute(object? parameter)
        {
            if(parameter != null && parameter is ComponentViewModel)
            {
                ComponentViewModel component = (ComponentViewModel)parameter;
                MessageBox.Show(component.Name);
            }
        }
    }
}
