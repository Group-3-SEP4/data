using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Repository.Context;
using WebService.Repository.Context.DatabaseSQL;

namespace WebService.Repository.DAO.Fact_Measurement
{
    interface IFactMeasurementDao
    {
        List<FMeasurementOverview> GetOverviewToday(string deviceEUI);
        
        List<FMeasurementOverview> GetOverviewLastWeek(string deviceEUI);
    }
}
