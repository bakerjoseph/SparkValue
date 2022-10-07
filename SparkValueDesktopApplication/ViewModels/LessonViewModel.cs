using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SparkValueDesktopApplication.ViewModels
{
    public class LessonViewModel : ViewModelBase
    {
        private string _username;
        public string Username
        {
            get 
            { 
                return _username; 
            }
            set 
            { 
                _username = value; 
                OnPropertyChanged(nameof(Username));
            }
        }

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

        private string _progress;
        public string Progress
        {
            get 
            { 
                return _progress; 
            }
            set 
            { 
                _progress = value; 
                OnPropertyChanged(nameof(Progress)); 
            }
        }

        private ObservableCollection<string> _content;
        public IEnumerable<string> Content => _content;

        private ObservableCollection<ViewModelBase> _interactiveElements;
        public IEnumerable<ViewModelBase> InteractiveElements => _interactiveElements;

        public ICommand LessonNavigateCommand { get; }
        public ICommand MenuNavigateCommand { get; }
        public ICommand SettingsNavigateCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand NextPageCommand { get; }

        public LessonViewModel(string username, string title, string description, List<string> content, List<ViewModelBase> interactiveElements)
        {
            _content = new ObservableCollection<string>(content);
            _interactiveElements = new ObservableCollection<ViewModelBase>(interactiveElements);

            Username = username;
            Title = title;
            Description = description;
            Progress = $"1/{_content.Count}";
        }
    }
}