using SparkValueDesktopApplication.Models;
using SparkValueDesktopApplication.Models.Components;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SparkValueDesktopApplication.ViewModels
{
    public class ComponentViewModel : ViewModelBase
    {
        private int offsetValue = 10;

        public Guid ComponentId { get; } = Guid.NewGuid();

        private string _name;
        public string Name
        {
            get 
            { 
                return _name; 
            }
            set 
            { 
                _name = value; 
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private BitmapImage _picture;
        public BitmapImage Picture 
        { 
            get 
            { 
                return _picture; 
            } 
            set 
            { 
                _picture = value;
                OnPropertyChanged(nameof(Picture));
            } 
        }

        private Point _position;
        public Point Position
        {
            get { return _position; }
            set
            {
                _position = value;
                OnPropertyChanged(nameof(Position));
            }
        }

        public string DisplayComponent => TestingMethod();

        private IComponentModel _component;

        private BreadboardViewModel _breadboard;

        public ComponentViewModel() { }

        public ComponentViewModel(IComponentModel component)
        {
            _name = component.Name;
            _description = component.Description;
            _picture = component.Image;
            _component = component;
            _position = new Point();
        }

        public string TestingMethod()
        {
            return "This is a test,\n can I bind to a method call variable?";
        }

        public (double outVoltage, double outCurrent) GetOutput()
        {
            (double inputVoltage, double inputCurrent) findResult = GetNearby(((0, 0), Position)).Item1;
            return _component.GetOutput(findResult.inputVoltage, findResult.inputCurrent);
        }

        public string DisplayValues()
        {
            (double inputVoltage, double inputCurrent) findResult = GetNearby(((0, 0), Position)).Item1;
            return _component.DisplayValues(findResult.inputVoltage, findResult.inputCurrent);
        }

        private ((double outVoltage, double outCurrent), Point position) GetNearby(((double inputVoltage, double inputCurrent) inputs, Point currentPosition) nearby)
        {
            // Check if in the same vertical column as a wire
            foreach (WireModel wire in _breadboard.PlacedWires)
            {
                // Will need to change will not work if drawing wires backward
                if (wire.endPosition.X - offsetValue <= nearby.currentPosition.X && nearby.currentPosition.X <= wire.endPosition.X + offsetValue)
                {
                    // Connected to positive rail
                    if (wire.IsPowered)
                    {
                        return ((_breadboard.BreadboardVoltage, _breadboard.BreadboardCurrent), new Point());
                    }
                    // Connected to negative rail
                    else if (wire.IsGounded)
                    {
                        break;
                    }
                    // Wire goes to another component or it is not connected??
                    else
                    {
                        return GetNearby((nearby.inputs, wire.startPosition));
                    }
                }
            }

            // Check if in the same vertical column as a component
            foreach (ComponentViewModel comp in _breadboard.PlacedComponents)
            {
                // Not our current component and in the same vertical column as the current component +- offset
                if (!comp.ComponentId.Equals(ComponentId) &&
                    (comp.Position.X + comp.Picture.Width - offsetValue <= Position.X && Position.X <= comp.Position.X + comp.Picture.Width + offsetValue))
                {
                    return (comp.GetOutput(), comp.Position);
                }
            }

            return (nearby.inputs, nearby.currentPosition);
        }



    }
}