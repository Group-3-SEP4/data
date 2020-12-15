using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebService.Repository.Context;
using WebService.Repository.Context.DatabaseSQL;

namespace WebService.Repository.DAO.Historical
{
    public class HistoricalMeasurementDAO : IHistoricalMeasurementDAO
    {
        private EnviormentContext _context;

        public HistoricalMeasurementDAO(EnviormentContext context)
        {
            _context = context;
        }

        public List<HistoricalOverview> GetHistoricalOverview(string deviceEUI)
        {
            List<DetailedCo2> detailedCo2s = _context.DetailedCo2.FromSqlRaw(
                "SELECT " +
                "DATEADD(HOUR, t.Hour, CAST(d.Date AS DATETIME)) AS timestamp, " +
                "AVG(fm.CarbonDioxide) AS value " +
                "FROM DW.F_Measurement fm " +
                "INNER JOIN DW.DateDim d ON fm.DateDimKey = d.DateDimKey " +
                "INNER JOIN DW.TimeDim t ON fm.TimeDimKey = t.TimeDimKey " +
                "INNER JOIN DW.DeviceDim dv ON fm.DeviceDimKey = dv.DeviceDimKey " +
                "WHERE dv.DeviceEUI = '0004A30B00219CB5' " +
                "AND d.Date <= '2020-12-15' " +
                "AND d.Date >= '2020-11-05' " +
                "GROUP BY t.Hour, d.Date " +
                "ORDER BY d.Date asc, t.Hour asc ").ToList();
            
            List<DetailedHumidity> detailedHumiditys = _context.DetailedHumidity.FromSqlRaw(
                "SELECT " +
                "DATEADD(HOUR, t.Hour, CAST(d.Date AS DATETIME)) AS timestamp, " +
                "AVG(fm.HumidityPercentage) AS value " +
                "FROM DW.F_Measurement fm " +
                "INNER JOIN DW.DateDim d ON fm.DateDimKey = d.DateDimKey " +
                "INNER JOIN DW.TimeDim t ON fm.TimeDimKey = t.TimeDimKey " +
                "INNER JOIN DW.DeviceDim dv ON fm.DeviceDimKey = dv.DeviceDimKey " +
                "WHERE dv.DeviceEUI = '0004A30B00219CB5' " +
                "AND d.Date <= '2020-12-15' " +
                "AND d.Date >= '2020-12-07' " +
                "GROUP BY t.Hour, d.Date " +
                "ORDER BY d.Date asc, t.Hour asc").ToList();
            

            List<DetailedTemperature> detailedTemperatures = _context.DetailedTemperature.FromSqlRaw(
                "SELECT " +
                "DATEADD(HOUR, t.Hour, CAST(d.Date AS DATETIME)) AS timestamp, " +
                "AVG(fm.Temperature) AS value " +
                "FROM DW.F_Measurement fm " +
                "INNER JOIN DW.DateDim d ON fm.DateDimKey = d.DateDimKey " +
                "INNER JOIN DW.TimeDim t ON fm.TimeDimKey = t.TimeDimKey " +
                "INNER JOIN DW.DeviceDim dv ON fm.DeviceDimKey = dv.DeviceDimKey " +
                "WHERE dv.DeviceEUI = '0004A30B00219CB5' " +
                "AND d.Date <= '2020-12-15' " +
                "AND d.Date >= '2020-12-07' " +
                "GROUP BY t.Hour, d.Date " +
                "ORDER BY d.Date asc, t.Hour asc").ToList();
            DetailedMeasurements measurements = new DetailedMeasurements();
            measurements.detailedCo2List = detailedCo2s;
            measurements.detailedHumidityList = detailedHumiditys;
            measurements.detailedTemperatureList = detailedTemperatures;
            
            Console.Write(5555);

            return null;
        }
    }
}