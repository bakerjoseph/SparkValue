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
    /// Interaction logic for ANDsORsView.xaml
    /// </summary>
    public partial class ANDsORsView : UserControl
    {
        public ANDsORsView()
        {
            InitializeComponent();
        }

        private void ANDToggleButton_Changed(object sender, RoutedEventArgs e)
        {
            // Update the output binding of the AND Gate
            ANDOutput?.GetBindingExpression(Shape.FillProperty).UpdateTarget();
        }

        private void ORToggleButton_Changed(object sender, RoutedEventArgs e)
        {
            // Update the output binding of the OR Gate
            OROutput?.GetBindingExpression(Shape.FillProperty).UpdateTarget();
        }
    }
}
