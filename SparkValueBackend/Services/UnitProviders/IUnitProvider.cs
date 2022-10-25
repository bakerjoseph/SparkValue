using SparkValueBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueBackend.Services.UnitProviders
{
    public interface IUnitProvider
    {
        Task<IEnumerable<UnitModel>> GetAllUnits();
        Task<UnitModel> GetUnitByTitle(string title);
    }
}
