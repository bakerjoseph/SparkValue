using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SparkValueBackend.Models
{
    public class WireModel
    {
        public Point startPosition { get; }
        public Point endPosition { get; }
        public bool IsPowered => CheckIsPowered();
        public bool IsGounded => CheckIsGrounded();

        private readonly (double width, double height, Thickness offset, Thickness borderThickness) _positiveRail;
        private readonly double _posOffsetX;
        private readonly double _posOffsetY;

        private readonly (double width, double height, Thickness offset, Thickness borderThickness) _negativeRail;
        private readonly double _negOffsetX;
        private readonly double _negOffsetY;

        public WireModel(
            Point startPosition, Point endPosition,
            (double width, double height, Thickness offset, Thickness borderThickness) positiveRail, 
            (double width, double height, Thickness offset, Thickness borderThickness) negativeRail)
        {
            this.startPosition = startPosition;
            this.endPosition = endPosition;

            _positiveRail = positiveRail;
            _posOffsetX = _positiveRail.offset.Left + _positiveRail.borderThickness.Left;
            _posOffsetY = _positiveRail.offset.Top + _positiveRail.borderThickness.Top;

            _negativeRail = negativeRail;
            _negOffsetX = _negativeRail.offset.Left + _negativeRail.borderThickness.Left;
            _negOffsetY = _negativeRail.offset.Top + _negativeRail.borderThickness.Top;
        }

        public string StartPositionToString()
        {
            return $"({startPosition.X.ToString("0")}, {startPosition.Y.ToString("0")})";
        }
        
        public string EndPositionToString()
        {
            return $"({endPosition.X.ToString("0")}, {endPosition.Y.ToString("0")})";
        }

        private bool CheckIsPowered()
        {
            // Does the start point fall within the boundaries of the positive rail?
            if ((_posOffsetX <= startPosition.X && startPosition.X <= _posOffsetX + _positiveRail.width) && 
                (_posOffsetY <= startPosition.Y && startPosition.Y <= _posOffsetY + _positiveRail.height)) return true;
            // Does the end point fall within the boundaries of the positive rail?
            else if ((_posOffsetX <= endPosition.X && endPosition.X <= _posOffsetX + _positiveRail.width) && 
                     (_posOffsetY <= endPosition.Y && endPosition.Y <= _posOffsetY + _positiveRail.height)) return true;
            else return false;
        }

        private bool CheckIsGrounded()
        {
            // Does the start point fall within the boundaries of the negative rail?
            if ((_negOffsetX <= startPosition.X && startPosition.X <= _negOffsetX + _negativeRail.width) &&
                (_negOffsetY <= startPosition.Y && startPosition.Y <= _negOffsetY + _negativeRail.height)) return true;
            // Does the end point fall within the boundaries of the negative rail?
            else if ((_negOffsetX <= endPosition.X && endPosition.X <= _negOffsetX + _negativeRail.width) &&
                     (_negOffsetY <= endPosition.Y && endPosition.Y <= _negOffsetY + _negativeRail.height)) return true;
            else return false;
        }
    }
}
