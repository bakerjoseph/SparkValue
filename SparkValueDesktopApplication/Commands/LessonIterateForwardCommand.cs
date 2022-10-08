using SparkValueDesktopApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueDesktopApplication.Commands
{
    public class LessonIterateForwardCommand : CommandBase
    {
        private readonly LessonViewModel _lesson;

        public LessonIterateForwardCommand(LessonViewModel lesson)
        {
            _lesson = lesson;
        }

        public override void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }
    }
}
