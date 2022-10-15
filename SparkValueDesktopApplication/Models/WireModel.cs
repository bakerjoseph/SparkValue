using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SparkValueDesktopApplication.Models
{
    public class WireModel
    {
        Point startPosition { get; }
        Point endPosition { get; }

        public WireModel(Point startPosition, Point endPosition)
        {
            this.startPosition = startPosition;
            this.endPosition = endPosition;
        }

        public string StartPositionToString()
        {
            return $"({startPosition.X.ToString("0")}, {startPosition.Y.ToString("0")})";
        }
        
        public string EndPositionToString()
        {
            return $"({endPosition.X.ToString("0")}, {endPosition.Y.ToString("0")})";
        }
    }
}
