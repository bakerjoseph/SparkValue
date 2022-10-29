using MongoDB.Driver.Linq;
using SparkValueBackend.Models;
using SparkValueBackend.Models.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;

namespace SparkValueBackend.ViewModels
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

        public ComponentViewModel(IComponentModel component)
        {
            _name = component.Name;
            _description = component.Description;
            _picture = component.Image;
            _component = component;
            _position = new Point();
        }

        public ComponentViewModel(ComponentViewModel oldContext)
        {
            _name = oldContext.Name;
            _description = oldContext.Description;
            _picture = oldContext.Picture;
            _component = oldContext._component;
            _breadboard = oldContext._breadboard;
            _position = new Point();
        }

        public void AddBreadboard(BreadboardViewModel breadboard)
        {
            _breadboard = breadboard;
        }

        public Type GetTypeOfComponent()
        {
            return _component.GetType();
        }
        public ComponentViewModel GetComponentViewModel()
        {
            if (GetTypeOfComponent() == typeof(ResistorComponentModel))
            {
                return new ResistorViewModel((ResistorComponentModel)_component);
            }
            else if (GetTypeOfComponent() == typeof(CapacitorComponentModel))
            {
                return new CapacitorViewModel((CapacitorComponentModel)_component);
            }
            return this;
        }

        /// <summary>
        /// Get the output of the component using its model class methods.
        /// </summary>
        /// <param name="inputVoltage">Voltage into the component</param>
        /// <param name="inputCurrent">Current into the component</param>
        /// <returns>The resulting output the component given the inputs.</returns>
        public (double outVoltage, double outCurrent) GetOutput(double inputVoltage, double inputCurrent)
        {
            return _component.GetOutput(inputVoltage, inputCurrent);
        }

        /// <summary>
        /// Get the formatted values and output of the component using its model class methods.
        /// </summary>
        /// <returns>A formatted string containing the values and output of the component.</returns>
        public string DisplayValues()
        {
            // If we have a valid circuit, ie a path to the positive and negative rails
            // display the component values given the output found by traversing towards the positive rail.
            // Otherwise we have zero voltage and current flowing.
            if (Position.X != 0 && Position.Y != 0 && CompleteCircuit(this))
            {
                (double inputVoltage, double inputCurrent) findResult = TraverseForOutput(this);
                return _component.DisplayValues(findResult.inputVoltage, findResult.inputCurrent);
            }
            else return _component.DisplayValues(0, 0);
        }

        #region Get Output of Component

        /// <summary>
        /// Starting at currentComponent, traverse until you find the positive rail or have checked both sides of currentComponent
        /// At that point generate the output to be taken in by currentComponent to generate its outputs for display purpose.
        /// </summary>
        /// <param name="currentComponent">The component to start at</param>
        /// <returns>The output of the previous component or the output of the breadboard power supply.</returns>
        private (double outputVoltage, double outputCurrent) TraverseForOutput(ComponentViewModel currentComponent)
        {
            bool positiveRailFound = false;

            List<ComponentViewModel> visitedComponents = new List<ComponentViewModel>();

            // Traverse Left, wire checking first and then component checking
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

            // If we have not already found the positive rail check the right side of this component
            if (!positiveRailFound)
            {
                // Traverse Right, wire checking first and then component checking
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
            
            // Multiple components traveld through
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
            // Did not find a way to the positive rail
            else
            {
                // Should never get here, it already checks if it is a complete circuit before running through getting its output!
                return (0, 0);
            }

        }

        /// <summary>
        /// Traverse through a given wire object trying to find the positive rail.
        /// Checks starting from the wireOrigin to find the next wire or component connection to continue traversing.
        /// </summary>
        /// <param name="wire">Current wire object</param>
        /// <param name="wireOrigin">Current end/output of the wire, could be either the start or end point</param>
        /// <param name="visitedComps">A list of components that have been visited</param>
        /// <returns>If the positive rail has been found and all the components taken to get there.</returns>
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

        /// <summary>
        /// Traverse through a given component trying to find the positive rail.
        /// Checks starting from the componentColumnOrigin to find the next wire or component connection to continue traversing.
        /// </summary>
        /// <param name="component">Current component object</param>
        /// <param name="componentColumnOrigin">Current end/output column of the component</param>
        /// <param name="visitedComps">A list of components that have been visited</param>
        /// <returns>If the positive rail has been found and all the components taken to get there.</returns>
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

        /// <summary>
        /// Checks if the circuit has a valid path from the currentComponent to both the positive and negative rails through components and wires.
        /// </summary>
        /// <param name="currentComponent">The component to start at.</param>
        /// <returns>If it has found both the positive negative rails.</returns>
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

        /// <summary>
        /// Traverse through a given wire object trying to find the positive and negative rails.
        /// Checks starting from the wireOrigin to find the next wire or component connection to continue traversing.
        /// </summary>
        /// <param name="wire">Current wire object</param>
        /// <param name="wireOrigin">Current end/output of the wire, could be either the start or end point</param>
        /// <returns>If the positive or negative rail have been found.</returns>
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

        /// <summary>
        /// Traverse through a given component object trying to find the positive and negative rails.
        /// Checks starting from the componentColumnOrigin to find the next wire or component connection to continue traversing.
        /// </summary>
        /// <param name="component">Current component object</param>
        /// <param name="componentColumnOrigin">Current end/output column of the component</param>
        /// <returns>If the positive or negative rail have been found.</returns>
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
       
        /// <summary>
        /// Get all wires that start or end in a given column.
        /// Component searching for connected wires.
        /// </summary>
        /// <param name="startPoint">The column to check in</param>
        /// <returns>Wires that start or end in the specified column.</returns>
        private List<WireModel> GetColumnMatches(double startPoint)
        {
            if (_breadboard.PlacedWires.Any())
            {
                return _breadboard.PlacedWires.Where(wire => (wire.startPosition.X >= startPoint && wire.startPosition.X <= startPoint + gridWidth)
                                                      || (wire.endPosition.X >= startPoint && wire.endPosition.X <= startPoint + gridWidth)).ToList();
            }
            else
            {
                return new List<WireModel>();
            }
        }

        /// <summary>
        /// Get all wires that start or end in the same column as the wireOrigin.
        /// Wire searching for connected wires.
        /// </summary>
        /// <param name="wire">Wire object that we are comming from</param>
        /// <param name="wireOrigin">Current end/output of the wire, could be either the start or end point, that we are searching from</param>
        /// <returns>Wires that start or end in the same column as wireOrigin.</returns>
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

        /// <summary>
        /// Get all components that start or end in the same column as the wireOrigin.
        /// Wire searching for connected components.
        /// </summary>
        /// <param name="wireOrigin">Current end/output of the wire, could be either the start or end point, that we are searching from</param>
        /// <returns>Components that start or end in the same column as wireOrigin.</returns>
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

        /// <summary>
        /// Get all components that start or end in the same column as the componentOriginColumn.
        /// Component searching for connected components.
        /// </summary>
        /// <param name="component">Component object that we are comming from</param>
        /// <param name="componentOriginColumn">Current end/output column of the component, that we are searching from</param>
        /// <returns>Components that start or end in the same column as componentOriginColumn</returns>
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

        /// <summary>
        /// Find the end or origin of a wire given the source and the wire.
        /// </summary>
        /// <param name="wire">Wire object that we want the opposite end of</param>
        /// <param name="startX">The source of the wire</param>
        /// <returns>The origin point of the given wire.</returns>
        private Point GetOppositeEndOfWire(WireModel wire, double startX)
        {
            return (wire.startPosition.X >= startX && wire.startPosition.X <= startX + gridWidth) ? wire.endPosition : wire.startPosition;
        }

        /// <summary>
        /// Find the end or origin of a component given the source and the component.
        /// </summary>
        /// <param name="comp">Component object that we want the opposite end of</param>
        /// <param name="startX">The source of the component</param>
        /// <returns>The origin x position of the given component.</returns>
        private double GetOppositeEndOfComponent(ComponentViewModel comp, double startX)
        {
            return (comp.Position.X >= startX && comp.Position.X <= startX + gridWidth) ? ((comp.Position.X + comp.Picture.DpiX) % gridWidth * -1) + (comp.Position.X + comp.Picture.DpiX) : comp.Position.X;
        }

        /// <summary>
        /// Find the next origin of the next wire in the traversel process.
        /// </summary>
        /// <param name="previousWireOrigin">Point of origin of the previous wire/Source of the current wire</param>
        /// <param name="currentWire">The new wire that we wnat the opposite end of</param>
        /// <returns>The origin point of the new/current wire.</returns>
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