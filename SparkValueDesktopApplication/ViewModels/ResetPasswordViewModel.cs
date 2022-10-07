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
        private string _password;
        public string Password 
        { 
            get 
            { 
                return _password; 
            } 
            set 
            { 
                _password = value; 
                OnPropertyChanged(nameof(Password));
            } 
        }

        public ICommand CancelCommand { get; }
        public ICommand ResetPasswordCommand { get; }

        /// <summary>
        /// Used in conjunction with PasswordChangeView.xaml
        /// </summary>
        public ResetPasswordViewModel()
        {
            _password = string.Empty;


        }
    }
}
