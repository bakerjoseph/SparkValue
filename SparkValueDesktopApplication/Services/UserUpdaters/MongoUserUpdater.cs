using MongoDB.Driver;
using SparkValueDesktopApplication.Models;
using SparkValueDesktopApplication.Services.Utils;
using SparkValueDesktopApplication.Services.UserProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoUtils = SparkValueDesktopApplication.Services.Utils.MongoUtils;

namespace SparkValueDesktopApplication.Services.UserUpdaters
{
    public class MongoUserUpdater : MongoUtils, IUserUpdater
    {
        private readonly string _userCollection;
        private readonly IUserProvider _userProvider;

        public MongoUserUpdater(string connectionString, string databaseName, string userCollection) : base(connectionString, databaseName)
        {
            _userCollection = userCollection;
            _userProvider = new MongoUserProvider(connectionString, databaseName, userCollection);
        }

        public async Task UpdateUsersEmailAddress(string currentUsername, string newEmailAddress)
        {
            UserAccountModel targetUser = await _userProvider.GetUserByUsername(currentUsername);
            targetUser.UpdateEmailAddress(newEmailAddress);
            IMongoCollection<UserAccountModel> usersCollection = ConnectToMongo<UserAccountModel>(_userCollection);
            FilterDefinition<UserAccountModel> filter = Builders<UserAccountModel>.Filter.Eq("Id", targetUser.Id);
            await usersCollection.ReplaceOneAsync(filter, targetUser);
        }

        public async Task UpdateUsersPassword(string currentUsername, string newPassword)
        {
            UserAccountModel targetUser = await _userProvider.GetUserByUsername(currentUsername);
            targetUser.UpdatePassword(newPassword);
            IMongoCollection<UserAccountModel> usersCollection = ConnectToMongo<UserAccountModel>(_userCollection);
            FilterDefinition<UserAccountModel> filter = Builders<UserAccountModel>.Filter.Eq("Id", targetUser.Id);
            await usersCollection.ReplaceOneAsync(filter, targetUser);
        }

        public async Task UpdateUsersUsername(string currentUsername, string newUsername)
        {
            UserAccountModel targetUser = await _userProvider.GetUserByUsername(currentUsername);
            targetUser.UpdateUsername(newUsername);
            IMongoCollection<UserAccountModel> usersCollection = ConnectToMongo<UserAccountModel>(_userCollection);
            FilterDefinition<UserAccountModel> filter = Builders<UserAccountModel>.Filter.Eq("Id", targetUser.Id);
            await usersCollection.ReplaceOneAsync(filter, targetUser);
        }
    }
}
