using MongoDB.Driver.Linq;
using SparkValueDesktopApplication.Models;
using SparkValueDesktopApplication.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SparkValueDesktopApplication.ViewModels
{
    public class ComponentViewModel : ViewModelBase
    {
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

        public string DisplayComponent => DisplayValues();

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

        public void AddBreadboard(BreadboardViewModel breadboard)
        {
            _breadboard = breadboard;
        }

        public (double outVoltage, double outCurrent) GetOutput(double inputVoltage, double inputCurrent)
        {
            return _component.GetOutput(inputVoltage, inputCurrent);
        }

        public string DisplayValues()
        {
            if (CompleteCircuit(this))
            {
                (double inputVoltage, double inputCurrent) findResult = TraverseForOutput(this);
                return _component.DisplayValues(findResult.inputVoltage, findResult.inputCurrent);
            }
            else return _component.DisplayValues(0, 0);
        }

        #region Get Output of Component
        private (double outputVoltage, double outputCurrent) TraverseForOutput(ComponentViewModel currentComponent)
        {
            bool positiveRailFound = false;

            List<ComponentViewModel> visitedComponents = new List<ComponentViewModel>();

            // Traverse Left
            List<WireModel> leftColumnWireMatches = GetColumnMatches(currentComponent.Position.X);
            foreach (WireModel leftWire in leftColumnWireMatches)
            {
                (bool positiveRailFound, List<ComponentViewModel> visitedComponents) result = TraverseWire(leftWire, GetOppositeEndOfWire(leftWire, currentComponent.Position.X), visitedComponents);
                visitedComponents = result.visitedComponents;
                positiveRailFound = (!positiveRailFound) ? result.positiveRailFound : positiveRailFound;
            }
            List<ComponentViewModel> leftColumnComponentMatches = GetComponentConnections(currentComponent, currentComponent.Position.X);
            foreach (ComponentViewModel leftComp in leftColumnComponentMatches)
            {
                visitedComponents.Add(leftComp);
                (bool positiveRailFound, List<ComponentViewModel> visitedComponents) result = TraverseComponent(leftComp, GetOppositeEndOfComponent(leftComp, currentComponent.Position.X), visitedComponents);
                visitedComponents = result.visitedComponents;
                positiveRailFound = (!positiveRailFound) ? result.positiveRailFound : positiveRailFound;
            }

            if (!positiveRailFound)
            {
                // Traverse Right
                double rightColumn = GetOppositeEndOfComponent(currentComponent, currentComponent.Position.X);
                List<WireModel> rightColumnWireMatches = GetColumnMatches(rightColumn);
                foreach (WireModel rightWire in rightColumnWireMatches)
                {
                    (bool positiveRailFound, List<ComponentViewModel> visitedComponents) result = TraverseWire(rightWire, GetOppositeEndOfWire(rightWire, rightColumn), visitedComponents);
                    visitedComponents = result.visitedComponents;
                    positiveRailFound = (!positiveRailFound) ? result.positiveRailFound : positiveRailFound;
                }
                List<ComponentViewModel> rightColumnCompMatches = GetComponentConnections(currentComponent, rightColumn);
                foreach (ComponentViewModel rightComp in rightColumnCompMatches)
                {
                    visitedComponents.Add(rightComp);
                    (bool positiveRailFound, List<ComponentViewModel> visitedComponents) result = TraverseComponent(rightComp, GetOppositeEndOfComponent(rightComp, rightColumn), visitedComponents);
                    visitedComponents = result.visitedComponents;
                    positiveRailFound = (!positiveRailFound) ? result.positiveRailFound : positiveRailFound;
                }
            }
            
            // Multiple components
            if (positiveRailFound && visitedComponents.Count > 1)
            {
                // Start at furthest out
                visitedComponents.Reverse();

                (double outputVoltage, double outputCurrent) results = (_breadboard.BreadboardVoltage, _breadboard.BreadboardCurrent);

                foreach (ComponentViewModel component in visitedComponents)
                {
                    results = component.GetOutput(results.outputVoltage, results.outputCurrent);
                }
                return results;
            }
            // Just this component
            else if (positiveRailFound)
            {
                return (_breadboard.BreadboardVoltage, _breadboard.BreadboardCurrent);
            }
            // No way to positive rail
            else
            {
                return (0, 0);
            }

        }

        private (bool positiveRailFound, List<ComponentViewModel> visitedComponents) TraverseWire(WireModel wire, Point wireOrigin, List<ComponentViewModel> visitedComps)
        {
            bool positiveRailFound = false;

            double originColumn = (wireOrigin.X % gridWidth * -1) + wireOrigin.X;

            // Wire connected to power rail
            if (wire.IsPowered) positiveRailFound = true;
            // Wire connected to ground rail
            else if (wire.IsGounded) return (false, visitedComps);
            else
            {
                List<WireModel> connectedWires = GetWireConnections(wire, wireOrigin);
                // Wire connected to other wire, traverse it!
                if (connectedWires.Any())
                {
                    foreach (WireModel connection in connectedWires)
                    {
                        (bool positiveRailFound, List<ComponentViewModel> visitedComponents) result = TraverseWire(connection, GetNextWireOrigin(wireOrigin, connection), visitedComps);
                        visitedComps = result.visitedComponents;
                        positiveRailFound = (!positiveRailFound) ? result.positiveRailFound : positiveRailFound;
                    }
                }

                List<ComponentViewModel> connectedComps = GetComponentConnections(wireOrigin);
                // Wire connected to component
                if (connectedComps.Any())
                {
                    foreach (ComponentViewModel component in connectedComps)
                    {
                        visitedComps.Add(component);
                        (bool positiveRailFound, List<ComponentViewModel> visitedComponents) result = TraverseComponent(component, GetOppositeEndOfComponent(component, originColumn), visitedComps);
                        visitedComps = result.visitedComponents;
                        positiveRailFound = (!positiveRailFound) ? result.positiveRailFound : positiveRailFound;
                    }
                }
            }

            return (positiveRailFound, visitedComps);
        }

        private (bool positiveRailFound, List<ComponentViewModel> visitedComponents) TraverseComponent(ComponentViewModel component, double componentColumnOrigin, List<ComponentViewModel> visitedComps)
        {
            bool positiveRailFound = false;

            List<WireModel> connectedWires = GetColumnMatches(componentColumnOrigin);
            if (connectedWires.Any())
            {
                foreach (WireModel wire in connectedWires)
                {
                    (bool positiveRailFound, List<ComponentViewModel> visitedComponents) result = TraverseWire(wire, GetOppositeEndOfWire(wire, componentColumnOrigin), visitedComps);
                    visitedComps = result.visitedComponents;
                    positiveRailFound = (!positiveRailFound) ? result.positiveRailFound : positiveRailFound;
                }
            }

            List<ComponentViewModel> connectedComponents = GetComponentConnections(component, componentColumnOrigin);
            if (connectedComponents.Any())
            {
                foreach (ComponentViewModel comp in connectedComponents)
                {
                    visitedComps.Add(comp);
                    (bool positiveRailFound, List<ComponentViewModel> visitedComponents) result = TraverseComponent(comp, GetOppositeEndOfComponent(comp, componentColumnOrigin), visitedComps);
                    visitedComps = result.visitedComponents;
                    positiveRailFound = (!positiveRailFound) ? result.positiveRailFound : positiveRailFound;
                }
            }

            return (positiveRailFound, visitedComps);
        }
        #endregion

        #region Check Circuit Status
        private bool CompleteCircuit(ComponentViewModel currentComponent)
        {
            bool isPowered = false;
            bool isGrounded = false;

            if (currentComponent.Position.X == 0 && currentComponent.Position.Y == 0) return false;

            // Check left column
            List<WireModel> leftColumnWireMatches = GetColumnMatches(currentComponent.Position.X);
            foreach (WireModel leftWire in leftColumnWireMatches)
            {
                (bool powered, bool grounded) result = TraverseWire(leftWire, GetOppositeEndOfWire(leftWire, currentComponent.Position.X));
                isPowered = (!isPowered) ? result.powered : isPowered;
                isGrounded = (!isGrounded) ? result.grounded : isGrounded;
            }
            List<ComponentViewModel> leftColumnComponentMatches = GetComponentConnections(currentComponent, currentComponent.Position.X);
            foreach (ComponentViewModel leftComp in leftColumnComponentMatches)
            {
                (bool powered, bool grounded) result = TraverseComponent(leftComp, GetOppositeEndOfComponent(leftComp, currentComponent.Position.X));
                isPowered = (!isPowered) ? result.powered : isPowered;
                isGrounded = (!isGrounded) ? result.grounded : isGrounded;
            }

            // Check right column
            double rightColumn = GetOppositeEndOfComponent(currentComponent, currentComponent.Position.X);
            List<WireModel> rightColumnWireMatches = GetColumnMatches(rightColumn);
            foreach (WireModel rightWire in rightColumnWireMatches)
            {
                (bool powered, bool grounded) result = TraverseWire(rightWire, GetOppositeEndOfWire(rightWire, rightColumn));
                isPowered = (!isPowered) ? result.powered : isPowered;
                isGrounded = (!isGrounded) ? result.grounded : isGrounded;
            }
            List<ComponentViewModel> rightColumnCompMatches = GetComponentConnections(currentComponent, rightColumn);
            foreach (ComponentViewModel rightComp in rightColumnCompMatches)
            {
                (bool powered, bool grounded) result = TraverseComponent(rightComp, GetOppositeEndOfComponent(rightComp, rightColumn));
                isPowered = (!isPowered) ? result.powered : isPowered;
                isGrounded = (!isGrounded) ? result.grounded : isGrounded;
            }

            return isPowered && isGrounded;
        }

        private (bool powered, bool grounded) TraverseWire(WireModel wire, Point wireOrigin)
        {
            (bool powered, bool grounded) state = (false, false);

            double originColumn = (wireOrigin.X % gridWidth * -1) + wireOrigin.X;

            // Wire connected to power rail
            if (wire.IsPowered) state.powered = true;
            // Wire connected to ground rail
            else if (wire.IsGounded) state.grounded = true;
            else
            {
                List<WireModel> connectedWires = GetWireConnections(wire, wireOrigin);
                // Wire connected to other wire, traverse it!
                if (connectedWires.Any())
                {
                    foreach(WireModel connection in connectedWires)
                    {
                        (bool powered, bool grounded) result = TraverseWire(connection, GetNextWireOrigin(wireOrigin, connection));
                        state.powered = (!state.powered) ? result.powered : state.powered;
                        state.grounded = (!state.grounded) ? result.grounded : state.grounded;
                    }
                }

                List<ComponentViewModel> connectedComps = GetComponentConnections(wireOrigin);
                // Wire connected to component
                if (connectedComps.Any())
                {
                    foreach (ComponentViewModel component in connectedComps)
                    {
                        (bool powered, bool grounded) result = TraverseComponent(component, GetOppositeEndOfComponent(component, originColumn));
                        state.powered = (!state.powered) ? result.powered : state.powered;
                        state.grounded = (!state.grounded) ? result.grounded : state.grounded;
                    }
                }
            }

            return state;
        }

        private (bool powered, bool grounded) TraverseComponent(ComponentViewModel component, double componentColumnOrigin)
        {
            (bool powered, bool grounded) state = (false, false);

            List<WireModel> connectedWires = GetColumnMatches(componentColumnOrigin);
            if (connectedWires.Any())
            {
                foreach (WireModel wire in connectedWires)
                {
                    (bool powered, bool grounded) result = TraverseWire(wire, GetOppositeEndOfWire(wire, componentColumnOrigin));
                    state.powered = (!state.powered) ? result.powered : state.powered;
                    state.grounded = (!state.grounded) ? result.grounded : state.grounded;
                }
            }

            List<ComponentViewModel> connectedComponents = GetComponentConnections(component, componentColumnOrigin);
            if (connectedComponents.Any())
            {
                foreach (ComponentViewModel comp in connectedComponents)
                {
                    (bool powered, bool grounded) result = TraverseComponent(comp, GetOppositeEndOfComponent(comp, componentColumnOrigin));
                    state.powered = (!state.powered) ? result.powered : state.powered;
                    state.grounded = (!state.grounded) ? result.grounded : state.grounded;
                }
            }

            return state;
        }
        #endregion

        #region Component and Wire Utility Methods
        private List<WireModel> GetColumnMatches(double startPoint)
        {
            return _breadboard.PlacedWires.Where(wire => (wire.startPosition.X >= startPoint && wire.startPosition.X <= startPoint + gridWidth)
                                                      || (wire.endPosition.X >= startPoint && wire.endPosition.X <= startPoint + gridWidth)).ToList();
        }

        private List<WireModel> GetWireConnections(WireModel wire, Point wireOrigin)
        {
            List<WireModel> connections = new List<WireModel>();

            double originColumn = (wireOrigin.X % gridWidth * -1) + wireOrigin.X;

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

        private List<ComponentViewModel> GetComponentConnections(Point wireOrigin)
        {
            List<ComponentViewModel> connections = new List<ComponentViewModel>();

            double originColumn = (wireOrigin.X % gridWidth * -1) + wireOrigin.X;

            foreach (ComponentViewModel comp in _breadboard.PlacedComponents)
            {
                double compRightSide = comp.Picture.DpiX + comp.Position.X;
                if ((comp.Position.X >= originColumn && comp.Position.X <= originColumn + gridWidth)
                    || (compRightSide >= originColumn && compRightSide <= originColumn + gridWidth))
                {
                    connections.Add(comp);
                }
            }

            return connections;
        }

        private List<ComponentViewModel> GetComponentConnections(ComponentViewModel component, double componentOriginColumn)
        {
            List<ComponentViewModel> connections = new List<ComponentViewModel>();

            foreach (ComponentViewModel comp in _breadboard.PlacedComponents)
            {
                double compRightSide = comp.Picture.DpiX + comp.Position.X;
                if (comp != component &&
                    ((comp.Position.X >= componentOriginColumn && comp.Position.X <= componentOriginColumn + gridWidth)
                    || (compRightSide >= componentOriginColumn && compRightSide <= componentOriginColumn + gridWidth)))
                {
                    connections.Add(comp);
                }
            }

            return connections;
        }

        private Point GetOppositeEndOfWire(WireModel wire, double startX)
        {
            return (wire.startPosition.X >= startX && wire.startPosition.X <= startX + gridWidth) ? wire.endPosition : wire.startPosition;
        }

        private double GetOppositeEndOfComponent(ComponentViewModel comp, double startX)
        {
            return (comp.Position.X >= startX && comp.Position.X <= startX + gridWidth) ? ((comp.Position.X + comp.Picture.DpiX) % gridWidth * -1) + (comp.Position.X + comp.Picture.DpiX) : comp.Position.X;
        }

        private Point GetNextWireOrigin(Point previousWireOrigin, WireModel currentWire)
        {
            if (currentWire.startPosition.X >= previousWireOrigin.X && currentWire.startPosition.X <= previousWireOrigin.X + gridWidth) return currentWire.startPosition;

            if (currentWire.endPosition.X >= previousWireOrigin.X && currentWire.endPosition.X <= previousWireOrigin.X + gridWidth) return currentWire.endPosition;

            // Should never occur
            return new Point();
        }
        #endregion
    }
}