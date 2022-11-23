using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SparkValueBackend.ViewModels.LessonInteractiveElements
{
    public class ANDsORsViewModel : ViewModelBase
    {
        #region AND I/O
        private bool _inputOneStateAND;
        public bool InputOneStateAND
        {
            get { return _inputOneStateAND; }
            set 
            { 
                _inputOneStateAND = value;
                OnPropertyChanged(nameof(InputOneStateAND));
            }
        }

        private bool _inputTwoStateAND;
        public bool InputTwoStateAND
        {
            get { return _inputTwoStateAND; }
            set
            {
                _inputTwoStateAND = value;
                OnPropertyChanged(nameof(InputTwoStateAND));
            }
        }

        public Brush ANDOutput => GetOutputOfAND();
        #endregion

        #region OR I/O
        private bool _inputOneStateOR;
        public bool InputOneStateOR
        {
            get { return _inputOneStateOR; }
            set
            {
                _inputOneStateOR = value;
                OnPropertyChanged(nameof(InputOneStateOR));
            }
        }

        private bool _inputTwoStateOR;
        public bool InputTwoStateOR
        {
            get { return _inputTwoStateOR; }
            set
            {
                _inputTwoStateOR = value;
                OnPropertyChanged(nameof(InputTwoStateOR));
            }
        }

        public Brush OROutput => GetOutputOfOR();
        #endregion

        public ANDsORsViewModel()
        {

        }

        private Brush GetOutputOfAND()
        {
            return (InputOneStateAND && InputTwoStateAND) ? Brushes.Lime : Brushes.DarkRed;
        }

        private Brush GetOutputOfOR()
        {
            return (InputOneStateOR || InputTwoStateOR) ? Brushes.Lime : Brushes.DarkRed;
        }
    }
}
