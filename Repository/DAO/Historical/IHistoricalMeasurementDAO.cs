using System.Collections.Generic;
using WebService.Repository.Context;

namespace WebService.Repository.DAO.Historical
{
    public interface IHistoricalMeasurementDAO
    {
        DetailedMeasurements GetHistoricalOverview(string deviceEUI, string validFrom, string validTo);
    }
}