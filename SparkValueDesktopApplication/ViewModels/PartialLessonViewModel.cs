using SparkValueDesktopApplication.Commands;
using SparkValueDesktopApplication.Services;
using SparkValueDesktopApplication.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SparkValueDesktopApplication.ViewModels
{
    public class PartialLessonViewModel : ViewModelBase
    {
        private readonly NavigationService _dashboardViewNavigationService;
        private readonly NavigationService _userSettingsViewNavigationService;

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

        public ICommand LessonNavigateCommand { get; }

        public PartialLessonViewModel(NavigationStore navigationStore, NavigationService dashboardViewNavigationService, NavigationService userSettingsViewNavigationService, string title, string description, string progress)
        {
            _dashboardViewNavigationService = dashboardViewNavigationService;
            _userSettingsViewNavigationService = userSettingsViewNavigationService;

            Title = title;
            Description = description;
            Progress = progress;

            LessonNavigateCommand = new NavigateCommand(new NavigationService(navigationStore, CreateLessonViewModel));
        }

        private LessonViewModel CreateLessonViewModel()
        {
            return new LessonViewModel(_dashboardViewNavigationService, _userSettingsViewNavigationService,"username", Title, new List<string>(), new List<ViewModelBase>());
        }
    }
}
