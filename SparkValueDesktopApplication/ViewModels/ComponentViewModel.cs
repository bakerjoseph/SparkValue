using MongoDB.Driver.Linq;
using SparkValueDesktopApplication.Models;
using SparkValueDesktopApplication.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;

namespace SparkValueDesktopApplication.ViewModels
{
    public class ComponentViewModel : ViewModelBase
    {
        private int offsetValue = 10;
        private double gridWidth = 24.9;

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
            return $"Testing if complete {CompleteCircuit(this)}";
        }

        public void AddBreadboard(BreadboardViewModel breadboard)
        {
            _breadboard = breadboard;
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


        private bool CompleteCircuit(ComponentViewModel currentComponent)
        {
            bool isPowered = false;
            bool isGrounded = false;

            if (currentComponent.Position.X == 0 && currentComponent.Position.Y == 0) return false;

            // Check left column
            List<WireModel> leftColumnMatches = GetColumnMatches(currentComponent.Position.X);
            foreach (WireModel left in leftColumnMatches)
            {
                (bool powered, bool grounded) result = TraverseWire(left, GetOppositeEndOfWire(left, currentComponent.Position.X));
                isPowered = (!isPowered) ? result.powered : isPowered;
                isGrounded = (!isGrounded) ? result.grounded : isGrounded;
            }

            // Check right column
            double componentWidth = currentComponent.Picture.DpiX + currentComponent.Position.X;
            double closestRightColumn = (componentWidth % gridWidth * -1) + componentWidth;
            List<WireModel> rightColumnMatches = GetColumnMatches(closestRightColumn);
            foreach (WireModel right in rightColumnMatches)
            {
                (bool powered, bool grounded) result = TraverseWire(right, GetOppositeEndOfWire(right, closestRightColumn));
                isPowered = (!isPowered) ? result.powered : isPowered;
                isGrounded = (!isGrounded) ? result.grounded : isGrounded;
            }

            return isPowered && isGrounded;
        }

        private (bool powered, bool grounded) TraverseWire(WireModel wire, Point origin)
        {
            (bool powered, bool grounded) state = (false, false);

            // Wire connected to power rail
            if (wire.IsPowered) state.powered = true;
            // Wire connected to ground rail
            else if (wire.IsGounded) state.grounded = true;
            else
            {
                // Wire connected to other wire, traverse it!
                List<WireModel> connections = GetWireConnections(wire, origin);
                if (connections.Any())
                {
                    foreach(WireModel connection in connections)
                    {
                        (bool powered, bool grounded) result = TraverseWire(connection, GetNextWireOrigin(origin, connection));
                        state.powered = (!state.powered) ? result.powered : state.powered;
                        state.grounded = (!state.grounded) ? result.grounded : state.grounded;
                    }
                }
                // Wire connected to component
                else
                {

                }
            }


            return state;
        }

        private (bool powered, bool grounded) TraverseComponent(ComponentViewModel component)
        {
            (bool powered, bool grounded) state = (false, false);

            return state;
        }

        private List<WireModel> GetColumnMatches(double startPoint)
        {
            return _breadboard.PlacedWires.Where(wire => (wire.startPosition.X >= startPoint && wire.startPosition.X <= startPoint + gridWidth)
                                                      || (wire.endPosition.X >= startPoint && wire.endPosition.X <= startPoint + gridWidth)).ToList();
        }

        private List<WireModel> GetWireConnections(WireModel wire, Point origin)
        {
            List<WireModel> connections = new List<WireModel>();

            double originColumn = (origin.X % gridWidth * -1) + origin.X;

            foreach (WireModel w in _breadboard.PlacedWires)
            {
                // Are we looking at the same wire? Skip if we are.
                if (w != wire && 
                    ((w.startPosition.X >= originColumn && w.startPosition.X <= originColumn + gridWidth) 
                    || (w.endPosition.X >= originColumn && w.endPosition.X <= originColumn + gridWidth)))
                {
                    connections.Add(w);
                }
            }

            return connections;
        }

        private Point GetOppositeEndOfWire(WireModel wire, double startX)
        {
            return (wire.startPosition.X >= startX && wire.startPosition.X <= startX + gridWidth) ? wire.endPosition : wire.startPosition;
        }

        private Point GetNextWireOrigin(Point previousOrigin, WireModel currentWire)
        {
            if (currentWire.startPosition.X >= previousOrigin.X && currentWire.startPosition.X <= previousOrigin.X + gridWidth) return currentWire.startPosition;

            if (currentWire.endPosition.X >= previousOrigin.X && currentWire.endPosition.X <= previousOrigin.X + gridWidth) return currentWire.endPosition;

            // Should never occur
            return new Point();
        }
    }
}