using SparkValueBackend.Models;
using SparkValueBackend.Services;
using SparkValueBackend.Stores;
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

        private double _currentProgress;
        public double CurrentProgress
        {
            get { return _currentProgress; }
            set
            {
                _currentProgress = value;
                OnPropertyChanged(nameof(CurrentProgress));
            }
        }

        private double _progressGoal;
        public double ProgressGoal
        {
            get { return _progressGoal; }
            set
            {
                _progressGoal = value;
                OnPropertyChanged(nameof(ProgressGoal));
            }
        }

        private readonly ObservableCollection<LessonViewModel> _lessons;
        public IEnumerable<LessonViewModel> Lessons => _lessons;

        public UnitViewModel(NavigationStore navigationStore,
                             UserStore userStore,
                             NavigationService dashboardViewNavigationService, 
                             NavigationService userSettingsViewNavigationService,
                             UnitModel unit,
                             UserAccountModel user)
        {
            Title = unit.Title;
            Description = unit.Description;

            _lessons = new ObservableCollection<LessonViewModel>();
            int totalPages = 0;
            int totalCompleted = 0;
            foreach (var lesson in unit.Lessons)
            {
                // Create lessons
                _lessons.Add(new LessonViewModel(navigationStore, userStore, dashboardViewNavigationService, userSettingsViewNavigationService, lesson, user));
               
                // Total the ammount of pages there are in each lesson and the count of those completed
                totalPages += lesson.Content.Count;
                totalCompleted += user.LessonProgress.First(l => l.ItemName.Equals(lesson.Title)).Progress;
            }

            // Update the user's unit progress
            UpdateUnitProgress(userStore, user, Title, totalCompleted);

            // Get the values for the progres bar
            ProgressModel status = user.AccountOverallProgress.First(u => u.ItemName.Equals(unit.Title));
            CurrentProgress = ((double)decimal.Divide(status.Progress, totalPages)) * totalPages;
            ProgressGoal = totalPages;
        }

        private async void UpdateUnitProgress(UserStore userStore, UserAccountModel user, string unitTitle, int progress)
        {
            await userStore.UpdateUserUnitProgress(user, unitTitle, progress);
        }
    }
}