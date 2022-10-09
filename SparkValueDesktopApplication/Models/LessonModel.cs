using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
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
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; }
        public string Title { get; }
        public string Description { get; }
        public List<string> Content { get; }
        public List<string> InteractiveElementTitles { get; }

        public LessonModel() { }

        public LessonModel(string title, string description, IEnumerable<string> content, IEnumerable<string> interactiveElementTitles)
        {
            Title = title;
            Description = description;
            Content = new List<string>(content);
            InteractiveElementTitles = new List<string>(interactiveElementTitles);
        }
    }
}
