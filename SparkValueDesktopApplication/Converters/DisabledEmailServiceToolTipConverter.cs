using SparkValueBackend.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SparkValueDesktopApplication.Converters
{
    public class DisabledEmailServiceToolTipConverter : IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is SignInViewModel)
            {
                SignInViewModel viewModel = value as SignInViewModel;
                // Null is intended, this prevents an empty tooltip
                return (!viewModel.EmailingStatus) ? "Email service is unavailable." : null;
            }
            else if (value is SettingsAccountViewModel)
            {
                SettingsAccountViewModel viewModel = value as SettingsAccountViewModel;
                // Null is intended, this prevents an empty tooltip
                return (!viewModel.EmailingStatus) ? "Email service is unavailable." : null;
            }
            // Null is intended, this prevents an empty tooltip
            else return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
