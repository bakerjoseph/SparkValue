using SparkValueDesktopApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueDesktopApplication.Services.LessonProviders
{
    public interface ILessonProvider
    {
        Task<IEnumerable<LessonModel>> GetLessonsInAUnit(string unitTag);
    }
}
