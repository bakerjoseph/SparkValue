using SparkValueBackend.Models;
using SparkValueBackend.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Point = System.Windows.Point;

namespace SparkValueBackend.Commands
{
    public class UpdateBreadboardWireCommand : CommandBase
    {
        private readonly BreadboardViewModel _breadboard;

        private const double WireOffset = 1.5;

        public UpdateBreadboardWireCommand(BreadboardViewModel breadboard)
        {
            _breadboard = breadboard;
        }

        public override void Execute(object? parameter)
        {
            if (parameter != null && parameter is WireModel)
            {
                WireModel wire = (WireModel)parameter;
                _breadboard.PlacedWires.Add(wire);   
            }
            else if (parameter != null && parameter is (Point, Point))
            {
                (Point, Point) wirePosition = ((Point, Point))parameter;
                WireModel removedWire = null;
                foreach (WireModel wire in _breadboard.PlacedWires)
                {
                    if ((ComparePointsWithOffset(wire.startPosition, wirePosition.Item1, WireOffset) && ComparePointsWithOffset(wire.endPosition, wirePosition.Item2, WireOffset))
                     || (ComparePointsWithOffset(wire.startPosition, wirePosition.Item2, WireOffset) && ComparePointsWithOffset(wire.endPosition, wirePosition.Item1, WireOffset)))
                    {
                        removedWire = wire;
                        break;
                    }
                }
                if (removedWire != null) _breadboard.PlacedWires.Remove(removedWire);
            }
        }

        private bool ComparePointsWithOffset(Point p1, Point p2, double offset)
        {
            // Point 1 is within the offset from point 2 in both the x and y directions
            return (p1.X <= p2.X + offset && p1.X >= p2.X - offset) && (p1.Y <= p2.Y + offset && p1.Y >= p2.Y - offset);
        }
    }
}
