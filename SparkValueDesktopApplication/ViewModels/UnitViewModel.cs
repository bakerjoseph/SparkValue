using SparkValueDesktopApplication.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SparkValueDesktopApplication.ViewModels
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

        private readonly ObservableCollection<LessonViewModel> _lessons;
        public IEnumerable<LessonViewModel> Lessons => _lessons;

        public UnitViewModel(string title, string description, List<LessonViewModel> lessons)
        {
            Title = title;
            Description = description;

            _lessons = new ObservableCollection<LessonViewModel>(lessons);
        }
    }
}