using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SparkValueBackend.Models
{
    public class LessonModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public List<string> Content { get; private set; }
        public List<string> InteractiveElementTitles { get; private set; }

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
