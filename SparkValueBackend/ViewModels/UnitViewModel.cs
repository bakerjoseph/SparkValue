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

        private readonly ObservableCollection<LessonViewModel> _lessons;
        public IEnumerable<LessonViewModel> Lessons => _lessons;

        public UnitViewModel(NavigationStore navigationStore,
                             NavigationService dashboardViewNavigationService, 
                             NavigationService userSettingsViewNavigationService,
                             UnitModel unit,
                             UserAccountModel user)
        {
            Title = unit.Title;
            Description = unit.Description;

            _lessons = new ObservableCollection<LessonViewModel>();
            foreach (var lesson in unit.Lessons)
            {
                _lessons.Add(new LessonViewModel(navigationStore, dashboardViewNavigationService, userSettingsViewNavigationService, lesson, user));
            }
        }
    }
}