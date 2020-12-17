using System;
using System.Linq;
using WebService.Repository.Context.DatabaseSQL;

namespace WebService.Repository.DAO.Settings
{
    public class SettingsDAO : ISettingsDAO
    {
        private EnvironmentContext context;

        public SettingsDAO(EnvironmentContext context)
        {
            this.context = context;
        }

        public Context.Settings GetSettings(string deviceEUI) {
            Context.Room roomWithId = context.Room.Where(m => m.DeviceEui.Equals(deviceEUI)).Single();
            Context.Settings settings = context.Settings.Find(roomWithId.SettingsId);
            return settings;
        }

        public Context.Settings PostSettings(Context.Settings settings) {
            var entity = context.Settings.FirstOrDefault(item => item.SettingsId == settings.SettingsId);

            if (entity != null)
            {
                entity.PpmMax = settings.PpmMax;
                entity.PpmMin = settings.PpmMin;
                entity.TemperatureSetpoint = settings.TemperatureSetpoint;
                entity.LastUpdated = DateTime.UtcNow;
                entity.SettingsId = settings.SettingsId;
                entity.SentToDevice = null;
                context.Settings.Update(entity);
                context.SaveChanges();
            }
            Context.Settings updatedSettings = context.Settings.FirstOrDefault(item => item.SettingsId == settings.SettingsId);
            return updatedSettings;
        }
    }
}
