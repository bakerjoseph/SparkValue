using SparkValueBackend.Models;
using SparkValueBackend.Services.UserCreators;
using SparkValueBackend.Services.UserProviders;
using SparkValueBackend.Services.UserUpdaters;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueBackend.Stores
{
    public class UserStore
    {
        #region Logged In User
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
        }

        public async Task LoadUsers()
        {
            await _initializeLazy.Value;
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

        public async Task UpdateUsersPassword(UserAccountModel user, (string salt, string hashedPassword) password)
        {
            // Under normal conditions this should never return
            // You would always have at least one user, since it is required to have an account to log in
            if (!_users.Any()) return;

            user.UpdateSalt(password.salt);
            user.UpdatePassword(password.hashedPassword);

            await _userUpdater.UpdateUsersPassword(user);
        }

        public async Task UpdateUserUnitProgress(UserAccountModel user, string unitTitle, int value)
        {
            // Under normal conditions this should never return
            // You would always have at least one user, since it is required to have an account to log in
            if (!_users.Any()) return;

            user.UpdateAccountOverallProgress(unitTitle, value);

            await _userUpdater.UpdateUnitProgress(user);
        }

        public async Task UpdateUserLessonProgress(UserAccountModel user, string lessonTitle, int value)
        {
            // Under normal conditions this should never return
            // You would always have at least one user, since it is required to have an account to log in
            if (!_users.Any()) return;

            user.UpdateLessonProgress(lessonTitle, value);

            await _userUpdater.UpdateLessonProgres(user);
        }

        public async Task ResetAllUserProgress(UserAccountModel user)
        {
            // Under normal conditions this should never return
            // You would always have at least one user, since it is required to have an account to log in
            if (!_users.Any()) return;

            user.ResetAllProgress();

            await _userUpdater.ResetProgress(user);
        }
    }
}
