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

        public ICommand ChangeBand { get; }

        public ResistorChartViewModel()
        {
            // Defaults
            // 4 Band is 0 Ohm plus or minus 10 percent
            // 5 Band is 0 Ohms plus or minus 10 percent
            // 6 Band is 0 Ohms plus or minus 10 percent with 100 ppm
            BandOne = Brushes.Black;
            BandTwo = Brushes.Black;
            BandThree = Brushes.Black;

            BandFour = Brushes.Black;

            BandFive = Brushes.Silver;

            BandSix = Brushes.SaddleBrown;

            ChangeBand = new ChangeResistorBandCommand(this);
        }
    }
}
