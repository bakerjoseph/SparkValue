using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SparkValueBackend.Models
{
    public class UnitModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public List<LessonModel> Lessons { get; private set; }

        public UnitModel() { }

        public UnitModel(string title, string description, IEnumerable<LessonModel> lessons)
        {
            Title = title;
            Description = description;
            Lessons = new List<LessonModel>(lessons);
        }
    }
}
