using SparkValueBackend.Commands;
using SparkValueBackend.Models;
using SparkValueBackend.Services;
using SparkValueBackend.Stores;
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
        public ICommand LessonNavigateCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand NextPageCommand { get; }

        public LessonViewModel(NavigationStore navigationStore, 
                               NavigationService dashboardViewNavigationService, 
                               NavigationService userSettingsViewNavigationService,
                               string username, 
                               LessonModel lesson)
        {
            _content = new ObservableCollection<string>(lesson.Content);
            //_interactiveElements = new ObservableCollection<ViewModelBase>(lesson.InteractiveElementTitles);

            Username = username;
            Title = lesson.Title;
            Progress = $"0/{_content.Count}";

            MenuNavigateCommand = new NavigateCommand(dashboardViewNavigationService);
            SettingsNavigateCommand = new NavigateCommand(userSettingsViewNavigationService);
            LessonNavigateCommand = new NavigateCommand(new NavigationService(navigationStore, CreateLessonViewModel));

            PreviousPageCommand = new LessonIterateBackwardCommand(this);
            NextPageCommand = new LessonIterateForwardCommand(this);
        }

        private LessonViewModel CreateLessonViewModel()
        {
            return this;
        }
    }
}