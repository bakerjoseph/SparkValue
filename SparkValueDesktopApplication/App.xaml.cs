using SparkValueBackend.Services;
using SparkValueBackend.Stores;
using SparkValueBackend.ViewModels;
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
        private const string ConnectionString = "mongodb://localhost:27017";
        private const string DatabaseName = "sparkvaluedb";
        private const string UserCollection = "users";
        private const string UnitCollection = "units";

        private readonly NavigationStore _navigationStore;
        private readonly UserStore _userStore;
        private readonly UnitStore _unitStore;
        private readonly LocalComponentStore _componentStore;

        private readonly SecurityService _securityService;

        private readonly IEnumerable<ComponentCategoryViewModel> _componentCategories;

        public App()
        {
            _securityService = new SecurityService();

            _navigationStore = new NavigationStore();
            _userStore = new UserStore(ConnectionString, DatabaseName, UserCollection);
            _unitStore = new UnitStore(ConnectionString, DatabaseName, UnitCollection);
            _componentStore = new LocalComponentStore();

            _userStore.LoadUsers().Wait();
            _unitStore.LoadUnits().Wait();

            _componentCategories = _componentStore.GetComponentCategories();
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
            return new BreadboardViewModel(new NavigationService(_navigationStore, CreateSignInViewModel), _componentCategories);
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
                _navigationStore,
                new NavigationService(_navigationStore, CreateBreadboardViewModel),
                new NavigationService(_navigationStore, CreateUserSettingsGeneralViewModel),
                new NavigationService(_navigationStore, CreateSignInViewModel),
                new NavigationService(_navigationStore, CreateDashboardViewModel),
                // Temp band-aid until you can actually log in with an account!!
                (_userStore.LoggedInUser != null)? _userStore.LoggedInUser.Username : "none found");
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
                // Temp band-aid until you can actually log in with an account!!
                (_userStore.LoggedInUser != null) ? _userStore.LoggedInUser.Username : "none found",
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
                // Temp band-aid until you can actually log in with an account!!
                (_userStore.LoggedInUser != null) ? _userStore.LoggedInUser.Username : "none found",
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
