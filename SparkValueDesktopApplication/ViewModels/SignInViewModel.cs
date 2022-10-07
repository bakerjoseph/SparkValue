using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SparkValueDesktopApplication.ViewModels
{
    public class SignInViewModel : ViewModelBase
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

        public ICommand CreateAccountCommand { get; }
        public ICommand BreadboardNavigateCommand { get; }
        public ICommand SignInCommand { get; }
        public ICommand ForgotPasswordNavigateCommand { get; }

        /// <summary>
        /// Used in conjunction with SignInView.xaml
        /// </summary>
        public SignInViewModel()
        {
            _username = string.Empty;
            _password = string.Empty;


        }
    }
}
