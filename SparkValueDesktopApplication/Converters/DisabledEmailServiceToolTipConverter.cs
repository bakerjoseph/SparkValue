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
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is SignInViewModel)
            {
                SignInViewModel viewModel = value as SignInViewModel;

                return (!viewModel.EmailingStatus) ? "Email service is unavailable." : "";
            }
            else if (value is SettingsAccountViewModel)
            {
                SettingsAccountViewModel viewModel = value as SettingsAccountViewModel;

                return (!viewModel.EmailingStatus) ? "Email service is unavailable." : "";
            }
            else return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
