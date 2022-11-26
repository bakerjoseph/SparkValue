using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueBackend.ViewModels.LessonInteractiveElements
{
    public class OhmsLawBasicViewModel : ViewModelBase  
    {
        private string _questionOneAnswer;
        public string QuestionOneAnswer
        {
            get { return _questionOneAnswer; }
            set 
            { 
                _questionOneAnswer = value; 
                OnPropertyChanged(nameof(QuestionOneAnswer));
            }
        }

        private string _questionTwoAnswer;
        public string QuestionTwoAnswer
        {
            get { return _questionTwoAnswer; }
            set
            {
                _questionTwoAnswer = value;
                OnPropertyChanged(nameof(QuestionTwoAnswer));
            }
        }

        private string _questionThreeAnswer;
        public string QuestionThreeAnswer
        {
            get { return _questionThreeAnswer; }
            set
            {
                _questionThreeAnswer = value;
                OnPropertyChanged(nameof(QuestionThreeAnswer));
            }
        }

        private string _questionFourAnswer;
        public string QuestionFourAnswer
        {
            get { return _questionFourAnswer; }
            set
            {
                _questionFourAnswer = value;
                OnPropertyChanged(nameof(QuestionFourAnswer));
            }
        }

        public OhmsLawBasicViewModel()
        {

        }
    }
}
