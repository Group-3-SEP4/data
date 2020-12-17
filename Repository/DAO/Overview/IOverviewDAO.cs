using System.Collections.Generic;
using WebService.Repository.Context;

namespace WebService.Repository.DAO.Fact_Measurement
{
    interface IOverviewDAO
    {
        List<OverviewModel> GetOverviewToday(string deviceEUI);
        
        List<OverviewModel> GetOverviewLastWeek(string deviceEUI);
    }
}
