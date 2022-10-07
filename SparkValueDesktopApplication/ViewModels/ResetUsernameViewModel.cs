using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SparkValueDesktopApplication.ViewModels
{
    public class ResetUsernameViewModel : ViewModelBase
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

        public ICommand CancelCommand { get; }
        public ICommand ResetUsernameCommand { get; }

        /// <summary>
        /// Used in conjunction with UsernameChangeView.xaml
        /// </summary>
        public ResetUsernameViewModel()
        {
            _username = string.Empty;


        }
    }
}
