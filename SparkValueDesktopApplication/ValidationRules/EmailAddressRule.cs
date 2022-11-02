using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SparkValueDesktopApplication.ValidationRules
{
    public class EmailAddressRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            // Is the email valid?
            if (!MailAddress.TryCreate((string)value, out var mailAddress))
            {
                return new ValidationResult(false, "Email address must be valid");
            }

            return new ValidationResult(true, null);
        }
    }
}
