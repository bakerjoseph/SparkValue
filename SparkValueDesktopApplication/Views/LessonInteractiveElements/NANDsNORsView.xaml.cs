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
    /// Interaction logic for NANDsNORsView.xaml
    /// </summary>
    public partial class NANDsNORsView : UserControl
    {
        public NANDsNORsView()
        {
            InitializeComponent();
        }

        private void NANDToggleButton_Changed(object sender, RoutedEventArgs e)
        {
            // Update the output binding of the NAND Gate
            NANDOutput?.GetBindingExpression(Shape.FillProperty).UpdateTarget();
        }

        private void NORToggleButton_Changed(object sender, RoutedEventArgs e)
        {
            // Update the output binding of the NOR Gate
            NOROutput?.GetBindingExpression(Shape.FillProperty).UpdateTarget();
        }
    }
}
