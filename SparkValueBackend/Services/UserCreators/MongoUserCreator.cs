using MongoDB.Driver;
using SparkValueBackend.Models;
using SparkValueBackend.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoUtils = SparkValueBackend.Services.Utils.MongoUtils;

namespace SparkValueBackend.Services.UserCreators
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
