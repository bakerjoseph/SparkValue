using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SparkValueDesktopApplication.ValidationRules
{
    public class PasswordRule : ValidationRule
    {
        private readonly string AntiUppercaseRegex = @"^(.{0,7}|[^A-Z]*)$";
        private readonly string AntiLowerCaseRegex = @"^(.{0,7}|[^a-z]*)$";
        private readonly string AntiNumericalRegex = @"^(.{0,7}|[^0-9]*)$";
        private readonly string AntiSpecialRegex = @"^(.{0,7}|[A-Z0-9]*)$";
        private readonly int MinPasswordLength = 8;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            // Is the length of the password greater than 8?
            if (((value is SecureString) ? ConvertSecureString((SecureString)value) : (string)value).Length < MinPasswordLength)
            {
                return new ValidationResult(false, $"Password must contain {MinPasswordLength} characters or more.");
            }

            // Does the password contain at least one uppercase character?
            Regex inverseUppercaseRegex = new Regex(AntiUppercaseRegex);
            if (inverseUppercaseRegex.IsMatch((value is SecureString) ? ConvertSecureString((SecureString)value) : (string)value))
            {
                // Will match if there are no uppercase letters
                return new ValidationResult(false, "Password must contain at least one uppercase letter.");
            }

            // Does the password contain at least one lowercase character?
            Regex inverseLowerCaseRegex = new Regex(AntiLowerCaseRegex);
            if (inverseLowerCaseRegex.IsMatch((value is SecureString) ? ConvertSecureString((SecureString)value) : (string)value))
            {
                // Will match if there are no lower case letters
                return new ValidationResult(false, "Password must contain at least one lowercase letter.");
            }

            // Does the password contain at least one numerical character?
            Regex inverseNumericalRegex = new Regex(AntiNumericalRegex);
            if (inverseNumericalRegex.IsMatch((value is SecureString) ? ConvertSecureString((SecureString)value) : (string)value))
            {
                // Will match if there are no numerical characters
                return new ValidationResult(false, "Password must contain at least one numerical character.");
            }

            // Does the password contain at least one special character?
            Regex inverseSpecialRegex = new Regex(AntiSpecialRegex, RegexOptions.IgnoreCase);
            if (inverseSpecialRegex.IsMatch((value is SecureString) ? ConvertSecureString((SecureString)value) : (string)value))
            {
                // Will match if there are no special characters, ie. characters other than a-z or 0-9 ignoring case
                return new ValidationResult(false, "Password must contain at least one special character.");
            }

            return new ValidationResult(true, null);
        }

        private string ConvertSecureString(SecureString value)
        {
            byte[] returnBytes = new byte[value.Length];

            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(value);
                for (int i = 0; i < value.Length; i++)
                {
                    short unicodeChar = System.Runtime.InteropServices.Marshal.ReadInt16(valuePtr, i * 2);
                    returnBytes[i] = Convert.ToByte(unicodeChar);
                }

                return Encoding.Default.GetString(returnBytes);
            }
            finally
            {
                System.Runtime.InteropServices.Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }
    }
}
