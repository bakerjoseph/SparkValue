using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SparkValueDesktopApplication.ValidationRules.OhmsLaw
{
    public class OhmsLawBasicFourRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string inputValue = value as string;

            // Remove spaces from answer
            inputValue = inputValue.Trim();

            // Is the value passed in empty?
            if (string.IsNullOrEmpty(inputValue)) return new ValidationResult(false, "The answer can not be blank.");

            // Is the value passed in the expected result?
            if (!(inputValue.Equals("0.5") || inputValue.Equals(".5"))) return new ValidationResult(false, "That is not the correct answer, try again.");

            return new ValidationResult(true, "");
        }
    }
}
