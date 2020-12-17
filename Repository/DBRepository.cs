using System.Collections.Generic;
using WebService.Repository.Context;
using WebService.Repository.Context.DatabaseSQL;
using WebService.Repository.DAO.Fact_Measurement;
using WebService.Repository.DAO.Historical;
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
        IOverviewDAO _overviewDao;
        private IHistoricalMeasurementDAO historicalMeasurementDao;
        private EnvironmentContext _context;

        public DbRepository(EnvironmentContext context) {
            _context = context;
            this.measurementDAO = new MeasurementDAO(context);
            this.settingsDAO = new SettingsDAO(context);
            this.roomDAO = new RoomDAO(context);
            this._overviewDao = new OverviewDao(context);
            this.historicalMeasurementDao = new HistoricalMeasurementDAO(context);
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
        
        public Room UpdateRoom(Room room)
        {
            return roomDAO.UpdateRoom(room);
        }

        public List<OverviewModel> GetOverviewToday(string deviceEUI)
        {
            return _overviewDao.GetOverviewToday(deviceEUI);
        }
        
        public List<OverviewModel> GetOverviewLastWeek(string deviceEUI)
        {
            return _overviewDao.GetOverviewLastWeek(deviceEUI);
        }

        public DetailedMeasurements GetHistoricalOverview(string deviceEUI, string validFrom, string validTo)
        {
            return historicalMeasurementDao.GetHistoricalOverview(deviceEUI, validFrom, validTo);
        }
    }
}