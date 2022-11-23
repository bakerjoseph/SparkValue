using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for ButtonsView.xaml
    /// </summary>
    public partial class ButtonsView : UserControl
    {
        public ButtonsView()
        {
            InitializeComponent();
        }

        private void Button_ChangeState(object sender, MouseButtonEventArgs e)
        {
            if (ButtonOutput != null)
            {
                MultiBindingExpression outputBinding = BindingOperations.GetMultiBindingExpression(ButtonOutput, Shape.FillProperty);
                outputBinding.UpdateTarget();

                if (e.LeftButton == MouseButtonState.Released)
                {
                    ButtonInput.IsEnabled = false;
                    outputBinding.UpdateTarget();
                    ButtonInput.IsEnabled = true;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonOutput != null)
            {
                MultiBindingExpression outputBinding = BindingOperations.GetMultiBindingExpression(ButtonOutput, Shape.FillProperty);
                outputBinding.UpdateTarget();
            }
        }
    }
}
