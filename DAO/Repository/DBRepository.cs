using System;
using System.Collections.Generic;
using System.Linq;
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

        public int GetCO2Reading()
        {
            return _context.Measurement.AsEnumerable().Last().CarbonDioxide;
        }

        public IEnumerable<Settings> GetSettings()
        {
            return _context.Settings.AsEnumerable().ToList();
        }
    }
}