using SparkValueBackend.Models;
using SparkValueBackend.Services;
using SparkValueBackend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueBackend.Commands
{
    public class NavigateAwayFromLessonCommand : CommandBase
    {
        private readonly LessonViewModel _lessonViewModel;

        private readonly NavigationService? _targetNavigationService;

        private readonly UserAccountModel _userModel;

        public NavigateAwayFromLessonCommand(LessonViewModel lesson, NavigationService? target, UserAccountModel user)
        {
            _lessonViewModel = lesson;

            _targetNavigationService = target;

            _userModel = user;
        }


        public override void Execute(object? parameter)
        {
            // Update the users progress on that lesson
            _userModel.UpdateLessonProgress(_lessonViewModel.Title, _lessonViewModel.GetLessonProgress());

            // Continue to navigate away from the lesson to another view if we have a target view
            if (_targetNavigationService == null) return;
            _targetNavigationService.Navigate();
        }
    }
}
