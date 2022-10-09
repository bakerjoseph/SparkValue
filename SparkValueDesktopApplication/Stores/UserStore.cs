using SparkValueDesktopApplication.Models;
using SparkValueDesktopApplication.Services.UserCreators;
using SparkValueDesktopApplication.Services.UserProviders;
using SparkValueDesktopApplication.Services.UserUpdaters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueDesktopApplication.Stores
{
    public class UserStore
    {
        #region Logged In User
        private UserAccountModel? _loggedInUser;
        public UserAccountModel? LoggedInUser
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
        #endregion

        private readonly List<UserAccountModel> _users;
        public IEnumerable<UserAccountModel> Users => _users;
        private readonly Lazy<Task> _initializeLazy;

        private readonly IUserCreator _userCreator;
        private readonly IUserProvider _userProvider;
        private readonly IUserUpdater _userUpdater;



        public UserStore(string connectionString, string databaseName, string userCollection)
        {
            _users = new List<UserAccountModel>();
            _userCreator = new MongoUserCreator(connectionString, databaseName, userCollection);
            _userProvider = new MongoUserProvider(connectionString, databaseName, userCollection);
            _userUpdater = new MongoUserUpdater(connectionString, databaseName, userCollection);

            _initializeLazy = new Lazy<Task>(InitializeUsers);
            _initializeLazy.Value.Wait();
        }

        private async Task InitializeUsers()
        {
            IEnumerable<UserAccountModel> users = await _userProvider.GetAllUsers();

            _users.Clear();
            _users.AddRange(users);
        }

        public async Task CreateUser(UserAccountModel user)
        {
            await _userCreator.CreateUser(user);

            _users.Add(user);
        }

        public async Task UpdateUsersUsername(UserAccountModel user, string newUsername)
        {
            // Under normal conditions this should never return
            // You would always have at least one user, since it is required to have an account to log in
            if (!_users.Any()) return;

            user.UpdateUsername(newUsername);

            await _userUpdater.UpdateUsersUsername(user);
        }

        public async Task UpdateUsersEmailAddress(UserAccountModel user, string newEmailAddress)
        {
            // Under normal conditions this should never return
            // You would always have at least one user, since it is required to have an account to log in
            if (!_users.Any()) return;

            user.UpdateEmailAddress(newEmailAddress);

            await _userUpdater.UpdateUsersEmailAddress(user);
        }

        public async Task UpdateUsersPassword(UserAccountModel user, string newPassword)
        {
            // Under normal conditions this should never return
            // You would always have at least one user, since it is required to have an account to log in
            if (!_users.Any()) return;

            user.UpdatePassword(newPassword);

            await _userUpdater.UpdateUsersPassword(user);
        }
    }
}
