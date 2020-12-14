using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebService.Repository.Context.DatabaseSQL;

namespace WebService.Repository.DAO.Fact_Measurement
{
    public class FactMeasurementDao : IFactMeasurementDao
    {
        private EnviormentContext _context;

        public FactMeasurementDao(EnviormentContext context)
        {
            _context = context;
        }
        
        public IQueryable<FMeasurement> GetOverview()
        {
            
        }
    }
}
