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
    public class ResetPasswordViewModel : ViewModelBase
    {
        public ICommand CancelCommand { get; }
        public ICommand ResetPasswordCommand { get; }

        /// <summary>
        /// Used in conjunction with PasswordChangeView.xaml
        /// </summary>
        public ResetPasswordViewModel(NavigationService userSettingsViewNavigationService)
        {
            CancelCommand = new NavigateCommand(userSettingsViewNavigationService);
            ResetPasswordCommand = new ChangePasswordCommand(userSettingsViewNavigationService);
        }
    }
}
