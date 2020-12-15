using System.Collections.Generic;
using WebService.Repository.Context;

namespace WebService.Repository.DAO.Historical
{
    public interface IHistoricalMeasurementDAO
    {
        DetailedMeasurements GetHistoricalOverviewCO2(string deviceEUI, string validFrom, string validTo);
    }
}