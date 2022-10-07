using SparkValueDesktopApplication.Commands;
using SparkValueDesktopApplication.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SparkValueDesktopApplication.ViewModels
{
    public class DashboardViewModel : ViewModelBase
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
        private readonly ObservableCollection<UnitViewModel> _units;

        public ICommand BreadboardNavigateCommand { get; }
        public ICommand SettingsNavigateCommand { get; }
        public ICommand LogOutCommand { get; }
        public IEnumerable<UnitViewModel> Units => _units;

        /// <summary>
        /// Used in conjunction with DashboardView.xaml
        /// </summary>
        public DashboardViewModel(
            NavigationService breadboardViewNavigationService, 
            NavigationService userSettingsViewNavigationService,
            NavigationService signInViewNavigationService,
            string username)
        {
            Username = username;
            _units = new ObservableCollection<UnitViewModel>
            {
                new UnitViewModel(
                    "Unit 1 - Shocking Introduction", 
                    "The intro unit", 
                    new List<LessonViewModel>()
                    {
                        new LessonViewModel(
                            username,
                            "[Unit 1 - Shocking Introduction] Lesson 1 - Electricity Primer",
                            "An intro to electricity",
                            new List<string>(),
                            new List<ViewModelBase>()),
                        new LessonViewModel(
                            username,
                            "[Unit 1 - Shocking Introduction] Lesson 2 - Reading Circuit Diagrams",
                            "Learn how to read circuit diagrams to build your own circuits",
                            new List<string>(),
                            new List<ViewModelBase>()),
                    }),
                new UnitViewModel(
                    "Unit 2 - Components Galore",
                    "Component Discussion",
                    new List<LessonViewModel>()
                    {
                        new LessonViewModel(
                            username,
                            "[Unit 2 - Components Galore] Lesson 1 - Resistors",
                            "Let's talk about resistors",
                            new List<string>(),
                            new List<ViewModelBase>()),
                    })
            };

            BreadboardNavigateCommand = new NavigateCommand(breadboardViewNavigationService);
            SettingsNavigateCommand = new NavigateCommand(userSettingsViewNavigationService);
            LogOutCommand = new LogOutCommand(signInViewNavigationService);
        }
    }
}
