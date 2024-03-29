﻿using SparkValueBackend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueBackend.Commands
{
    public class LessonIterateForwardCommand : CommandBase
    {
        private readonly LessonViewModel _lesson;

        public LessonIterateForwardCommand(LessonViewModel lesson)
        {
            _lesson = lesson;
            lesson.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object? parameter)
        {
            return _lesson.CanGoForward() && base.CanExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _lesson.IncrementLessonProgress();
        }

        private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(LessonViewModel.Progress))
            {
                OnCanExecutedChanged();
            }
        }
    }
}
