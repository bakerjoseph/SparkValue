using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SparkValueBackend.ViewModels.LessonInteractiveElements
{
    public class TransistorWalkthroughViewModel : ViewModelBase
    {
        #region Transistor I/O
        private bool _collectorPin;
        public bool CollectorPin
        {
            get { return _collectorPin; }
            set 
            { 
                _collectorPin = value; 
                OnPropertyChanged(nameof(CollectorPin));
            }
        }

        private bool _basePin;
        public bool BasePin
        {
            get { return _basePin; }
            set
            {
                _basePin = value;
                OnPropertyChanged(nameof(BasePin));
            }
        }

        public Brush EmitterPin => GetOutputOfTransistor();
        #endregion

        public TransistorWalkthroughViewModel()
        {

        }

        private Brush GetOutputOfTransistor()
        {
            return (CollectorPin && BasePin) ? Brushes.Lime : Brushes.DarkRed;
        }
    }
}
