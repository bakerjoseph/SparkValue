using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SparkValueDesktopApplication.ViewModels
{
    public class ResetEmailAddressViewModel : ViewModelBase
    {
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
        public ICommand ResetEmailAddressCommand { get; }

        /// <summary>
        /// Used in conjunction with EmailChangeView.xaml
        /// </summary>
        public ResetEmailAddressViewModel()
        {
            _emailAddress = string.Empty;


        }
    }
}
