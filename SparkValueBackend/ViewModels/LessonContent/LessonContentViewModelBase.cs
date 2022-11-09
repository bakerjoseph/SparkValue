using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueBackend.ViewModels.LessonContent
{
    public class LessonContentViewModelBase : ViewModelBase
    {
        private string _plainText;
        public string PlainText
        {
            get
            {
                return _plainText;
            }
            set
            {
                _plainText = value;
                OnPropertyChanged(nameof(PlainText));
            }
        }

        private string? _interactiveElement;
        public string? InteractiveElement
        {
            get 
            { 
                return _interactiveElement; 
            }
            set
            {
                _interactiveElement = value;
                OnPropertyChanged(nameof(InteractiveElement));
            }
        }

        public LessonContentViewModelBase(string plainText, string? interactiveElement)
        {
            PlainText = plainText;
            InteractiveElement = interactiveElement;
        }
    }
}
