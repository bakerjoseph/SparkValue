using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueDesktopApplication.Models
{
    public class UnitModel
    {
        public string Title { get; }
        public string Description { get; }
        public List<LessonModel> Lessons { get; }

        public UnitModel() { }

        public UnitModel(string title, string description, IEnumerable<LessonModel> lessons)
        {
            Title = title;
            Description = description;
            Lessons = new List<LessonModel>(lessons);
        }
    }
}
