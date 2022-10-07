using SparkValueDesktopApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueDesktopApplication.Models
{
    public class LessonModel
    {
        public string Title { get; }
        public string Description { get; }
        public List<string> Content { get; }
        public List<ViewModelBase> InteractiveElements { get; }

        public LessonModel() { }

        public LessonModel(string title, string description, IEnumerable<string> content, IEnumerable<ViewModelBase> interactiveElements)
        {
            Title = title;
            Description = description;
            Content = new List<string>(content);
            InteractiveElements = new List<ViewModelBase>(interactiveElements);
        }
    }
}
