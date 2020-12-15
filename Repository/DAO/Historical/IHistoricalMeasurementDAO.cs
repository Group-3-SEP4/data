using System.Collections.Generic;
using WebService.Repository.Context;

namespace WebService.Repository.DAO.Historical
{
    public interface IHistoricalMeasurementDAO
    {
        List<HistoricalOverview> GetHistoricalOverview(string deviceEUI);
    }
}