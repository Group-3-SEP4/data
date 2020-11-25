using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Models;

namespace WebService.DAO.Repository
{
    public interface IDbRepository
    {
        public int GetCo2Reading();
        public Settings GetSettings();
        Settings PostSettings(Settings settings);
    }
}
