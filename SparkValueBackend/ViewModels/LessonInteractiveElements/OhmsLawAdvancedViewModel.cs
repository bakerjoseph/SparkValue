using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueBackend.ViewModels.LessonInteractiveElements
{
    public class OhmsLawAdvancedViewModel : ViewModelBase
    {
        private string _questionOneVoltageAnswer;
        public string QuestionOneVoltageAnswer
        {
            get { return _questionOneVoltageAnswer; }
            set 
            { 
                _questionOneVoltageAnswer = value;
                OnPropertyChanged(nameof(QuestionOneVoltageAnswer));
            }
        }

        private string _questionOneCurrentAnswer;
        public string QuestionOneCurrentAnswer
        {
            get { return _questionOneCurrentAnswer; }
            set
            {
                _questionOneCurrentAnswer = value;
                OnPropertyChanged(nameof(QuestionOneCurrentAnswer));
            }
        }

        private string _questionOneResistanceAnswer;
        public string QuestionOneResistanceAnswer
        {
            get { return _questionOneResistanceAnswer; }
            set
            {
                _questionOneResistanceAnswer = value;
                OnPropertyChanged(nameof(QuestionOneResistanceAnswer));
            }
        }

        public OhmsLawAdvancedViewModel()
        {

        }
    }
}
