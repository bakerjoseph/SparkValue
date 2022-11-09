using SparkValueBackend.Commands;
using SparkValueBackend.Models;
using SparkValueBackend.Services;
using SparkValueBackend.Stores;
using SparkValueBackend.ViewModels.LessonContent;
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

        private LessonModel _lesson;

        private LessonContentViewModelBase _displayedContent;
        public LessonContentViewModelBase DisplayedContent 
        { 
            get 
            { 
                return _displayedContent; 
            }
            set
            {
                _displayedContent = value;
                OnPropertyChanged(nameof(DisplayedContent));
            }
        }

        public ICommand CloseCommand { get; }
        public ICommand MenuNavigateCommand { get; }
        public ICommand SettingsNavigateCommand { get; }
        public ICommand LessonNavigateCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand NextPageCommand { get; }

        public LessonViewModel(NavigationStore navigationStore, 
                               NavigationService dashboardViewNavigationService, 
                               NavigationService userSettingsViewNavigationService, 
                               LessonModel lesson,
                               UserAccountModel user)
        {
            _lesson = lesson;

            Username = user.Username;
            Title = lesson.Title;
            Description = lesson.Description;

            ProgressModel targetProgress = user.LessonProgress.First(p => p.ItemName.Equals(lesson.Title));
            // Check the progress on the lesson, if it is zero, set it to one and diplay it
            if (targetProgress.Progress == 0)
            {
                user.UpdateLessonProgress(targetProgress.ItemName, 1);
            }

            Progress = $"{targetProgress.Progress}/{_lesson.Content.Count}";

            CloseCommand = new NavigateAwayFromLessonCommand(this, null, user);
            MenuNavigateCommand = new NavigateAwayFromLessonCommand(this, dashboardViewNavigationService, user);
            SettingsNavigateCommand = new NavigateAwayFromLessonCommand(this, userSettingsViewNavigationService, user);
            LessonNavigateCommand = new NavigateCommand(new NavigationService(navigationStore, CreateLessonViewModel));

            PreviousPageCommand = new LessonIterateBackwardCommand(this);
            NextPageCommand = new LessonIterateForwardCommand(this);
        }

        private LessonViewModel CreateLessonViewModel()
        {
            return this;
        }

        public int GetLessonProgress()
        {
            // Convert "1/5" to "1", we just need the first number to update the users progress
            return int.Parse(Progress.Split("/")[0].Trim());
        }

        public bool CanGoBack()
        {
            return GetLessonProgress() > _lesson.Content.Count();
        }

        public bool CanGoForward()
        {
            return GetLessonProgress() < _lesson.Content.Count();
        }
    }
}