using System.Collections.Generic;
using WebService.Repository.Context;

namespace WebService.Repository.DAO.Fact_Measurement
{
    interface IOverviewDAO
    {
        OverviewModel GetOverviewToday(string deviceEUI);
        
        OverviewModel GetOverviewLastWeek(string deviceEUI);
    }
}
