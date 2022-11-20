using System;
using System.Collections.Generic;
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

namespace SparkValueDesktopApplication.Views.LessonInteractiveElements
{
    /// <summary>
    /// Interaction logic for ResistorChartView.xaml
    /// </summary>
    public partial class ResistorChartView : UserControl
    {
        public static readonly DependencyProperty UpdateBandCommandProperty =
            DependencyProperty.Register("UpdateBandCommand", typeof(ICommand), typeof(ResistorChartView), new PropertyMetadata(null));

        public ICommand UpdateBandCommand
        {
            get { return (ICommand)GetValue(UpdateBandCommandProperty); }
            set { SetValue(UpdateBandCommandProperty, value); }
        }

        public ResistorChartView()
        {
            InitializeComponent();
        }

        private void BandMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Update bindings when the selection of band count gets changed
            if (sender is ComboBox)
            {
                if (DigitThree != null)
                {
                    MultiBindingExpression bindDigit3 = BindingOperations.GetMultiBindingExpression(DigitThree, VisibilityProperty);
                    bindDigit3.UpdateTarget();
                }

                if (DigitThreeBand != null)
                {
                    MultiBindingExpression bindDigit3Band = BindingOperations.GetMultiBindingExpression(DigitThreeBand, VisibilityProperty);
                    bindDigit3Band.UpdateTarget();
                }

                if (Tempa != null)
                {
                    MultiBindingExpression bindTempa = BindingOperations.GetMultiBindingExpression(Tempa, VisibilityProperty);
                    bindTempa.UpdateTarget();
                }

                if (TempaBand != null)
                {
                    MultiBindingExpression bindTempaBand = BindingOperations.GetMultiBindingExpression(TempaBand, VisibilityProperty);
                    bindTempaBand.UpdateTarget();
                }
            }
        }

        private void Band_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UpdateBandCommand != null && UpdateBandCommand.CanExecute(null) && sender is ComboBox)
            {
                ComboBox selector = sender as ComboBox;

                if (selector?.SelectedItem is ComboBoxItem)
                {
                    ComboBoxItem selected = selector.SelectedItem as ComboBoxItem;

                    if (selected?.Content is Grid)
                    {
                        Grid selectedItem = selected.Content as Grid;

                        if (selectedItem?.Children[0] is Rectangle && selectedItem?.Children[1] is TextBlock)
                        {
                            Rectangle background = selectedItem.Children[0] as Rectangle;
                            TextBlock content = selectedItem.Children[1] as TextBlock;

                            ExecuteBandUpdateCommand(selector.Name, background.Fill, content.Text);
                        }
                    }
                }
                
            }
        }

        private void ExecuteBandUpdateCommand(string bandName, Brush color, string value)
        {
            switch (bandName)
            {
                case "DigitOne":
                    UpdateBandCommand.Execute((1, color, value));
                    break;
                case "DigitTwo":
                    UpdateBandCommand.Execute((2, color, value));
                    break;
                case "DigitThree":
                    UpdateBandCommand.Execute((3, color, value));
                    break;
                case "Mult":
                    UpdateBandCommand.Execute((4, color, value));
                    break;
                case "Toler":
                    UpdateBandCommand.Execute((5, color, value));
                    break;
                case "Tempa":
                    UpdateBandCommand.Execute((6, color, value));
                    break;
            }
        }
    }
}
