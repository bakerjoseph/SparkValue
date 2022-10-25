using SparkValueBackend.Commands;
using SparkValueBackend.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SparkValueBackend.ViewModels
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

        public ICommand MenuNavigateCommand { get; }
        public ICommand SettingsNavigateCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand NextPageCommand { get; }

        public LessonViewModel(NavigationService dashboardViewNavigationService, NavigationService userSettingsViewNavigationService, string username, string title, List<string> content, List<ViewModelBase> interactiveElements)
        {
            _content = new ObservableCollection<string>(content);
            _interactiveElements = new ObservableCollection<ViewModelBase>(interactiveElements);

            Username = username;
            Title = title;
            Progress = $"1/{_content.Count}";

            MenuNavigateCommand = new NavigateCommand(dashboardViewNavigationService);
            SettingsNavigateCommand = new NavigateCommand(userSettingsViewNavigationService);

            PreviousPageCommand = new LessonIterateBackwardCommand(this);
            NextPageCommand = new LessonIterateForwardCommand(this);

        }
    }
}