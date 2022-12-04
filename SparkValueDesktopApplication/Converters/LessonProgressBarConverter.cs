using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SparkValueDesktopApplication.Converters
{
    public class LessonProgressBarConverter : IValueConverter
    {
        private readonly string ProgressRegex = @"^\d*/\d*$";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Regex progressSet = new Regex(ProgressRegex);
            if (progressSet.IsMatch((string)value))
            {
                string progressValue = value as string;

                // Get the starting and ending numbers of the progress value
                double progress = double.Parse(progressValue.Substring(0, progressValue.IndexOf('/')));
                double total = double.Parse(progressValue.Substring(progressValue.IndexOf('/') + 1));

                // Shortcut if we have not started a lesson
                if (progress == 0) return 0;

                // Divide the starting number by the ending number to get our progress out of a hundred
                return ((double)decimal.Divide((decimal)progress, (decimal)total)) * 100;
            }
            // Return 0 if the value is not an accepted format of number/number (Ex: 1/1 or 23/50)
            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
