using SparkValueBackend.Models;
using SparkValueBackend.Services.UnitProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueBackend.Stores
{
    public class UnitStore
    {
        private readonly List<UnitModel> _units;
        public IEnumerable<UnitModel> Units => _units;
        private readonly Lazy<Task> _initializeLazy;

        private readonly IUnitProvider _unitProvider;

        public UnitStore(string connectionString, string databaseName, string unitCollection)
        {
            _units = new List<UnitModel>();

            _unitProvider = new MongoUnitProvider(connectionString, databaseName, unitCollection);
            _initializeLazy = new Lazy<Task>(InitializeUnits);
        }

        public async Task LoadUnits()
        {
            await _initializeLazy.Value;
        }

        private async Task InitializeUnits()
        {
            IEnumerable<UnitModel> units = await _unitProvider.GetAllUnits();
            // Sort the units
            units = units.OrderBy(u => u.OrderNumber);

            // Sort the lessons
            foreach (UnitModel unit in units)
            {
                unit.Lessons.Sort((x, y) => x.OrderNumber.CompareTo(y.OrderNumber));
            }

            _units.Clear();
            _units.AddRange(units);
        }


    }
}
