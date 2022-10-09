using MongoDB.Driver;
using SparkValueDesktopApplication.Models;
using SparkValueDesktopApplication.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoUtils = SparkValueDesktopApplication.Services.Utils.MongoUtils;

namespace SparkValueDesktopApplication.Services.UserCreators
{
    public class MongoUserCreator : MongoUtils, IUserCreator
    {
        private readonly string _userCollection;

        public MongoUserCreator(string connectionString, string databaseName, string userCollection) : base(connectionString, databaseName)
        {
            _userCollection = userCollection;
        }

        public Task CreateUser(UserAccountModel user)
        {
            IMongoCollection<UserAccountModel> usersCollection = ConnectToMongo<UserAccountModel>(_userCollection);
            return usersCollection.InsertOneAsync(user);
        }
    }
}
