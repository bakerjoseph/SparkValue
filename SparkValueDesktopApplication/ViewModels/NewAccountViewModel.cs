using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SparkValueDesktopApplication.ViewModels
{
    public class NewAccountViewModel : ViewModelBase
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

        private string _emailAddress;
        public string EmailAddress
        {
            get 
            { 
                return _emailAddress; 
            }
            set
            {
                _emailAddress = value;
                OnPropertyChanged(nameof(EmailAddress));
            }
        }

        public ICommand CancelCommand { get; }
        public ICommand CreateAccountCommand { get; }

        /// <summary>
        /// Used in conjunction with NewAccountView.xaml
        /// </summary>
        public NewAccountViewModel()
        {
            _username = string.Empty;
            _emailAddress = string.Empty;


        }
    }
}
