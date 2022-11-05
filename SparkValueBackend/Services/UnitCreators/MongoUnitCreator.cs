using MongoDB.Driver;
using SparkValueBackend.Models;
using SparkValueBackend.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoUtils = SparkValueBackend.Services.Utils.MongoUtils;

namespace SparkValueBackend.Services.UnitCreators
{
    public class MongoUnitCreator : MongoUtils, IUnitCreator
    {
        private readonly string _unitCollection;

        public MongoUnitCreator(string connectionString, string databaseName, string unitCollection) : base(connectionString, databaseName)
        {
            _unitCollection = unitCollection;
        }

        public Task CreateUnit(UnitModel unit)
        {
            IMongoCollection<UnitModel> unitCollection = ConnectToMongo<UnitModel>(_unitCollection);
            return unitCollection.InsertOneAsync(unit);
        }
    }
}
