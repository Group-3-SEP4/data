using System;
using System.Linq;
using WebService.Repository.Context;
using WebService.Repository.DAO.Measurement;
using WebService.Repository.DAO.Room;
using WebService.Repository.DAO.Settings;

namespace WebService.Repository
{
    public class DbRepository : IDbRepository
    {
        ISettingsDAO settingsDAO;
        IRoomDAO roomDAO;
        IMeasurementDAO measurementDAO;
        private EnviormentContext _context;

        public DbRepository(EnviormentContext context) {
            _context = context;
            this.measurementDAO = new MeasurementDAO(context);
            this.settingsDAO = new SettingsDAO(context);
            this.roomDAO = new RoomDAO(context);
        }

        public Measurement GetMeasurement(string deviceEUI) {
            return measurementDAO.GetMeasurement(deviceEUI);
        }

        public Settings GetSettings(string deviceEUI) {
            return settingsDAO.GetSettings(deviceEUI);
        }

        public Settings PostSettings(Settings settings) {
            return settingsDAO.PostSettings(settings);
        }

        public bool InitRoom(string deviceEUI) {
            return roomDAO.InitRoom(deviceEUI);
            }

        public Room GetRoom(string deviceEUI)
        {
            return roomDAO.GetRoom(deviceEUI);
        }
    }
}