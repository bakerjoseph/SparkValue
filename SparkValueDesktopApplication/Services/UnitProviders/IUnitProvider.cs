using SparkValueDesktopApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkValueDesktopApplication.Services.UnitProviders
{
    public interface IUnitProvider
    {
        Task<IEnumerable<UnitModel>> GetAllUnits();
    }
}
