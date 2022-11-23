using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SparkValueDesktopApplication.Converters
{
    public class ButtonLightConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values[0] is RepeatButton && values[1] is Ellipse)
            {
                RepeatButton button = values[0] as RepeatButton;
                Ellipse output = values[1] as Ellipse;

                if (output.Fill == null) return Brushes.DarkRed;
                else if (button.IsPressed && button.IsEnabled && output.Fill.Equals(Brushes.DarkRed)) return Brushes.Lime;
                else if (button.IsPressed && button.IsEnabled && output.Fill.Equals(Brushes.Lime)) return Brushes.Lime;
                else return Brushes.DarkRed;
            }
            else return Brushes.DarkRed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
