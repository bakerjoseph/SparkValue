using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SparkValueDesktopApplication.Converters
{
    public class ResistorColorBandVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values[0] is ComboBox)
            {
                ComboBox selector = values[0] as ComboBox;

                // Does the selector have items?
                if (!selector.HasItems) return Visibility.Hidden;

                // Otherwise return the visibility of the combobox this band is tied to
                return selector.Visibility;
            }
            else return Visibility.Hidden;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
