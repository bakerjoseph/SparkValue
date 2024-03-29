﻿using SparkValueBackend.Services;
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
        private readonly EmailStatusStore _emailStatusStore;
        private readonly UserStore _userStore;
        private readonly UnitStore _unitStore;
        private readonly LocalComponentStore _componentStore;

        private readonly SecurityService _securityService;
        private readonly MongoInitService _mongoInitService;
        private readonly EmailService _emailService;

        private readonly IEnumerable<ComponentCategoryViewModel> _componentCategories;

        public App()
        {
            _securityService = new SecurityService();
            _mongoInitService = new MongoInitService(ConnectionString, DatabaseName, UnitCollection);
            
            _emailStatusStore = new EmailStatusStore();
            _emailService = new EmailService(_emailStatusStore);

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
            return new BreadboardViewModel(new NavigationService(_navigationStore, CreateSignInViewModel), new NavigationService(_navigationStore, CreateDashboardViewModel), _userStore, _componentCategories);
        }

        private NewAccountViewModel CreateNewAccountViewModel()
        {
            return new NewAccountViewModel(_userStore, _unitStore, _emailStatusStore, new NavigationService(_navigationStore, CreateSignInViewModel), _securityService, _emailService);
        }

        private SignInViewModel CreateSignInViewModel()
        {
            return new SignInViewModel(
                _userStore,
                _emailStatusStore,
                new NavigationService(_navigationStore, CreateBreadboardViewModel),
                new NavigationService(_navigationStore, CreateNewAccountViewModel),
                new NavigationService(_navigationStore, CreateDashboardViewModel),
                new NavigationService(_navigationStore, CreateUsernameRequestViewModel),
                _securityService);
        }

        private DashboardViewModel CreateDashboardViewModel()
        {
            return new DashboardViewModel(
                _navigationStore,
                _unitStore,
                _userStore,
                new NavigationService(_navigationStore, CreateBreadboardViewModel),
                new NavigationService(_navigationStore, CreateUserSettingsGeneralViewModel),
                new NavigationService(_navigationStore, CreateSignInViewModel),
                new NavigationService(_navigationStore, CreateDashboardViewModel));
        }

        private SettingsViewModel CreateUserSettingsGeneralViewModel()
        {
            return new SettingsViewModel(
                _userStore,
                _emailStatusStore,
                new NavigationService(_navigationStore, CreateDashboardViewModel),
                new List<NavigationService>() 
                { 

                },
                new List<NavigationService>()
                {
                    new NavigationService(_navigationStore, CreateResetUsernameViewModel),
                    new NavigationService(_navigationStore, CreateResetEmailAddressViewModel),
                    new NavigationService(_navigationStore, CreateResetPasswordViewModelToSettings),
                    new NavigationService(_navigationStore, CreateDashboardViewModel)
                },
                "General");
        }

        private SettingsViewModel CreateUserSettingsAccountViewModel()
        {
            return new SettingsViewModel(
                _userStore,
                _emailStatusStore,
                new NavigationService(_navigationStore, CreateDashboardViewModel),
                new List<NavigationService>()
                {

                },
                new List<NavigationService>()
                {
                    new NavigationService(_navigationStore, CreateResetUsernameViewModel),
                    new NavigationService(_navigationStore, CreateResetEmailAddressViewModel),
                    new NavigationService(_navigationStore, CreateResetPasswordViewModelToSettings),
                    new NavigationService(_navigationStore, CreateDashboardViewModel)
                },
                "Account");
        }

        private ResetUsernameViewModel CreateResetUsernameViewModel()
        {
            return new ResetUsernameViewModel(_userStore, new NavigationService(_navigationStore, CreateUserSettingsAccountViewModel));
        }

        private ResetEmailAddressViewModel CreateResetEmailAddressViewModel()
        {
            return new ResetEmailAddressViewModel(_userStore, new NavigationService(_navigationStore, CreateUserSettingsAccountViewModel));
        }

        private ResetPasswordViewModel CreateResetPasswordViewModelToSettings()
        {
            return new ResetPasswordViewModel(_userStore, _emailStatusStore, _emailService, new NavigationService(_navigationStore, CreateUserSettingsAccountViewModel), _securityService);
        }

        private UsernameEmailRequestViewModel CreateUsernameRequestViewModel()
        {
            return new UsernameEmailRequestViewModel(_navigationStore, _emailStatusStore, _userStore, _emailService, new NavigationService(_navigationStore, CreateSignInViewModel), _securityService);
        }
    }
}
