using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SparkValueBackend.ViewModels.LessonInteractiveElements
{
    public class NOTsXORsViewModel : ViewModelBase
    {
        #region NOT I/O
        private bool _inputOneStateNOT;
        public bool InputOneStateNOT
        {
            get { return _inputOneStateNOT; }
            set 
            { 
                _inputOneStateNOT = value;
                OnPropertyChanged(nameof(InputOneStateNOT));
            }
        }

        public Brush NOTOutput => GetOutputOfNOT();
        #endregion

        #region XOR I/O
        private bool _inputOneStateXOR;
        public bool InputOneStateXOR
        {
            get { return _inputOneStateXOR; }
            set
            {
                _inputOneStateXOR = value;
                OnPropertyChanged(nameof(InputOneStateXOR));
            }
        }

        private bool _inputTwoStateXOR;
        public bool InputTwoStateXOR
        {
            get { return _inputTwoStateXOR; }
            set
            {
                _inputTwoStateXOR = value;
                OnPropertyChanged(nameof(InputTwoStateXOR));
            }
        }

        public Brush XOROutput => GetOutputOfXOR();
        #endregion

        public NOTsXORsViewModel()
        {

        }

        private Brush GetOutputOfNOT()
        {
            return (!InputOneStateNOT) ? Brushes.Lime : Brushes.DarkRed;
        }

        private Brush GetOutputOfXOR()
        {
            return (InputOneStateXOR ^ InputTwoStateXOR) ? Brushes.Lime : Brushes.DarkRed;
        }
    }
}
