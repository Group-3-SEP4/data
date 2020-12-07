using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Models;

namespace WebService.DAO.Repository
{
    public interface IDbRepository
    {
        public Measurement GetMeasurement(string deviceId);
        public Settings GetSettings(string deviceId);
        Settings PostSettings(Settings settings, string deviceId);
        bool InitRoom(string deviceId);
        Room GetRoom(string deviceId);
    }
}
