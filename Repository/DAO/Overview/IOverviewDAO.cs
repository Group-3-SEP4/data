using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebService.Repository.Context;
using WebService.Repository.Context.DatabaseSQL;

namespace WebService.Repository.DAO.Fact_Measurement
{
    interface IOverviewDAO
    {
        List<OverviewModel> GetOverviewToday(string deviceEUI);
        
        List<OverviewModel> GetOverviewLastWeek(string deviceEUI);
    }
}
