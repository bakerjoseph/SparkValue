using SparkValueDesktopApplication.Commands;
using SparkValueDesktopApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SparkValueDesktopApplication.ViewModels
{
    public class SignInViewModel : ViewModelBase
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

        public ICommand CreateAccountCommand { get; }
        public ICommand BreadboardNavigateCommand { get; }
        public ICommand SignInCommand { get; }
        public ICommand ForgotPasswordNavigateCommand { get; }

        /// <summary>
        /// Used in conjunction with SignInView.xaml
        /// </summary>
        public SignInViewModel(NavigationService breadboardViewNavigationService,
                            NavigationService newAccountViewNavigationService,
                            NavigationService dashboardViewNavigationService,
                            NavigationService passwordChangeViewNavigationService)
        {
            _username = string.Empty;

            BreadboardNavigateCommand = new NavigateCommand(breadboardViewNavigationService);
            CreateAccountCommand = new NavigateCommand(newAccountViewNavigationService);
            SignInCommand = new SignInCommand(dashboardViewNavigationService);
            ForgotPasswordNavigateCommand = new NavigateCommand(passwordChangeViewNavigationService);
        }
    }
}
