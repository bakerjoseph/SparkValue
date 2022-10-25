using SparkValueBackend.Models.Components;
using SparkValueBackend.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SparkValueBackend.Commands
{
    public class UpdateBreadboardComponentsCommand : CommandBase
    {
        private readonly BreadboardViewModel _breadboard;

        public UpdateBreadboardComponentsCommand(BreadboardViewModel breadboard)
        {
            _breadboard = breadboard;
        }

        public override void Execute(object? parameter)
        {
            if(parameter != null && parameter is (ComponentViewModel, Point))
            {
                (ComponentViewModel, Point) componentPair = ((ComponentViewModel, Point))parameter;
                ComponentViewModel component = componentPair.Item1;
                component.Position = componentPair.Item2;
                // If no other component in the list has the same id, add it to the list, otherwise update the entry
                if (_breadboard.PlacedComponents.Where(comp => comp.ComponentId.Equals(component.ComponentId)).Count() == 0)
                {
                    _breadboard.PlacedComponents.Add(component);
                }
                else
                {
                    ComponentViewModel oldComp = _breadboard.PlacedComponents.First(comp => comp.ComponentId.Equals(component.ComponentId));
                    oldComp = component;
                }
                MessageBox.Show($"Component: {component.Name} Dropped at: ({component.Position.X.ToString("0")}, {component.Position.Y.ToString("0")})");
            }
        }
    }
}
