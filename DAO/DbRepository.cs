using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebService.Context;
using WebService.Models.Shared;

namespace WebService.DAO
{
    public class DbRepository
    {
        private readonly EnviormentContext _context;

        public DbRepository(EnviormentContext context)
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