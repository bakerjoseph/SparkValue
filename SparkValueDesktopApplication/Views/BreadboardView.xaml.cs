﻿using SparkValueDesktopApplication.Adorners;
using SparkValueBackend.Models;
using SparkValueBackend.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SparkValueBackend.Models.Components;

namespace SparkValueDesktopApplication.Views
{
    /// <summary>
    /// Interaction logic for BreadboardView.xaml
    /// </summary>
    public partial class BreadboardView : UserControl
    {
        private Point startPoint = new Point();
        private Point currentPoint = new Point();
        private bool drawing = false;

        private const double EraserMargin = 2;

        private const double GridSize = 24.9;

        private const int ComponentImageWidth = 75;
        private const int ComponentImageMaxHeight = 40;

        public static readonly DependencyProperty ComponentPlaceCommandProperty =
            DependencyProperty.Register("ComponentPlaceCommand", typeof(ICommand), typeof(BreadboardView), new PropertyMetadata(null));
        public ICommand ComponentPlaceCommand
        {
            get { return (ICommand)GetValue(ComponentPlaceCommandProperty); }
            set { SetValue(ComponentPlaceCommandProperty, value); }
        }


        public static readonly DependencyProperty WirePlaceCommandProperty =
            DependencyProperty.Register("WirePlaceCommand", typeof(ICommand), typeof(BreadboardView), new PropertyMetadata(null));
        public ICommand WirePlaceCommand
        {
            get { return (ICommand)GetValue(WirePlaceCommandProperty); }
            set { SetValue(WirePlaceCommandProperty, value); }
        }

        public static readonly DependencyProperty UpdateCurrentComponentCommandProperty =
            DependencyProperty.Register("UpdateCurrentComponentCommand", typeof(ICommand), typeof(BreadboardView), new PropertyMetadata(null));
        public ICommand UpdateCurrentComponentCommand
        {
            get { return (ICommand)GetValue(UpdateCurrentComponentCommandProperty); }
            set { SetValue(UpdateCurrentComponentCommandProperty, value); }
        }

        public BreadboardView()
        {
            InitializeComponent();
        }

