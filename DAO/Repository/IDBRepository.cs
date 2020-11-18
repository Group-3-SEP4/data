using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Models;

namespace WebService.DAO.Repository
{
    public interface IDBRepository
    {
        public CarbonDioxideReading GetCO2Reading();
        public IEnumerable<Settings> GetSettings();
    }
}
