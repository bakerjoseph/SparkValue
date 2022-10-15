using SparkValueDesktopApplication.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SparkValueDesktopApplication.Commands
{
    public class UpdateBreadboardWireCommand : CommandBase
    {
        public override void Execute(object? parameter)
        {
            if (parameter != null && parameter is WireModel)
            {
                WireModel wire = (WireModel)parameter;
                MessageBox.Show($"Wire was drawn from {wire.StartPositionToString()} to {wire.EndPositionToString()}");
            }
        }
    }
}
