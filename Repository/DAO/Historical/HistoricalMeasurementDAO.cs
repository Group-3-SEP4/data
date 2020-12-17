using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebService.Repository.Context;
using WebService.Repository.Context.DatabaseSQL;

namespace WebService.Repository.DAO.Historical
{
    public class HistoricalMeasurementDAO : IHistoricalMeasurementDAO
    {
        private EnvironmentContext _context;

        public HistoricalMeasurementDAO(EnvironmentContext context)
        {
            _context = context;
        }

        public DetailedMeasurements GetHistoricalOverview(string deviceEUI, string validFrom, string validTo)
        {
            List<DetailedTemperature> detailedTemperatures = _context.DetailedTemperature.FromSqlRaw(
                "SELECT " +
                "DATEADD(HOUR, t.Hour, CAST(d.Date AS DATETIME)) AS timestamp, " +
                "AVG(fm.Temperature) AS value " +
                "FROM DW.F_Measurement fm " +
                "INNER JOIN DW.DateDim d ON fm.DateDimKey = d.DateDimKey " +
                "INNER JOIN DW.TimeDim t ON fm.TimeDimKey = t.TimeDimKey " +
                "INNER JOIN DW.DeviceDim dv ON fm.DeviceDimKey = dv.DeviceDimKey " +
                "WHERE dv.DeviceEUI = {0} " +
                "AND d.Date <= {1} " +
                "AND d.Date >= {2} " +
                "GROUP BY t.Hour, d.Date " +
                "ORDER BY d.Date asc, t.Hour asc", deviceEUI, validTo, validFrom).ToList();

            List<DetailedHumidity> detailedHumiditys = _context.DetailedHumidity.FromSqlRaw(
                "SELECT " +
                "DATEADD(HOUR, t.Hour, CAST(d.Date AS DATETIME)) AS timestamp, " +
                "AVG(fm.HumidityPercentage) AS value " +
                "FROM DW.F_Measurement fm " +
                "INNER JOIN DW.DateDim d ON fm.DateDimKey = d.DateDimKey " +
                "INNER JOIN DW.TimeDim t ON fm.TimeDimKey = t.TimeDimKey " +
                "INNER JOIN DW.DeviceDim dv ON fm.DeviceDimKey = dv.DeviceDimKey " +
                "WHERE dv.DeviceEUI = {0} " +
                "AND d.Date <= {1} " +
                "AND d.Date >= {2} " +
                "GROUP BY t.Hour, d.Date " +
                "ORDER BY d.Date asc, t.Hour asc", deviceEUI, validTo, validFrom).ToList();

            List<DetailedCo2> detailedCo2s = _context.DetailedCo2.FromSqlRaw(
                "SELECT " +
                "DATEADD(HOUR, t.Hour, CAST(d.Date AS DATETIME)) AS timestamp, " +
                "AVG(fm.CarbonDioxide) AS value " +
                "FROM DW.F_Measurement fm " +
                "INNER JOIN DW.DateDim d ON fm.DateDimKey = d.DateDimKey " +
                "INNER JOIN DW.TimeDim t ON fm.TimeDimKey = t.TimeDimKey " +
                "INNER JOIN DW.DeviceDim dv ON fm.DeviceDimKey = dv.DeviceDimKey " +
                "WHERE dv.DeviceEUI = {0} " +
                "AND d.Date <= {1} " +
                "AND d.Date >= {2} " +
                "GROUP BY t.Hour, d.Date " +
                "ORDER BY d.Date asc, t.Hour asc ", deviceEUI, validTo, validFrom).ToList();

            DetailedMeasurements measurements = new DetailedMeasurements();
            measurements.detailedCo2List = detailedCo2s;
            measurements.detailedHumidityList = detailedHumiditys;
            measurements.detailedTemperatureList = detailedTemperatures;

            return measurements;
        }
    }
}