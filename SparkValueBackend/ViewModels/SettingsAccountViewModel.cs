using SparkValueBackend.Commands;
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
    public class SettingsAccountViewModel : ViewModelBase
    {
        private bool _emailingStatus;
        public bool EmailingStatus
        {
            get { return _emailingStatus; }
            set 
            { 
                _emailingStatus = value; 
                OnPropertyChanged(nameof(EmailingStatus));
            }
        }

        public ICommand ResetUsernameCommand { get; }
        public ICommand ResetEmailAddressCommand { get; }
        public ICommand ResetPasswordCommand { get; }
        public ICommand ResetAccountCommand { get; }

        public SettingsAccountViewModel(List<NavigationService>? navigationServices, UserStore userStore, EmailStatusStore emailStatusStore)
        {
            if (navigationServices != null && navigationServices.Count == 4)
            {
                ResetUsernameCommand = new NavigateCommand(navigationServices[0]);
                ResetEmailAddressCommand = new NavigateCommand(navigationServices[1]);
                ResetPasswordCommand = new NavigateCommand(navigationServices[2]);
                ResetAccountCommand = new WipeAccountCommand(navigationServices[3], userStore);

                EmailingStatus = emailStatusStore.Status;
            }
            else
            {
                EmailingStatus = false;
                throw new ArgumentException($"{navigationServices?.Count} navigation services were passed, expected 4");
            }
        }
    }
}
