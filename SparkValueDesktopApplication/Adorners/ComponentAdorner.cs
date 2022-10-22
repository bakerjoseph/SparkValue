﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SparkValueDesktopApplication.Adorners
{
    public class ComponentAdorner : Adorner
    {
        // Most of this code was reused from this article https://learn.microsoft.com/en-us/dotnet/desktop/wpf/advanced/how-to-rotate-ink?view=netframeworkdesktop-4.8

        private Thumb rotateHandle;

        private Path outline;

        private VisualCollection visualChildren;

        private Point center;
        private double lastAngle;

        private RotateTransform rotation;

        private const int HandleMargin = 10;

        private Rect compBounds = Rect.Empty;

        public ComponentAdorner(UIElement adornedElement) : base(adornedElement)
        {
            visualChildren = new VisualCollection(this);

            rotateHandle = new Thumb();
            rotateHandle.Cursor = Cursors.SizeNWSE;
            rotateHandle.Width = 20;
            rotateHandle.Height = 20;
            rotateHandle.Background = Brushes.SteelBlue;

            rotateHandle.DragDelta += new DragDeltaEventHandler(rotateHandle_DragDelta);
            rotateHandle.DragCompleted += new DragCompletedEventHandler(rotateHandle_DragCompleted);

            outline = new Path();
            outline.Stroke = Brushes.SteelBlue;
            outline.StrokeThickness = 1;

            visualChildren.Add(outline);
            visualChildren.Add(rotateHandle);

            compBounds = new Rect(AdornedImage.DesiredSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            if (compBounds.IsEmpty) return finalSize;

            center = new Point(compBounds.X + compBounds.Width / 2,
                               compBounds.Y + compBounds.Height / 2);

            Rect handleRect = new Rect(compBounds.X, compBounds.Y - (compBounds.Height / 2 + HandleMargin),
                                       compBounds.Width, compBounds.Height);

            if (rotation != null) handleRect.Transform(rotation.Value);

            rotateHandle.Arrange(handleRect);
            outline.Data = new RectangleGeometry(compBounds);
            outline.Arrange(new Rect(finalSize));
            return finalSize;
        }

        private void rotateHandle_DragDelta(object sender, DragDeltaEventArgs e)
        {
            Point pos = Mouse.GetPosition(this);

            double deltaX = pos.X - center.X;
            double deltaY = pos.Y - center.Y;

            if (deltaY.Equals(0)) return;

            double tan = deltaX / deltaY;
            double angle = Math.Atan(tan);

            angle = angle * 180 / Math.PI;

            if (deltaY > 0) angle = 180 - Math.Abs(angle);

            if (deltaX < 0) angle = -Math.Abs(angle);
            else angle = Math.Abs(angle);

            if (Double.IsNaN(angle)) return;

            if (angle < -315) angle = 0;
            else if (angle <= -225 && angle >= -315) angle = -270;
            else if (angle <= -135 && angle >= -225) angle = -180;
            else if (angle <= -45 && angle >= -135) angle = -90;
            else if (angle <= -45 && angle <= 45) angle = 0;
            else if (angle >= 45 && angle <= 135) angle = 90;
            else if (angle >= 135 && angle <= 225) angle = 180;
            else if (angle >= 225 && angle <= 315) angle = 270;
            else if (angle > 315) angle = 0;

            rotation = new RotateTransform(angle, center.X, center.Y);
            outline.RenderTransform = rotation;
        }

        private void rotateHandle_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            if (rotation == null) return;

            AdornedImage.RenderTransform = new RotateTransform(rotation.Angle);

            lastAngle = rotation.Angle;

            this.InvalidateArrange();
        }

        private Image AdornedImage
        {
            get
            {
                return (Image)AdornedElement;
            }
        }

        protected override int VisualChildrenCount
        {
            get
            {
                return visualChildren.Count;
            }
        }

        protected override Visual GetVisualChild(int index)
        {
            return visualChildren[index];
        }
    }
}