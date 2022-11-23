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
    /// Interaction logic for SwitchingView.xaml
    /// </summary>
    public partial class SwitchingView : UserControl
    {
        public SwitchingView()
        {
            InitializeComponent();
        }

        private void ToggleButton_Changed(object sender, RoutedEventArgs e)
        {
            // Update the LED after the switch
            SwitchOutput?.GetBindingExpression(Shape.FillProperty).UpdateTarget();
        }
    }
}
