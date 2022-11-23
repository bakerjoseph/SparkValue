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
    /// Interaction logic for NOTsXORsView.xaml
    /// </summary>
    public partial class NOTsXORsView : UserControl
    {
        public NOTsXORsView()
        {
            InitializeComponent();
        }

        private void NOTToggleButton_Changed(object sender, RoutedEventArgs e)
        {
            NOTOutput?.GetBindingExpression(Shape.FillProperty).UpdateTarget();
        }

        private void XORToggleButton_Changed(object sender, RoutedEventArgs e)
        {
            XOROutput?.GetBindingExpression(Shape.FillProperty).UpdateTarget();
        }
    }
}
