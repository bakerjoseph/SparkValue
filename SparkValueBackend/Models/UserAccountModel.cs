using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

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
        public string HashValue { get; private set; }

        public UserAccountModel() { }

        public UserAccountModel(string username, string password, string emailAddress, string hashValue)
        {
            Username = username;
            Password = password;
            EmailAddress = emailAddress;
            HashValue = hashValue;
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
    }
}
