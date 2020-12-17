using System.Collections.Generic;
using WebService.Repository.Context;

namespace WebService.Repository
{
    public interface IDbRepository
    {
        Measurement GetMeasurement(string deviceEUI);
        Settings GetSettings(string deviceEUI);
        Settings PostSettings(Settings settings);
        bool InitRoom(string deviceEUI);
        Room GetRoom(string deviceEUI);
        List<OverviewModel> GetOverviewToday(string deviceEUI);
        List<OverviewModel> GetOverviewLastWeek(string deviceEUI);
        DetailedMeasurements GetHistoricalOverview(string deviceEUI, string validFrom, string validTo);
        Room UpdateRoom(Room room);
    }
}
