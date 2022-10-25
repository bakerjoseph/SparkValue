using SparkValueBackend.Commands;
using SparkValueBackend.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SparkValueBackend.ViewModels
{
    public class SettingsAccountViewModel : ViewModelBase
    {
        public ICommand ResetUsernameCommand { get; }
        public ICommand ResetEmailAddressCommand { get; }
        public ICommand ResetPasswordCommand { get; }
        public ICommand ResetAccountCommand { get; }

        public SettingsAccountViewModel(List<NavigationService>? navigationServices)
        {
            if (navigationServices != null && navigationServices.Count == 4)
            {
                ResetUsernameCommand = new NavigateCommand(navigationServices[0]);
                ResetEmailAddressCommand = new NavigateCommand(navigationServices[1]);
                ResetPasswordCommand = new NavigateCommand(navigationServices[2]);
                ResetAccountCommand = new WipeAccountCommand(navigationServices[3]);
            }
            else
            {
                throw new ArgumentException($"{navigationServices?.Count} navigation services were passed, expected 4");
            }
        }
    }
}
