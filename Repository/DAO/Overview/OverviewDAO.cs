using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebService.Repository.Context;
using WebService.Repository.Context.DatabaseSQL;

namespace WebService.Repository.DAO.Fact_Measurement
{
    public class OverviewDao : IOverviewDAO
    {
        private EnvironmentContext _context;

        public OverviewDao(EnvironmentContext context)
        {
            _context = context;
        }
        
        public OverviewModel GetOverviewToday(string deviceEUI)
        {
            return _context.OverviewModel.FromSqlRaw(
                "SELECT" +
                " MIN(fm.CarbonDioxide) AS co2Min, AVG(CAST(fm.CarbonDioxide AS FLOAT)) AS co2Avg,     MAX(fm.CarbonDioxide) AS co2Max," +
                " MIN(fm.HumidityPercentage) AS humiMin, AVG(CAST(fm.HumidityPercentage AS FLOAT)) AS humiAvg, MAX(fm.HumidityPercentage) AS humiMax," +
                " MIN(fm.Temperature) AS tempMin, AVG(fm.Temperature) AS tempAvg, MAX(fm.Temperature) AS tempMax" +
                " FROM DW.F_Measurement fm" +
                " INNER JOIN DW.DateDim d ON fm.DateDimKey = d.DateDimKey" +
                " INNER JOIN DW.DeviceDim dv ON fm.DeviceDimKey = dv.DeviceDimKey" +
                " WHERE d.Date >= DATEADD(DAY, DATEDIFF(DAY, 0, GETDATE()), 0)" +
                " AND dv.DeviceEUI = " +
                "{0}" +
                " AND NOT fm.Temperature = 0" +
                " AND NOT fm.HumidityPercentage = 0", deviceEUI).First();
        }

        public OverviewModel GetOverviewLastWeek(string deviceEUI)
        {
            return _context.OverviewModel.FromSqlRaw("SELECT" + 
                                          " MIN(fm.CarbonDioxide) AS co2Min, AVG(CAST(fm.CarbonDioxide AS FLOAT)) AS co2Avg, MAX(fm.CarbonDioxide) AS co2Max," +
                                          " MIN(fm.HumidityPercentage) AS humiMin, AVG(CAST(fm.HumidityPercentage AS FLOAT)) AS humiAvg, MAX(fm.HumidityPercentage) AS humiMax," +
                                          " MIN(fm.Temperature) AS tempMin, AVG(fm.Temperature) AS tempAvg, MAX(fm.Temperature) AS tempMax" +
                                          " FROM DW.F_Measurement fm" +
                                          " INNER JOIN DW.DateDim d ON fm.DateDimKey = d.DateDimKey" +
                                          " INNER JOIN DW.DeviceDim dv ON fm.DeviceDimKey = dv.DeviceDimKey" +
                                          " WHERE d.Date >= DATEADD(DAY, -7, GETDATE())" +
                                          " AND dv.DeviceEUI = " +
                                          "{0}" +
                                          " AND NOT fm.Temperature = 0" +
                                          " AND NOT fm.HumidityPercentage = 0", deviceEUI).First();
        }
    }
}
