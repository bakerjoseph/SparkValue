using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace SparkValueBackend.Models
{
    public class UnitModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        public int OrderNumber { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public List<LessonModel> Lessons { get; private set; }

        public UnitModel() { }

        public UnitModel(string title, int orderNumber, string description, IEnumerable<LessonModel> lessons)
        {
            Title = title;
            OrderNumber = orderNumber;
            Description = description;
            Lessons = new List<LessonModel>(lessons);
        }
    }
}
