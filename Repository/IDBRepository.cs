
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
    }
}
