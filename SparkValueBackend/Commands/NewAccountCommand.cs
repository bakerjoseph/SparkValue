using SparkValueBackend.Models;
using SparkValueBackend.Services;
using SparkValueBackend.Stores;
using SparkValueBackend.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SparkValueBackend.Commands
{
    public class NewAccountCommand : AsyncCommandBase
    {
        private readonly NavigationService _signInViewNavigationService;
        private readonly SecurityService _securityService;

        private readonly NewAccountViewModel _newAccountViewModel;

        private readonly UserStore _userStore;
        private readonly UnitStore _unitStore;

        public NewAccountCommand(NewAccountViewModel accountVM, UserStore userStore, UnitStore unitStore, NavigationService signInViewNavigationService, SecurityService security)
        {
            _signInViewNavigationService = signInViewNavigationService;
            _securityService = security;

            _newAccountViewModel = accountVM;
            accountVM.PropertyChanged += OnViewModelPropertyChanged;

            _userStore = userStore;
            _unitStore = unitStore; 
        }

        public override bool CanExecute(object? parameter)
        {
            return !string.IsNullOrEmpty(_newAccountViewModel.Username) && 
                !string.IsNullOrEmpty(_newAccountViewModel.EmailAddress) && 
                _newAccountViewModel.SecurePassword != null && 
                _newAccountViewModel.SecurePassword.Length > 0 && 
                base.CanExecute(parameter);
        }

        public override async Task ExecuteAsync(object? parameter)
        {
            // Does an other account have the same username?
            List<UserAccountModel> users = new List<UserAccountModel>(_userStore.Users);
            if (users.Where(x => x.Username == _newAccountViewModel.Username).Any())
            {
                _newAccountViewModel.ErrorText = "Another account already exists with this username, please enter a different username to continue.";
                return;
            }

            try
            {
                // Create account with the passed in view model values
                (string salt, string hashed) outcome = _securityService.ProtectPassword(_newAccountViewModel.SecurePassword);
                UserAccountModel newUser = new UserAccountModel(_newAccountViewModel.Username, outcome.hashed, _newAccountViewModel.EmailAddress, outcome.salt, GetDefaultUnitProgress(), GetDefaultLessonProgress());
                await _userStore.CreateUser(newUser);

                _signInViewNavigationService.Navigate();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex, "Create New User");
                _newAccountViewModel.ErrorText = "Unable to create account, please try again later.";
            }
        }

        private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(NewAccountViewModel.Username) || e.PropertyName == nameof(NewAccountViewModel.EmailAddress) || e.PropertyName == nameof(NewAccountViewModel.SecurePassword))
            {
                OnCanExecutedChanged();
            }
        }

        private List<ProgressModel> GetDefaultUnitProgress()
        {
            List<ProgressModel> unitProgressModels = new List<ProgressModel>();

            foreach (UnitModel unit in _unitStore.Units)
            {
                unitProgressModels.Add(new ProgressModel(unit.Title, 0));
            }

            return unitProgressModels;
        }

        private List<ProgressModel> GetDefaultLessonProgress()
        {
            List<ProgressModel> lessonProgresModels = new List<ProgressModel>();

            foreach (UnitModel unit in _unitStore.Units)
            {
                foreach (LessonModel lesson in unit.Lessons)
                {
                    lessonProgresModels.Add(new ProgressModel(lesson.Title, 0));
                }
            }

            return lessonProgresModels;
        }
    }
}
