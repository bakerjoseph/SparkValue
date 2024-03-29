﻿using SparkValueBackend.Commands;
using SparkValueBackend.Services;
using SparkValueBackend.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SparkValueBackend.ViewModels
{
    public class SettingsViewModel : ViewModelBase
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

        private ViewModelBase _currentSettingViewModel;
        public ViewModelBase CurrentSettingViewModel
        {
            get 
            { 
                return _currentSettingViewModel; 
            }
            set 
            {
                _currentSettingViewModel?.Dispose();
                _currentSettingViewModel = value;
                OnCurrentSettingViewModelChanged();
                OnPropertyChanged(nameof(CurrentSettingViewModel));
            }
        }

        public Action CurrentSettingViewModelChanged;

        private void OnCurrentSettingViewModelChanged()
        {
            CurrentSettingViewModelChanged?.Invoke();
        }

        public ICommand SwitchToGeneralCommand { get; }
        public ICommand SwitchToAccountCommand { get; }
        public ICommand MenuNavigateCommand { get; }

        /// <summary>
        /// Used in conjunction with UserSettingsView.xaml
        /// </summary>
        public SettingsViewModel(UserStore userStore, EmailStatusStore emailStatusStore, NavigationService dashboardViewNavigationService, List<NavigationService> generalNavigationServices, List<NavigationService> accountNavigationServices, string currentSettingViewModel)
        {
            _username = userStore.LoggedInUser.Username;
            _currentSettingViewModel = (currentSettingViewModel.Equals("General"))? new SettingsGeneralViewModel(generalNavigationServices) : 
                                       (currentSettingViewModel.Equals("Account")) ? new SettingsAccountViewModel(accountNavigationServices, userStore, emailStatusStore) : throw new Exception($"{currentSettingViewModel} is not a recognized settings view model");

            MenuNavigateCommand = new NavigateCommand(dashboardViewNavigationService);
            SwitchToGeneralCommand = new SwitchSettingsCommand("General", this, generalNavigationServices, userStore, emailStatusStore);
            SwitchToAccountCommand = new SwitchSettingsCommand("Account", this, accountNavigationServices, userStore, emailStatusStore);
        }
    }
}
