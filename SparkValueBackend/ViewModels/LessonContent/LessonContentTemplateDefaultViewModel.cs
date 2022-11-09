using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueBackend.ViewModels.LessonContent
{
    public class LessonContentTemplateDefaultViewModel : LessonContentViewModelBase
    {
        public LessonContentTemplateDefaultViewModel(string plainText, string? interactiveElement) : base(plainText, interactiveElement)
        {
        }
    }
}
