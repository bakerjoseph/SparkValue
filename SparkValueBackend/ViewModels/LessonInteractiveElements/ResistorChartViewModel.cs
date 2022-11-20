using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SparkValueBackend.ViewModels.LessonInteractiveElements
{
    public class ResistorChartViewModel : ViewModelBase
    {
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

        public ResistorChartViewModel()
        {
            // Defaults
            // 4 Band is 1 Ohm plus or minus 10 percent
            // 5 Band is 10 Ohms plus or minus 10 percent
            // 6 Band is 10 Ohms plus or minus 10 percent with 50 ppm
            BandOne = Brushes.Black;
            BandTwo = Brushes.SaddleBrown;
            BandThree = Brushes.Black;

            BandFour = Brushes.Black;

            BandFive = Brushes.Silver;

            BandSix = Brushes.Red;
        }
    }
}
