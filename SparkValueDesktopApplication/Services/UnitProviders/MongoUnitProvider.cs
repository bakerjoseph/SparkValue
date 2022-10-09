using MongoDB.Driver;
using SparkValueDesktopApplication.Models;
using SparkValueDesktopApplication.Services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoUtils = SparkValueDesktopApplication.Services.Utils.MongoUtils;

namespace SparkValueDesktopApplication.Services.UnitProviders
{
    public class MongoUnitProvider : MongoUtils, IUnitProvider
    {
        private readonly string _unitCollection;

        public MongoUnitProvider(string connectionString, string databaseName, string unitCollection) : base(connectionString, databaseName)
        {
            _unitCollection = unitCollection;
        }

        public async Task<IEnumerable<UnitModel>> GetAllUnits()
        {
            IMongoCollection<UnitModel> unitCollection = ConnectToMongo<UnitModel>(_unitCollection);
            IAsyncCursor<UnitModel> results = await unitCollection.FindAsync(_ => true);
            return results.ToList();
        }

        public async Task<UnitModel> GetUnitByTitle(string title)
        {
            IMongoCollection<UnitModel> unitCollection = ConnectToMongo<UnitModel>(_unitCollection);
            IAsyncCursor<UnitModel> results = await unitCollection.FindAsync(unit => unit.Title.Equals(title));
            return results.FirstOrDefault();
        }
    }
}
