using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace SparkValueDesktopApplication.Converters
{
    public class WireThicknessColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values[0] is ColorPicker)
            {
                ColorPicker wireColor = values[0] as ColorPicker;
                return new SolidColorBrush((wireColor?.SelectedColor != null) ? (Color)wireColor.SelectedColor : Colors.Black);
            }
            else return Brushes.Black;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
