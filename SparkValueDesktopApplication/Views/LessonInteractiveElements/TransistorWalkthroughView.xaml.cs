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
    /// Interaction logic for TransistorWalkthroughView.xaml
    /// </summary>
    public partial class TransistorWalkthroughView : UserControl
    {
        public TransistorWalkthroughView()
        {
            InitializeComponent();
        }

        private void ToggleButton_Changed(object sender, RoutedEventArgs e)
        {
            EmitterPin?.GetBindingExpression(Shape.FillProperty).UpdateTarget();
        }
    }
}
