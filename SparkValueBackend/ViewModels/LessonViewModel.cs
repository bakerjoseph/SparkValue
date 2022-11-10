using SparkValueBackend.Commands;
using SparkValueBackend.Models;
using SparkValueBackend.Services;
using SparkValueBackend.Stores;
using SparkValueBackend.ViewModels.LessonContent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
                _displayedContent?.Dispose();
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
                               UserStore userStore,
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
                // Will not update db with this progress until the lesson is exited
                user.UpdateLessonProgress(targetProgress.ItemName, 1);
            }

            Progress = $"{targetProgress.Progress}/{_lesson.Content.Count}";

            CreateContent(targetProgress.Progress);

            CloseCommand = new NavigateAwayFromLessonCommand(this, null, userStore, user);
            MenuNavigateCommand = new NavigateAwayFromLessonCommand(this, dashboardViewNavigationService, userStore, user);
            SettingsNavigateCommand = new NavigateAwayFromLessonCommand(this, userSettingsViewNavigationService, userStore, user);
            LessonNavigateCommand = new NavigateCommand(new NavigationService(navigationStore, CreateLessonViewModel));

            PreviousPageCommand = new LessonIterateBackwardCommand(this);
            NextPageCommand = new LessonIterateForwardCommand(this);
        }

        private LessonViewModel CreateLessonViewModel()
        {
            return this;
        }

        /// <summary>
        /// Get a users progress through the lesson
        /// </summary>
        /// <returns>A users progress number</returns>
        public int GetLessonProgress()
        {
            // Convert "1/5" to "1", we just need the first number to update the users progress
            return int.Parse(Progress.Split("/")[0].Trim());
        }

        /// <summary>
        /// Increment the lesson progress, generates new displayed content
        /// </summary>
        public void IncrementLessonProgress()
        {
            // Increment progress
            int previousProgress = GetLessonProgress();
            Progress = $"{previousProgress + 1}/{_lesson.Content.Count}";

            // Generate new displayed content
            CreateContent(previousProgress + 1);
        }

        /// <summary>
        /// Decrement the lesson progress, generates new displayed content
        /// </summary>
        public void DecrementLessonProgress()
        {
            // Decrement progress
            int previousProgress = GetLessonProgress();
            Progress = $"{previousProgress - 1}/{_lesson.Content.Count}";

            // Generate new displayed content
            CreateContent(previousProgress - 1);
        }

        /// <summary>
        /// Can we traverse backwards through the lesson?
        /// </summary>
        /// <returns>A bool: True = yes | False = no</returns>
        public bool CanGoBack()
        {
            return GetLessonProgress() > 1;
        }

        /// <summary>
        /// Can we traverse forwards through the lesson?
        /// </summary>
        /// <returns>A bool: True = yes | False = no</returns>
        public bool CanGoForward()
        {
            return GetLessonProgress() < _lesson.Content.Count();
        }

        /// <summary>
        /// Uses an index to create a lesson content template to set the displayed content
        /// </summary>
        /// <param name="index">Lesson page index</param>
        public void CreateContent(int index)
        {
            int listIndex = index - 1;

            LessonContentViewModelBase template = null;

            // Are within the range of the lists in the lesson?
            if (listIndex >= 0 && listIndex < _lesson.Content.Count && listIndex < _lesson.InteractiveElementTitles.Count && listIndex < _lesson.TemplateIds.Count())
            {
                string content = (string.IsNullOrEmpty(_lesson.Content[listIndex])) ? "No Text Found" : _lesson.Content[listIndex];
                string interactiveElement = (string.IsNullOrEmpty(_lesson.InteractiveElementTitles[listIndex])) ? "No Interactive Element Found" : _lesson.InteractiveElementTitles[listIndex];
                
                // Create a template based on the id
                switch (_lesson.TemplateIds[listIndex])
                {
                    default:
                        // Falls into case 0, creates a default template
                    case 0:
                        // Default template, displays no interactive element
                        template = new LessonContentTemplateDefaultViewModel(content, interactiveElement);
                        break;
                    case 1:
                        // Template one
                        template = new LessonContentTemplateOneViewModel(content, interactiveElement);
                        break;
                    case 2:
                        // Template two
                        template = new LessonContentTemplateTwoViewModel(content, interactiveElement);
                        break;
                    case 3:
                        // Template three
                        template = new LessonContentTemplateThreeViewModel(content, interactiveElement);
                        break;
                    case 4:
                        // Template four
                        template = new LessonContentTemplateFourViewModel(content, interactiveElement);
                        break;
                }
            }
            // If we are not within the range, set the content to a default template
            else
            {
                Debug.WriteLine($"Index was out of range when trying to create content for {_lesson.Title}.");
                template = new LessonContentTemplateDefaultViewModel("Out of bounds", null);
            }

            DisplayedContent = template;
        }
    }
}