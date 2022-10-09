using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueDesktopApplication.Services.Utils
{
    public class MongoUtils
    {
        private readonly string _connectionString;
        private readonly string _databaseName;

        public MongoUtils(string connectionString, string databaseName)
        {
            _connectionString = connectionString;
            _databaseName = databaseName;
        }

        public IMongoCollection<T> ConnectToMongo<T>(in string collection)
        {
            var client = new MongoClient(_connectionString);
            var db = client.GetDatabase(_databaseName);
            return db.GetCollection<T>(collection);
        }
    }
}
