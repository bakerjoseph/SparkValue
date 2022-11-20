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
    public class ComboBoxVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values[0] is ComboBox)
            {
                ComboBox selector = values[0] as ComboBox;

                // If there are no items, hide the combobox
                if (!selector.HasItems) return Visibility.Hidden;

                string selection = ((ComboBoxItem)selector.SelectedItem).Content as string;

                // Have we selected the six band configuration?
                if (selection.Equals("6-Bands") && (parameter.Equals("six") || parameter.Equals("three"))) return Visibility.Visible;
                // Have we selected the five band configuration?
                if (selection.Equals("5-Bands") && parameter.Equals("three")) return Visibility.Visible;
                // Otherwise return hidden
                else return Visibility.Hidden;
            }
            else return Visibility.Hidden;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
