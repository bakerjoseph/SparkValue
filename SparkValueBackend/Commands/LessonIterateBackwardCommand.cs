using SparkValueBackend.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueBackend.Commands
{
    public class LessonIterateBackwardCommand : CommandBase
    {
        private readonly LessonViewModel _lesson;

        public LessonIterateBackwardCommand(LessonViewModel lesson)
        {
            _lesson = lesson;
        }

        public override void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }
    }
}
