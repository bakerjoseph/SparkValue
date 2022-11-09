using SparkValueBackend.Models;
using SparkValueBackend.Services;
using SparkValueBackend.Stores;
using SparkValueBackend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueBackend.Commands
{
    public class NavigateAwayFromLessonCommand : AsyncCommandBase
    {
        private readonly LessonViewModel _lessonViewModel;

        private readonly NavigationService? _targetNavigationService;

        private readonly UserStore _userStore;

        private readonly UserAccountModel _userModel;

        public NavigateAwayFromLessonCommand(LessonViewModel lesson, NavigationService? target, UserStore userStore, UserAccountModel user)
        {
            _lessonViewModel = lesson;

            _targetNavigationService = target;

            _userStore = userStore;

            _userModel = user;
        }


        public override async Task ExecuteAsync(object? parameter)
        {
            // Update the users progress on that lesson
            await _userStore.UpdateUserLessonProgress(_userModel, _lessonViewModel.Title, _lessonViewModel.GetLessonProgress());

            // Continue to navigate away from the lesson to another view if we have a target view
            if (_targetNavigationService == null) return;
            _targetNavigationService.Navigate();
        }
    }
}
