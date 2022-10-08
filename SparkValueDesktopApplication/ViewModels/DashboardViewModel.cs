using SparkValueDesktopApplication.Commands;
using SparkValueDesktopApplication.Services;
using SparkValueDesktopApplication.Stores;
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
            NavigationStore navigationStore,
            NavigationService breadboardViewNavigationService, 
            NavigationService userSettingsViewNavigationService,
            NavigationService signInViewNavigationService,
            NavigationService dashboardViewNavigationService,
            string username)
        {
            Username = username;
            _units = new ObservableCollection<UnitViewModel>
            {
                new UnitViewModel(
                    "Unit 1 - Shocking Introduction", 
                    "The intro unit", 
                    new List<PartialLessonViewModel>()
                    {
                        new PartialLessonViewModel(
                            navigationStore,
                            dashboardViewNavigationService,
                            userSettingsViewNavigationService,
                            "Lesson 1 - Electricity Primer",
                            "An intro to electricity",
                            "0/0"),
                        new PartialLessonViewModel(
                            navigationStore,
                            dashboardViewNavigationService,
                            userSettingsViewNavigationService,
                            "Lesson 2 - Reading Circuit Diagrams",
                            "Learn how to read circuit diagrams to build your own circuits",
                            "0/0"),
                    }),
                new UnitViewModel(
                    "Unit 2 - Components Galore",
                    "Component Discussion",
                    new List<PartialLessonViewModel>()
                    {
                        new PartialLessonViewModel(
                            navigationStore,
                            dashboardViewNavigationService,
                            userSettingsViewNavigationService,
                            "Lesson 1 - Resistors",
                            "Let's talk about resistors",
                            "0/0"),
                    })
            };

            BreadboardNavigateCommand = new NavigateCommand(breadboardViewNavigationService);
            SettingsNavigateCommand = new NavigateCommand(userSettingsViewNavigationService);
            LogOutCommand = new LogOutCommand(signInViewNavigationService);
        }
    }
}
