using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SparkValueDesktopApplication.ViewModels
{
    public class SettingsAccountViewModel : ViewModelBase
    {
        public ICommand ResetUsernameCommand { get; }
        public ICommand ResetEmailAddressCommand { get; }
        public ICommand ResetPasswordCommand { get; }
        public ICommand ResetAccountCommand { get; }

        public SettingsAccountViewModel()
        {

        }
    }
}
