using SparkValueDesktopApplication.Services;
using SparkValueDesktopApplication.Stores;
using SparkValueDesktopApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SparkValueDesktopApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;

        public App()
        {
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = CreateSignInViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };
            MainWindow.Show();

            base.OnStartup(e);
        }

        private BreadboardViewModel CreateBreadboardViewModel()
        {
            return new BreadboardViewModel(new NavigationService(_navigationStore, CreateSignInViewModel));
        }

        private NewAccountViewModel CreateNewAccountViewModel()
        {
            return new NewAccountViewModel(new NavigationService(_navigationStore, CreateSignInViewModel));
        }

        private SignInViewModel CreateSignInViewModel()
        {
            return new SignInViewModel(
                new NavigationService(_navigationStore, CreateBreadboardViewModel),
                new NavigationService(_navigationStore, CreateNewAccountViewModel),
                new NavigationService(_navigationStore, CreateDashboardViewModel),
                new NavigationService(_navigationStore, CreateResetPasswordViewModel));
        }

        private DashboardViewModel CreateDashboardViewModel()
        {
            return new DashboardViewModel(
                new NavigationService(_navigationStore, CreateBreadboardViewModel),
                new NavigationService(_navigationStore, CreateUserSettingsGeneralViewModel),
                new NavigationService(_navigationStore, CreateSignInViewModel),
                "username");
        }

        private SettingsViewModel CreateUserSettingsGeneralViewModel()
        {
            return new SettingsViewModel(
                new NavigationService(_navigationStore, CreateDashboardViewModel),
                new List<NavigationService>() 
                { 

                },
                new List<NavigationService>()
                {
                    new NavigationService(_navigationStore, CreateResetUsernameViewModel),
                    new NavigationService(_navigationStore, CreateResetEmailAddressViewModel),
                    new NavigationService(_navigationStore, CreateResetPasswordViewModel),
                    new NavigationService(_navigationStore, CreateDashboardViewModel)
                },
                "General");
        }

        private SettingsViewModel CreateUserSettingsAccountViewModel()
        {
            return new SettingsViewModel(
                new NavigationService(_navigationStore, CreateDashboardViewModel),
                new List<NavigationService>()
                {

                },
                new List<NavigationService>()
                {
                    new NavigationService(_navigationStore, CreateResetUsernameViewModel),
                    new NavigationService(_navigationStore, CreateResetEmailAddressViewModel),
                    new NavigationService(_navigationStore, CreateResetPasswordViewModel),
                    new NavigationService(_navigationStore, CreateDashboardViewModel)
                },
                "Account");
        }

        private ResetUsernameViewModel CreateResetUsernameViewModel()
        {
            return new ResetUsernameViewModel(new NavigationService(_navigationStore, CreateUserSettingsAccountViewModel));
        }

        private ResetEmailAddressViewModel CreateResetEmailAddressViewModel()
        {
            return new ResetEmailAddressViewModel(new NavigationService(_navigationStore, CreateUserSettingsAccountViewModel));
        }

        private ResetPasswordViewModel CreateResetPasswordViewModel()
        {
            return new ResetPasswordViewModel(new NavigationService(_navigationStore, CreateUserSettingsAccountViewModel));
        }
    }
}
