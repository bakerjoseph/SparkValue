using SparkValueDesktopApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueDesktopApplication.Stores
{
    public class UserStore
    {
        private UserAccountModel _loggedInUser;
        public UserAccountModel LoggedInUser
        {
            get { return _loggedInUser; }
            set
            {
                _loggedInUser = value;
                OnCurrentLoggedInUserChanged();
            }
        }

        public event Action CurrentLoggedInUserChanged;

        private void OnCurrentLoggedInUserChanged()
        {
            CurrentLoggedInUserChanged?.Invoke();
        }
    }
}
