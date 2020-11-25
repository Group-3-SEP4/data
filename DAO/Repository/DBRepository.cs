using System;
using System.Collections.Generic;
using System.Linq;
using WebService.DAO.Context;
using WebService.Models;

namespace WebService.DAO.Repository
{
    public class DbRepository : IDbRepository
    {
        private EnvironmentContext _context;

        public DbRepository(EnvironmentContext context)
        {
            _context = context;
        }

        public int GetCo2Reading()
        {
            return _context.Measurement.AsEnumerable().Last().CarbonDioxide;
        }

        public Settings GetSettings()
        {
            return _context.Settings.AsEnumerable().Last();
        }

        public Settings PostSettings(Settings settings)
        {
            var entity = _context.Settings.FirstOrDefault(item => item.SettingsId == settings.SettingsId);

            if (entity != null)
            {
                entity.PpmMax = settings.PpmMax;
                entity.PpmMin = settings.PpmMin;
                entity.TemperatureSetpoint = settings.TemperatureSetpoint;
                entity.LastUpdated = DateTime.Now;
                entity.SettingsId = settings.SettingsId;
                _context.Settings.Update(entity);
                _context.SaveChanges();
            }
            Settings student = _context.Settings.FirstOrDefault(item => item.SettingsId == settings.SettingsId);
            return student;
        }
    }
}