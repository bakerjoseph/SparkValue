using SparkValueBackend.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SparkValueBackend.ViewModels
{
    public class UnitViewModel : ViewModelBase
    {
        private string _title;
        public string Title
        {
            get 
            { 
                return _title; 
            }
            set 
            { 
                _title = value; 
                OnPropertyChanged(nameof(Title));
            }
        }

        private string _description;
        public string Description
        {
            get 
            { 
                return _description; 
            }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private readonly ObservableCollection<PartialLessonViewModel> _lessons;
        public IEnumerable<PartialLessonViewModel> Lessons => _lessons;
        // Change this to accept the models and build the partial lesson views from the lesson model
        public UnitViewModel(string title, string description, List<PartialLessonViewModel> lessons)
        {
            Title = title;
            Description = description;

            _lessons = new ObservableCollection<PartialLessonViewModel>(lessons);
        }
    }
}