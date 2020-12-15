
using System.Collections.Generic;
using System.Linq;
using WebService.Repository.Context;
using WebService.Repository.Context.DatabaseSQL;

namespace WebService.Repository
{
    public interface IDbRepository
    {
        Measurement GetMeasurement(string deviceEUI);
        Settings GetSettings(string deviceEUI);
        Settings PostSettings(Settings settings);
        bool InitRoom(string deviceEUI);
        Room GetRoom(string deviceEUI);
        List<FMeasurementOverview> GetOverviewToday(string deviceEUI);
        List<FMeasurementOverview> GetOverviewLastWeek(string deviceEUI);
        DetailedMeasurements GetHistoricalOverview(string deviceEUI, string validFrom, string validTo);
        Room UpdateRoom(Room room);
    }
}
