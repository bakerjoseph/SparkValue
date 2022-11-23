using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SparkValueBackend.ViewModels.LessonInteractiveElements
{
    public class NANDsNORsViewModel : ViewModelBase
    {
        #region NAND I/O
        private bool _inputOneStateNAND;
        public bool InputOneStateNAND
        {
            get { return _inputOneStateNAND; }
            set 
            { 
                _inputOneStateNAND = value; 
                OnPropertyChanged(nameof(InputOneStateNAND));
            }
        }

        private bool _inputTwoStateNAND;
        public bool InputTwoStateNAND
        {
            get { return _inputTwoStateNAND; }
            set
            {
                _inputTwoStateNAND = value;
                OnPropertyChanged(nameof(InputTwoStateNAND));
            }
        }

        public Brush NANDOutput => GetOutputOfNAND();
        #endregion

        #region NOR I/O
        private bool _inputOneStateNOR;
        public bool InputOneStateNOR
        {
            get { return _inputOneStateNOR; }
            set
            {
                _inputOneStateNOR = value;
                OnPropertyChanged(nameof(InputOneStateNOR));
            }
        }

        private bool _inputTwoStateNOR;
        public bool InputTwoStateNOR
        {
            get { return _inputTwoStateNOR; }
            set
            {
                _inputTwoStateNOR = value;
                OnPropertyChanged(nameof(InputTwoStateNOR));
            }
        }

        public Brush NOROutput => GetOutputOfNOR();
        #endregion

        public NANDsNORsViewModel()
        {

        }

        private Brush GetOutputOfNAND()
        {
            return (!(InputOneStateNAND && InputTwoStateNAND)) ? Brushes.Lime : Brushes.DarkRed;
        }

        private Brush GetOutputOfNOR()
        {
            return (!(InputOneStateNOR || InputTwoStateNOR)) ? Brushes.Lime : Brushes.DarkRed;
        }
    }
}
