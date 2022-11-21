using SparkValueBackend.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;

namespace SparkValueBackend.ViewModels.LessonInteractiveElements
{
    public class ResistorChartViewModel : ViewModelBase
    {
        #region Band One
        private Brush _bandOne;
        public Brush BandOne
        {
            get { return _bandOne; }
            set
            {
                _bandOne = value;
                OnPropertyChanged(nameof(BandOne));
            }
        }

        private string _bandOneValue;
        public string BandOneValue
        {
            get { return _bandOneValue; }
            set
            {
                _bandOneValue = value;
                OnPropertyChanged(nameof(BandOneValue));
            }
        }
        #endregion

        #region Band Two
        private Brush _bandTwo;
        public Brush BandTwo
        {
            get { return _bandTwo; }
            set
            {
                _bandTwo = value;
                OnPropertyChanged(nameof(BandTwo));
            }
        }

        private string _bandTwoValue;
        public string BandTwoValue
        {
            get { return _bandTwoValue; }
            set
            {
                _bandTwoValue = value;
                OnPropertyChanged(nameof(BandTwoValue));
            }
        }
        #endregion

        #region Band Three
        private Brush _bandThree;
        public Brush BandThree
        {
            get { return _bandThree; }
            set
            {
                _bandThree = value;
                OnPropertyChanged(nameof(BandThree));
            }
        }

        private string _bandThreeValue;
        public string BandThreeValue
        {
            get { return _bandThreeValue; }
            set
            {
                _bandThreeValue = value;
                OnPropertyChanged(nameof(BandThreeValue));
            }
        }

        private bool _bandThreeVisible;
        public bool BandThreeVisible
        {
            get { return _bandThreeVisible; }
            set
            {
                _bandThreeVisible = value;
                OnPropertyChanged(nameof(BandThreeVisible));
            }
        }
        #endregion

        #region Band Four
        private Brush _bandFour;
        public Brush BandFour
        {
            get { return _bandFour; }
            set
            {
                _bandFour = value;
                OnPropertyChanged(nameof(BandFour));
            }
        }

        private string _bandFourValue;
        public string BandFourValue
        {
            get { return _bandFourValue; }
            set
            {
                _bandFourValue = value;
                OnPropertyChanged(nameof(BandFourValue));
            }
        }
        #endregion

        #region Band Five
        private Brush _bandFive;
        public Brush BandFive
        {
            get { return _bandFive; }
            set
            {
                _bandFive = value;
                OnPropertyChanged(nameof(BandFive));
            }
        }

        private string _bandFiveValue;
        public string BandFiveValue
        {
            get { return _bandFiveValue; }
            set
            {
                _bandFiveValue = value;
                OnPropertyChanged(nameof(BandFiveValue));
            }
        }
        #endregion

        #region Band Six
        private Brush _bandSix;
        public Brush BandSix
        {
            get { return _bandSix; }
            set
            {
                _bandSix = value;
                OnPropertyChanged(nameof(BandSix));
            }
        }

        private string _bandSixValue;
        public string BandSixValue
        {
            get { return _bandSixValue; }
            set
            {
                _bandSixValue = value;
                OnPropertyChanged(nameof(BandSixValue));
            }
        }

        private bool _bandSixVisible;
        public bool BandSixVisible
        {
            get { return _bandSixVisible; }
            set
            {
                _bandSixVisible = value;
                OnPropertyChanged(nameof(BandSixVisible));
            }
        }
        #endregion

        public string Result => CalculateResistance();

        public ICommand ChangeBand { get; }

        public ResistorChartViewModel()
        {
            // Defaults
            // 4 Band is 0 Ohm plus or minus 10 percent
            // 5 Band is 0 Ohms plus or minus 10 percent
            // 6 Band is 0 Ohms plus or minus 10 percent with 100 ppm
            BandOne = Brushes.Black;
            BandOneValue = "0";
            BandTwo = Brushes.Black;
            BandTwoValue = "0";
            BandThree = Brushes.Black;
            BandThreeValue = "0";

            BandFour = Brushes.Silver;
            BandFourValue = "0.01";

            BandFive = Brushes.Silver;
            BandFiveValue = "\u00B1 10%";

            BandSix = Brushes.SaddleBrown;
            BandSixValue = "100 PPM";

            ChangeBand = new ChangeResistorBandCommand(this);
        }

        private string CalculateResistance()
        {
            // Concat the digits and parse them to a double
            string startString = BandOneValue + BandTwoValue + ((BandThreeVisible) ? BandThreeValue : string.Empty);
            double resultNum = double.Parse(startString);

            // Calculate the value multiplier
            double multiplier = 0;
            // Does the value of band four contain a suffix already?
            if (double.TryParse(BandFourValue, out double mult))
            {
                multiplier = mult;
            }
            else
            {
                // Find the appropriate multiplier value
                switch (BandFourValue)
                {
                    case "1K":
                        multiplier = 1000;
                        break;
                    case "10K":
                        multiplier = 10000;
                        break;
                    case "100K":
                        multiplier = 100000;
                        break;
                    case "1M":
                        multiplier = 1000000;
                        break;
                    case "10M":
                        multiplier = 10000000;
                        break;
                }
            }

            // Return the whole sequence
            return $"Result: {GetFormattedResistance(resultNum, multiplier)} {BandFiveValue} {((BandSixVisible)? BandSixValue : string.Empty) }";
        }

        private string GetFormattedResistance(double baseNum, double multiplier)
        {
            double resultNum = (double)decimal.Multiply((decimal)baseNum, (decimal)multiplier);
            string resultNumString = resultNum.ToString();
            string result;
            string suffix;

            // Billions Ohm Range
            if (resultNum > 999999999)
            {
                suffix = "G";
                result = ResistanceFormater(resultNumString, 9);
            }
            // Millions Ohm Range
            else if (resultNum > 999999)
            {
                suffix = "M";
                result = ResistanceFormater(resultNumString, 6);
            }
            // Thousands Ohm Range
            else if (resultNum > 999)
            {
                suffix = "K";
                result = ResistanceFormater(resultNumString, 3);
            }
            // Hundreds or Less Than Zero Ohm Range
            else
            {
                suffix = string.Empty;
                result = resultNum.ToString();
            }

            return $"{result} {suffix}\u03A9";
        }

        private string ResistanceFormater(string number, int offsetValue)
        {
            // Formats the resistance from a full number to a condensed format, suffix is already taken care of
            // 27,000 becomes 27 
            // 1,500 becomes 1.5
            string lesserPart = number.Substring(number.Length - offsetValue);
            return number.Substring(0, number.Length - offsetValue) + ((double.Parse(lesserPart) > 0) ? $".{ReplaceTrailingZeros(lesserPart)}" : string.Empty);
        }
        
        private string ReplaceTrailingZeros(string number)
        {
            // Removes all trailing zeros, leaving leading zeros
            // 010 becomes 01
            // 100 becomes 1
            return number.Substring(0, number.IndexOfAny("123456789".ToCharArray()) + 1);
        }
    }
}
