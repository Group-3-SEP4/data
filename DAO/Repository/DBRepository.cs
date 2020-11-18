using System.Collections.Generic;
using System.Linq;
using WebService.DAO.Context;
using WebService.Models;

namespace WebService.DAO.Repository
{
    public class DBRepository : IDBRepository
    {
        private EnvironmentContext _context;

        public DBRepository(EnvironmentContext context)
        {
            _context = context;
        }

        public CarbonDioxideReading GetCO2Reading()
        {
            return _context.CarbonDioxideReading.AsEnumerable().Last();
        }

        public IEnumerable<Settings> GetSettings()
        {
            return _context.Settings.AsEnumerable().ToList();
        }
    }
}