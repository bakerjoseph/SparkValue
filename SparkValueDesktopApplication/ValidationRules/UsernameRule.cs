using SparkValueBackend.Stores;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SparkValueDesktopApplication.ValidationRules
{
    public class UsernameRule : ValidationRule
    {
        private readonly string AntiUsernamePattern = @"[^a-z0-9]";
        private readonly int MinUsernameLength = 8;
        private readonly int MaxUsernameLength = 25;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            // Inverse meaning it is looking for anything other than 0-9 and a-z
            Regex inverseUsernameRegex = new Regex(AntiUsernamePattern, RegexOptions.IgnoreCase);
            string inputUsername = (string)value;

            // Is the length of the username less than 25 and greater than 8?
            if (inputUsername.Length < MinUsernameLength || inputUsername.Length > MaxUsernameLength)
            {
                return new ValidationResult(false, $"Username must contain {MinUsernameLength} to {MaxUsernameLength} characters.");
            }

            // Does the username contains symbols or characters that are not numbers or letters?
            if (inverseUsernameRegex.IsMatch(inputUsername))
            {
                // Will match if there are characters other than 0-9 or a-z
                return new ValidationResult(false, "Username must contain only alphabet and numerical characters.");
            }

            return new ValidationResult(true, null);
        }
    }
}
