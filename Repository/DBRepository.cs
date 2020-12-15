﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        IFactMeasurementDao factMeasurementDao;
        private IHistoricalMeasurementDAO historicalMeasurementDao;
        private EnviormentContext _context;

        public DbRepository(EnviormentContext context) {
            _context = context;
            this.measurementDAO = new MeasurementDAO(context);
            this.settingsDAO = new SettingsDAO(context);
            this.roomDAO = new RoomDAO(context);
            this.factMeasurementDao = new FactMeasurementDao(context);
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

        public List<FMeasurementOverview> GetOverviewToday(string deviceEUI)
        {
            return factMeasurementDao.GetOverviewToday(deviceEUI);
        }
        
        public List<FMeasurementOverview> GetOverviewLastWeek(string deviceEUI)
        {
            return factMeasurementDao.GetOverviewLastWeek(deviceEUI);
        }

        public List<HistoricalOverview> GetHistoricalOverview(string deviceEUI)
        {
            return historicalMeasurementDao.GetHistoricalOverview(deviceEUI);
        }
    }
}