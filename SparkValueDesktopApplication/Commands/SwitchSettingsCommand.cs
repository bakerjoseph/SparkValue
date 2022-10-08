using SparkValueDesktopApplication.Services;
using SparkValueDesktopApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueDesktopApplication.Commands
{
    public class SwitchSettingsCommand : CommandBase
    {
        private readonly string _desiredViewModel;
        private readonly SettingsViewModel _settingsViewModel;
        private readonly List<NavigationService> _navigationServices;

        public SwitchSettingsCommand(string desiredViewModel, SettingsViewModel settingsViewModel, List<NavigationService> navigationServices)
        {
            _desiredViewModel = desiredViewModel;
            _settingsViewModel = settingsViewModel;
            _navigationServices = navigationServices;
        }

        public override void Execute(object? parameter)
        {
            if (_settingsViewModel.CurrentSettingViewModel.GetType() == typeof(SettingsGeneralViewModel) && _desiredViewModel.Equals("General")) return;
            else if (_settingsViewModel.CurrentSettingViewModel.GetType() == typeof(SettingsAccountViewModel) && _desiredViewModel.Equals("Account")) return;
            else _settingsViewModel.CurrentSettingViewModel = (_desiredViewModel.Equals("General"))? new SettingsGeneralViewModel(_navigationServices) : new SettingsAccountViewModel(_navigationServices);
        }
    }
}
