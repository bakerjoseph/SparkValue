using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SparkValueBackend.ViewModels.LessonInteractiveElements
{
    public class SwitchingViewModel : ViewModelBase
    {
        #region Switch I/O
        private bool _inputOneState;
        public bool InputOneState
        {
            get { return _inputOneState; }
            set 
            { 
                _inputOneState = value; 
                OnPropertyChanged(nameof(InputOneState));
            }
        }

        public Brush SwitchOutput => GetOutputOfSwitch();
        #endregion

        public SwitchingViewModel()
        {

        }

        private Brush GetOutputOfSwitch()
        {
            return (InputOneState) ? Brushes.Lime : Brushes.DarkRed;
        }
    }
}
