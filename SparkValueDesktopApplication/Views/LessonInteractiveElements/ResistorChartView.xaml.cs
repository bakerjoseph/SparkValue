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
    }
}
