using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Security;

namespace SparkValueBackend.Models
{
    public class UserAccountModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string EmailAddress { get; private set; }
        public string SaltValue { get; private set; }

        public List<ProgressModel> AccountOverallProgress { get; private set; }
        public List<ProgressModel> LessonProgress { get; private set; }

        public UserAccountModel() { }

        public UserAccountModel(string username, string password, string emailAddress, string saltValue, List<ProgressModel> accountOverallProgress, List<ProgressModel> lessonProgress)
        {
            Username = username;
            Password = password;
            EmailAddress = emailAddress;
            SaltValue = saltValue;

            AccountOverallProgress = accountOverallProgress;
            LessonProgress = lessonProgress;
        }

        /// <summary>
        /// Update a user's username
        /// </summary>
        /// <param name="newUsername">The new username of the user</param>
        public void UpdateUsername(string newUsername)
        {
            Username = newUsername;
        }

        /// <summary>
        /// Update a user's password
        /// </summary>
        /// <param name="newPassword">The new password of the user</param>
        public void UpdatePassword(string newPassword)
        {
            Password = newPassword;
        }

        /// <summary>
        /// Update a user's email address
        /// </summary>
        /// <param name="newEmailAddress">The new email address of the user</param>
        public void UpdateEmailAddress(string newEmailAddress)
        {
            EmailAddress = newEmailAddress;
        }

        /// <summary>
        /// Update account progress
        /// </summary>
        /// <param name="item">Unit name</param>
        /// <param name="value">Progress in that unit</param>
        public void UpdateAccountOverallProgress(string item, int value)
        {
            ProgressModel? modelFound = AccountOverallProgress.FirstOrDefault(pm => pm.ItemName.Equals(item));

            // Is the item a valid name in the progress?
            if (modelFound == null) return;

            modelFound.UpdateProgress(value);
        }

        /// <summary>
        /// Update lesson progress
        /// </summary>
        /// <param name="item">Lesson name</param>
        /// <param name="value">Progress in that lesson</param>
        public void UpdateLessonProgress(string item, int value)
        {
            ProgressModel? modelFound = LessonProgress.FirstOrDefault(pm => pm.ItemName.Equals(item));

            // Is the item a valid name in the progress?
            if (modelFound == null) return;

            modelFound.UpdateProgress(value);
        }

        /// <summary>
        /// Reset all unit and lesson progress back to zero.
        /// </summary>
        public void ResetAllProgress()
        {

            foreach (ProgressModel pmU in AccountOverallProgress)
            {
                pmU.ResetProgress();
            }

            foreach (ProgressModel pmL in LessonProgress)
            {
                pmL.ResetProgress();
            }
        }
    }
}
