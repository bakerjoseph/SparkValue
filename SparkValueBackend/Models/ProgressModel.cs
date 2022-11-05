using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueBackend.Models
{
    public class ProgressModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        public string ItemName { get; private set; }
        public int Progress { get; private set; }

        public ProgressModel(string itemName, int progress)
        {
            ItemName = itemName;
            Progress = progress;
        }

        /// <summary>
        /// Update the progress of an item.
        /// </summary>
        /// <param name="value">This represents the new progress ammount</param>
        public void UpdateProgress(int value)
        {
            // Can not update if the new value is less than or equal to the current progress
            if (value <= Progress) return;

            Progress = value;
        }

        /// <summary>
        /// Reset the progress back to zero.
        /// </summary>
        public void ResetProgress()
        {
            Progress = 0;
        }
    }
}
