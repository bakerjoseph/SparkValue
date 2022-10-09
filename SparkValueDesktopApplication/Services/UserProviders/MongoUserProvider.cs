using MongoDB.Driver;
using SparkValueDesktopApplication.Models;
using SparkValueDesktopApplication.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoUtils = SparkValueDesktopApplication.Services.Utils.MongoUtils;

namespace SparkValueDesktopApplication.Services.UserProviders
{
    public class MongoUserProvider : MongoUtils, IUserProvider
    {
        private readonly string _userCollection;

        public MongoUserProvider(string connectionString, string databaseName, string userCollection) : base(connectionString, databaseName)
        {
            _userCollection = userCollection;
        }

        public async Task<IEnumerable<UserAccountModel>> GetAllUsers()
        {
            IMongoCollection<UserAccountModel> usersCollection = ConnectToMongo<UserAccountModel>(_userCollection);
            IAsyncCursor<UserAccountModel> results = await usersCollection.FindAsync(_ => true);
            return results.ToList();
        }

        public async Task<UserAccountModel> GetUserByUsername(string username)
        {
            IMongoCollection<UserAccountModel> usersCollection = ConnectToMongo<UserAccountModel>(_userCollection);
            IAsyncCursor<UserAccountModel> result = await usersCollection.FindAsync(user => user.Username.Equals(username));
            return result.FirstOrDefault();
        }
    }
}
