using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SparkValueBackend.Models
{
    public class UnitModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; }
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