        private void Component_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                Image image = sender as Image;
                if (image?.DataContext is ComponentViewModel)
                {
                    image.Source = ((ComponentViewModel)image.DataContext).Picture;
                    ((ToolTip)image.ToolTip).DataContext = (ComponentViewModel)image.DataContext;
                    DragDrop.DoDragDrop((DependencyObject)sender, new DataObject(DataFormats.Serializable, (image, (ComponentViewModel)image.DataContext)), DragDropEffects.Move);
                }
                else if ((image?.ToolTip as ToolTip).DataContext is ComponentViewModel)
                {
                    ComponentViewModel vm = (ComponentViewModel)(image?.ToolTip as ToolTip).DataContext;
                    image.Source = vm.Picture;
                    ((ToolTip)image.ToolTip).DataContext = vm;
                    DragDrop.DoDragDrop((DependencyObject)sender, new DataObject(DataFormats.Serializable, (image, vm)), DragDropEffects.Move);
                }
            }
        }

        private void Component_MouseEnter(object sender, MouseEventArgs e)
        {
            if (UpdateCurrentComponentCommand.CanExecute(null))
            {
                UpdateCurrentComponentCommand?.Execute(sender);
            }
        }

        private void Component_MouseLeave(object sender, MouseEventArgs e)
        {
            if (UpdateCurrentComponentCommand.CanExecute(null))
            {
                UpdateCurrentComponentCommand?.Execute(null);
            }
        }

        private void breadboard_Drop(object sender, DragEventArgs e)
        {
            (Image, ComponentViewModel) data = ((Image, ComponentViewModel))e.Data.GetData(DataFormats.Serializable);

            if (ComponentPlaceCommand.CanExecute(null))
            {
                SnapToGrid(data.Item1);
                Point dropPosition = new Point(Canvas.GetLeft(data.Item1), Canvas.GetTop(data.Item1));
                ComponentPlaceCommand?.Execute((data.Item2, dropPosition));

                // Open the context menu on placement of a resistor or capacitor component
                // They should be the only types of components that have context menus at this time
                if (data.Item1.ContextMenu != null)
                {
                    data.Item1.ContextMenu.IsOpen = true;
                }
                // Create and open the context menu on placement of a resistor or capacitor component
                else if (data.Item2.GetTypeOfComponent() == typeof(ResistorComponentModel) || data.Item2.GetTypeOfComponent() == typeof(CapacitorComponentModel))
                {
                    data.Item1.ContextMenu = CreateContextMenu(data.Item2);
                    data.Item1.ContextMenu.IsOpen = true;
                }
            }
            
        }

        private void expander_DragLeave(object sender, DragEventArgs e)
        {
            (Image, ComponentViewModel) data = ((Image, ComponentViewModel))e.Data.GetData(DataFormats.Serializable);

            Grid parent = data.Item1.Parent as Grid;
            if (parent != null && parent.Children.Count > 0)
            {
                parent.Children.Clear();

                // Create a new image with the same content as the previous image
                ComponentViewModel source = parent.DataContext as ComponentViewModel;
                // Add the new image to the grid, acts like a factory is pumping out new components
                parent.Children.Add(CreateNewComponent(source));
            }
        }

        private void breadboard_DragOver(object sender, DragEventArgs e)
        {
            (Image, ComponentViewModel) draggedData = ((Image, ComponentViewModel))e.Data.GetData(DataFormats.Serializable);

            Point dragPosition = e.GetPosition(breadboard);
            Canvas.SetLeft(draggedData.Item1, dragPosition.X);
            Canvas.SetTop(draggedData.Item1, dragPosition.Y);
            if (!breadboard.Children.Contains(draggedData.Item1))
            {
                breadboard.Children.Add(draggedData.Item1);
            }
        }

        private void breadboard_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (breadboard.Cursor == Cursors.Pen)
            {
                startPoint = e.GetPosition(breadboard);
                currentPoint = e.GetPosition(breadboard);
                drawing = true;
            }
        }

        private void breadboard_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed && breadboard.Cursor == Cursors.Pen && drawing)
            {
                Line line = new Line();
                if (!(bool)wireVis.IsChecked) line.Visibility = Visibility.Hidden;
                line.Stroke = new SolidColorBrush((wireColorPicker.SelectedColor != null) ? (Color)wireColorPicker.SelectedColor : Colors.Black);
                line.StrokeThickness = GetWireThickness();
                line.X1 = currentPoint.X;
                line.Y1 = currentPoint.Y;
                line.X2 = e.GetPosition(breadboard).X;
                line.Y2 = e.GetPosition(breadboard).Y;

                currentPoint = e.GetPosition(breadboard);

                breadboard.Children.Add(line);
            }
        }

        private void breadboard_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (WirePlaceCommand.CanExecute(null) && breadboard.Cursor == Cursors.Pen && startPoint != e.GetPosition(breadboard) && drawing)
            {
                WireModel wire = new WireModel(startPoint, e.GetPosition(breadboard), 
                    (width: positiveRail.ActualWidth, height: positiveRail.ActualHeight, offset: positiveBorder.Margin, borderThickness: positiveBorder.BorderThickness),
                    (width: negativeRail.ActualWidth, height: negativeRail.ActualHeight, offset: negativeBorder.Margin, borderThickness: negativeBorder.BorderThickness));
                WirePlaceCommand?.Execute(wire);
            }
            else if (WirePlaceCommand.CanExecute(null) && breadboard.Cursor != Cursors.Pen)
            {
                (Point start, Point end)? removed = RemoveLine(e.GetPosition(breadboard));
                if (removed != null) WirePlaceCommand?.Execute(removed);
            }

            drawing = false;
        }

        private void NumericBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !double.TryParse(((TextBox)sender).Text + e.Text, out double value);
        }

        private void trashCan_Drop(object sender, DragEventArgs e)
        {
            (Image, ComponentViewModel) data = ((Image, ComponentViewModel))e.Data.GetData(DataFormats.Serializable);
            
            if (ComponentPlaceCommand.CanExecute(null))
            {
                ComponentPlaceCommand?.Execute(data.Item2);
            }

            trashCan.Children.Remove(data.Item1);
        }

        private void trashCan_DragOver(object sender, DragEventArgs e)
        {
            (Image, ComponentViewModel) draggedData = ((Image, ComponentViewModel))e.Data.GetData(DataFormats.Serializable);

            Point dragPosition = e.GetPosition(trashCan);
            Canvas.SetLeft(draggedData.Item1, dragPosition.X);
            Canvas.SetTop(draggedData.Item1, dragPosition.Y);
            if (!trashCan.Children.Contains(draggedData.Item1))
            {
                trashCan.Children.Add(draggedData.Item1);
            }
        }

        private void canvas_DragLeave(object sender, DragEventArgs e)
        {
            (Image, ComponentViewModel) data = ((Image, ComponentViewModel))e.Data.GetData(DataFormats.Serializable);

            Canvas parent = data.Item1.Parent as Canvas;
            if (parent != null) parent.Children.Remove(data.Item1);
        }

        private void Component_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (sender != null && sender is Image)
            {
                Image img = sender as Image;
                AdornerLayer adornerLayer = AdornerLayer.GetAdornerLayer(img);
                if (adornerLayer != null)
                {
                    // If there is an adorner already on the image, remove it
                    Adorner[] adorners = adornerLayer.GetAdorners(img);
                    ComponentViewModel comp = (img.DataContext is ComponentViewModel) ? (ComponentViewModel)img.DataContext : (ComponentViewModel)((ToolTip)img.ToolTip).DataContext;
                    if (adorners != null)
                    {
                        foreach (Adorner adorner in adorners)
                        {
                            if (adorner.GetType().Equals(typeof(ComponentAdorner)))
                            {
                                adornerLayer.Remove(adorner);
                            }
                        }
                    }
                    // Create and add an adorner to the image
                    // If it is on the breadboard, not in the menu, and there are no other adorners on the image
                    else if (comp.Position.X != 0 && comp.Position.Y != 0)
                    {
                        ComponentAdorner adorner = new ComponentAdorner(img);
                        adornerLayer.Add(adorner);
                    }
                }

                // Check if the type of image is a resistor or capacitor component
                // and create the context menu if needed
                ComponentViewModel viewModel = (img.DataContext is ComponentViewModel) ? (ComponentViewModel)img.DataContext : (ComponentViewModel)((ToolTip)img.ToolTip).DataContext;
                if (img.ContextMenu == null && (viewModel.GetTypeOfComponent() == typeof(ResistorComponentModel) || viewModel.GetTypeOfComponent() == typeof(CapacitorComponentModel)))
                {
                    img.ContextMenu = CreateContextMenu(viewModel);
                }
            }
        }

        private void Component_ToolTipOpening(object sender, ToolTipEventArgs e)
        {
            if (sender != null && sender is Image)
            {
                ToolTip toolTip = (ToolTip)((Image)sender).ToolTip;
                toolTip.HorizontalOffset = 0;
                toolTip.VerticalOffset = 0;

                StackPanel stackPanel = toolTip.Content as StackPanel;
                TextBlock componentDisplayBlock = null;
                foreach (var obj in stackPanel.Children)
                {
                    if (obj is TextBlock && (obj as TextBlock).Name.Equals("CompDisplay")) componentDisplayBlock = (TextBlock)obj;
                }
                componentDisplayBlock?.GetBindingExpression(TextBlock.TextProperty).UpdateTarget();
            }
        }

        private void wireColorPicker_Closed(object sender, RoutedEventArgs e)
        {
            foreach (ComboBoxItem item in wireWidth.Items)
            {
                StackPanel? content = item.Content as StackPanel;
                if (content != null && content.Children[0] is Line)
                {
                    Line line = content.Children[0] as Line;
                    MultiBindingExpression bind = BindingOperations.GetMultiBindingExpression(line, Shape.StrokeProperty);
                    bind.UpdateTarget();
                }
            }
        }

        private void wireTool_Checked(object sender, RoutedEventArgs e)
        {
            wireTool.Content = "Erasor";
            MultiBindingExpression bind = BindingOperations.GetMultiBindingExpression(breadboard, CursorProperty);
            bind.UpdateTarget();
        }

        private void wireTool_Unchecked(object sender, RoutedEventArgs e)
        {
            wireTool.Content = "Pen";
            MultiBindingExpression bind = BindingOperations.GetMultiBindingExpression(breadboard, CursorProperty);
            bind.UpdateTarget();
        }

        /// <summary>
        /// Snap a UI element to a grid
        /// Inspired/Credit from this post https://stackoverflow.com/a/3508932
        /// </summary>
        /// <param name="element">Any UI element that needs to be snapped to a grid intersection</param>
        private void SnapToGrid(UIElement element)
        {
            double xSnap = Canvas.GetLeft(element) % GridSize;
            double ySnap = Canvas.GetTop(element) % GridSize;

            // If closer to the left remove the remainder from the left, pushing all the way left
            if (xSnap <= GridSize / 2.0) xSnap *= -1;
            // If closer to the right get the rest of the distance and add it to the left, pushing all the way right
            else xSnap = GridSize - xSnap;

            // If closer to the top remove the remainder from the top, pushing all the way to the top
            if (ySnap <= GridSize / 2.0) ySnap *= -1;
            // If closer to the bottom get the rest of the distance and add it to the top, pushing all the way to the bottom
            else ySnap = GridSize - ySnap;

            xSnap += Canvas.GetLeft(element);
            ySnap += Canvas.GetTop(element);

            Canvas.SetLeft(element, xSnap);
            Canvas.SetTop(element, ySnap);
        }

        private (Point start, Point end)? RemoveLine(Point origin)
        {
            Line lineAtPoint = null;
            // Go through each UIElement searching for a wire that is at the position passed in
            foreach (var elm in breadboard.Children)
            {
                if (elm is Line)
                {
                    Line line = (Line)elm;
                    if ((line.X1 <= origin.X + EraserMargin && line.X1 >= origin.X - EraserMargin && line.Y1 <= origin.Y + EraserMargin && line.Y1 >= origin.Y - EraserMargin) 
                        || (line.X2 <= origin.X + EraserMargin && line.X2 >= origin.X - EraserMargin && line.Y2 <= origin.Y + EraserMargin && line.Y2 >= origin.Y - EraserMargin))
                    {
                        lineAtPoint = line;
                        break;
                    }
                }
            }

            // If you found a wire, get the index and traverse to both ends of the wire,
            // then remove all of the lines from the breadboard,
            // returning the start and end points of the wire
            if (lineAtPoint != null)
            {
                List<Line> linesToRemove = new List<Line>();
                int index = breadboard.Children.IndexOf(lineAtPoint);
                // Traverse to upper wire end
                linesToRemove.AddRange(RemoveLine(index + 1, new Point(lineAtPoint.X2, lineAtPoint.Y2), new List<Line>(), "up"));
                linesToRemove.Add(lineAtPoint);
                // Traverse to lower wire end
                linesToRemove.AddRange(RemoveLine(index - 1, new Point(lineAtPoint.X1, lineAtPoint.Y1), new List<Line>(), "down"));
                // Make sure there are no duplicate lines
                linesToRemove.Distinct();

                // Remove every line from the children collection
                foreach (Line l in linesToRemove)
                {
                    breadboard.Children.Remove(l);
                }

                // Start point is at the begining of the list and the end poin is at the end of the list
                return (new Point(linesToRemove[0].X1, linesToRemove[0].Y1), new Point(linesToRemove[linesToRemove.Count - 1].X2, linesToRemove[linesToRemove.Count - 1].Y2));
            }
            // No line was found to remove from the children collection
            else return null;
        }

        private List<Line> RemoveLine(int index, Point origin, List<Line> removeList, string direction)
        {
            if (index > 0 && index < breadboard.Children.Count && breadboard.Children[index] is Line)
            {
                // Get the line at the index, make sure it is in the bounds of the children collection
                Line line = (Line)breadboard.Children[index];
                if (direction.Equals("up"))
                {
                    // Is the next line above the previous one a part of the wire we are trying to erase?
                    if (line.X1 == origin.X && line.Y1 == origin.Y)
                    {
                        // Keep going up the line until the end is found
                        removeList = RemoveLine(index + 1, new Point(line.X2, line.Y2), removeList, direction);
                        removeList.Add(line);
                    }
                }
                else if (direction.Equals("down"))
                {
                    // Is the next line below the previous one a part of the wire we are trying to erase?
                    if (line.X2 == origin.X && line.Y2 == origin.Y)
                    {
                        removeList.Add(line);
                        // Keep going down the line until the end is found
                        removeList = RemoveLine(index - 1, new Point(line.X1, line.Y1), removeList, direction);
                    }
                }
            }
            return removeList;
        }

        private Image CreateNewComponent(ComponentViewModel context)
        {
            ComponentViewModel newContext = new ComponentViewModel(context);
            Image newComponent = new Image();
            newComponent.Source = newContext.Picture;
            newComponent.Width = ComponentImageWidth;
            newComponent.MaxHeight = ComponentImageMaxHeight;
            newComponent.Cursor = Cursors.Hand;
            newComponent.DataContext = newContext;
            
            newComponent.MouseEnter += Component_MouseEnter;
            newComponent.MouseLeave += Component_MouseLeave;
            newComponent.MouseMove += Component_MouseMove;
            newComponent.MouseRightButtonUp += Component_MouseRightButtonUp;

            newComponent.ToolTip = CreateComponentToolTip(newContext);
            newComponent.ToolTipOpening += Component_ToolTipOpening;

            return newComponent;
        }

        private ToolTip CreateComponentToolTip(ComponentViewModel context)
        {
            // Create the textblocks with binding to the component view model
            Binding bindingName = new Binding();
            bindingName.Source = context;

            bindingName.Path = new PropertyPath("Name");
            TextBlock compName = new TextBlock();
            BindingOperations.SetBinding(compName, TextBlock.TextProperty, bindingName);
            compName.TextDecorations = TextDecorations.Underline;
            compName.Name = "CompName";

            Binding bindingValues = new Binding();
            bindingValues.Source = context;

            bindingValues.Path = new PropertyPath("DisplayComponent");
            TextBlock compValues = new TextBlock();
            BindingOperations.SetBinding(compValues, TextBlock.TextProperty, bindingValues);
            compValues.Name = "CompDisplay";

            // Create the stack panel and add the two textblocks
            StackPanel stackPanel = new StackPanel();
            stackPanel.Children.Add(compName);
            stackPanel.Children.Add(compValues);

            // Create the tooltip and add the stackpanel
            ToolTip toolTip = new ToolTip();
            toolTip.Content = stackPanel;

            return toolTip;
        }

        private ContextMenu CreateContextMenu(ComponentViewModel typeToCreate)
        {
            ContextMenu menu = new ContextMenu();
            if (typeToCreate.GetTypeOfComponent() == typeof(ResistorComponentModel))
            {
                menu = (ContextMenu)Resources["ResistanceMenu"];
                menu.DataContext = typeToCreate.GetComponentViewModel();
            }
            else if (typeToCreate.GetTypeOfComponent() == typeof(CapacitorComponentModel))
            {
                menu = (ContextMenu)Resources["CapacitanceMenu"];
                menu.DataContext= typeToCreate.GetComponentViewModel();
            }
            return menu;
        }

        private double GetWireThickness()
        {
            ComboBoxItem comboBoxItem = (ComboBoxItem)wireWidth.SelectedItem;
            StackPanel? content = comboBoxItem.Content as StackPanel;
            if (content != null)
            {
                return (content.Children[0] is Line) ? ((Line)content.Children[0]).StrokeThickness : 2;
            }
            else return 2;
        }
    }
}
