using System;
using System.Linq;
using WebService.Models;

namespace WebService.DAO.Repository
{
    public class DbRepository : IDbRepository
    {
        private EnviormentContext _context;
        private int ppmMin;
        private int ppmMax;
        private float temperatureSetPoint;
        

        public DbRepository(EnviormentContext context) {
            _context = context;
            this.ppmMin = 400;
            this.ppmMax = 5000;
            this.temperatureSetPoint = 18;
        }

        public Measurement GetMeasurement(string deviceId) {
            return _context.Measurement.AsEnumerable().Where(m => m.DeviceId.Equals(deviceId)).Last();
        }

        public Settings GetSettings(string deviceId) {
            Room roomWithId = _context.Room.Where(m => m.DeviceEui.Equals(deviceId)).Single();
            Settings settings = _context.Settings.Find(roomWithId.SettingsId);
            return settings;
        }

        public Settings PostSettings(Settings settings, string deviceId) {
            var entity = _context.Settings.FirstOrDefault(item => item.SettingsId == settings.SettingsId);

            if (entity != null)
            {
                entity.PpmMax = settings.PpmMax;
                entity.PpmMin = settings.PpmMin;
                entity.TemperatureSetpoint = settings.TemperatureSetpoint;
                entity.LastUpdated = DateTime.Now;
                entity.SettingsId = settings.SettingsId;
                entity.SentToDevice = new DateTime(1754, 1, 1, 1, 1, 1);
                _context.Settings.Update(entity);
                _context.SaveChanges();
            }
            Settings student = _context.Settings.FirstOrDefault(item => item.SettingsId == settings.SettingsId);
            return student;
        }

        public bool InitRoom(string deviceId) {
            Room roomForDevice = null;
            try
            {
                roomForDevice = _context.Room.AsEnumerable().Where(r => r.DeviceEui.Equals(deviceId)).Last();
            }
            catch (InvalidOperationException e)
            {
            }
            
            if (roomForDevice != null)
                return false;
            else {
                Settings newSettings = new Settings();
                newSettings.TemperatureSetpoint = temperatureSetPoint;
                newSettings.PpmMax = ppmMax;
                newSettings.PpmMin = ppmMin;
                newSettings.LastUpdated = DateTime.Now;

                _context.Settings.Add(newSettings);
                _context.SaveChanges();

                Room newRoom = new Room();
                newRoom.DeviceEui = deviceId;
                newRoom.Name = "New device";
                newRoom.SettingsId = newSettings.SettingsId;
                _context.Room.Add(newRoom);
                _context.SaveChanges();
                return true;
            }
        }

        public Room GetRoom(string deviceId)
        {
            return _context.Room.Where(room => room.DeviceEui.Equals(deviceId)).Single();
        }
    }
}