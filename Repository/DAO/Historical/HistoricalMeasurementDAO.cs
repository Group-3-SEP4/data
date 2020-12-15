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
            return _context.HistoricalOverview.FromSqlRaw("SELECT" +
                                                          " AVG(fm.CarbonDioxide) AS co2Avg" +
                                                          ", AVG(fm.HumidityPercentage) AS humiAvg" +
                                                          ", AVG(fm.Temperature) AS tempAvg" +
                                                          ", d.Date AS date" +
                                                          " FROM DW.F_Measurement fm" +
                                                          " INNER JOIN DW.DateDim d ON fm.DateDimKey = d.DateDimKey" +
                                                          " INNER JOIN DW.DeviceDim dv ON fm.DeviceDimKey = dv.DeviceDimKey" +
                                                          " WHERE d.Date <= GETDATE()" +
                                                          " AND dv.DeviceEUI = {0}" +
                                                          " GROUP BY d.Date" +
                                                          " ORDER BY d.Date asc", deviceEUI).ToList();
        }
    }
}