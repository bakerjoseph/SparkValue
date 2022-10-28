using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Resources;

namespace SparkValueDesktopApplication.Converters
{
    public class BreadboardCursorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values != null && values[0] is ToggleButton)
            {
                ToggleButton? wireTool = values[0] as ToggleButton;
                if (wireTool != null && wireTool.IsChecked != null)
                {
                    StreamResourceInfo streamResourceInfoCursor = Application.GetResourceStream(new Uri("\\Cursors\\Eraser.cur", UriKind.Relative));
                    return (bool)wireTool.IsChecked ? new Cursor(streamResourceInfoCursor.Stream) : Cursors.Pen ;
                }

            }
            return Cursors.Pen;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
