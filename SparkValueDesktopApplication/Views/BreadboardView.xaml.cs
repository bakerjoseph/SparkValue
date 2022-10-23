using SparkValueDesktopApplication.Adorners;
using SparkValueDesktopApplication.Models;
using SparkValueDesktopApplication.ViewModels;
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

namespace SparkValueDesktopApplication.Views
{
    /// <summary>
    /// Interaction logic for BreadboardView.xaml
    /// </summary>
    public partial class BreadboardView : UserControl
    {
        private Point startPoint = new Point();
        private Point currentPoint = new Point();

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

        public BreadboardView()
        {
            InitializeComponent();
        }

        private void Component_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                ComponentViewModel selectedComponent = null;
                foreach (var item in categoryList.Items)
                {
                    //UIElement element = (UIElement)categoryList.ItemContainerGenerator.ContainerFromItem(item);
                    ContentPresenter container = categoryList.ItemContainerGenerator.ContainerFromItem(item) as ContentPresenter;
                    container.ApplyTemplate();

                    Expander? expander = container.ContentTemplate.FindName("dropDown", container) as Expander;
                    ItemsControl? itemsControl = container.ContentTemplate.FindName("componentList", container) as ItemsControl;

                    if (selectedComponent != null) break;

                    if (expander != null && itemsControl != null && expander.IsExpanded)
                    {
                        if (itemsControl.Items.Count <= 0) break;
                        foreach (var component in itemsControl.Items)
                        {
                            if(component.GetType() == typeof(ComponentViewModel))
                            {
                                ComponentViewModel comp = component as ComponentViewModel;
                                Image targetImage = sender as Image;
                                if (comp?.Picture == targetImage?.Source)
                                {
                                    selectedComponent = comp;
                                    break;
                                }
                            }
                        }
                    }
                }
                Image image = sender as Image;
                image.Source = selectedComponent?.Picture;
                image.DataContext = selectedComponent;
                DragDrop.DoDragDrop((DependencyObject)sender, new DataObject(DataFormats.Serializable, (image, selectedComponent)), DragDropEffects.Move);
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
            }
            
        }

        private void expander_DragLeave(object sender, DragEventArgs e)
        {
            (Image, ComponentViewModel) data = ((Image, ComponentViewModel))e.Data.GetData(DataFormats.Serializable);

            Grid parent = data.Item1.Parent as Grid;
            if (parent != null && parent.Children.Count > 0)
            {
                parent.Children.RemoveAt(0);

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
            startPoint = e.GetPosition(breadboard);
            currentPoint = e.GetPosition(breadboard);
        }

        private void breadboard_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Line line = new Line();
                line.Stroke = SystemColors.WindowFrameBrush;
                line.StrokeThickness = 2;
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
            if (WirePlaceCommand.CanExecute(null))
            {
                WireModel wire = new WireModel(startPoint, e.GetPosition(breadboard), 
                    (width: positiveRail.ActualWidth, height: positiveRail.ActualHeight, offset: positiveBorder.Margin, borderThickness: positiveBorder.BorderThickness),
                    (width: negativeRail.ActualWidth, height: negativeRail.ActualHeight, offset: negativeBorder.Margin, borderThickness: negativeBorder.BorderThickness));
                WirePlaceCommand?.Execute(wire);
            }
        }

        private void NumericBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !double.TryParse(((TextBox)sender).Text + e.Text, out double value);
        }

        private void trashCan_Drop(object sender, DragEventArgs e)
        {
            (Image, ComponentViewModel) data = ((Image, ComponentViewModel))e.Data.GetData(DataFormats.Serializable);

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
                    ComponentViewModel comp = (ComponentViewModel)img.DataContext;
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
                    // If it is on the breadboard, not in the menu, and there are no other adorners on teh image
                    else if (comp.Position.X != 0 && comp.Position.Y != 0)
                    {
                        ComponentAdorner adorner = new ComponentAdorner(img);
                        adornerLayer.Add(adorner);
                    }
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
            }
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

        private Image CreateNewComponent(ComponentViewModel context)
        {
            Image newComponent = new Image();
            newComponent.Source = context.Picture;
            newComponent.Width = ComponentImageWidth;
            newComponent.MaxHeight = ComponentImageMaxHeight;
            newComponent.Cursor = Cursors.Hand;

            newComponent.MouseMove += Component_MouseMove;
            newComponent.MouseRightButtonUp += Component_MouseRightButtonUp;

            newComponent.ToolTip = CreateComponentToolTip(context);

            return newComponent;
        }

        private ToolTip CreateComponentToolTip(ComponentViewModel context)
        {
            // Create the textblocks with binding to the component view model
            Binding bindingName = new Binding();
            bindingName.Source = context;
            bindingName.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            bindingName.Path = new PropertyPath("Name");
            TextBlock compName = new TextBlock();
            BindingOperations.SetBinding(compName, TextBlock.TextProperty, bindingName);
            compName.TextDecorations = TextDecorations.Underline;

            Binding bindingValues = new Binding();
            bindingValues.Source = context;
            bindingValues.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;

            bindingValues.Path = new PropertyPath("DisplayComponent");
            TextBlock compValues = new TextBlock();
            BindingOperations.SetBinding(compValues, TextBlock.TextProperty, bindingValues);

            // Create the stack panel and add the two textblocks
            StackPanel stackPanel = new StackPanel();
            stackPanel.Children.Add(compName);
            stackPanel.Children.Add(compValues);

            // Create the tooltip and add the stackpanel
            ToolTip toolTip = new ToolTip();
            toolTip.Content = stackPanel;

            return toolTip;
        }
    }
}
