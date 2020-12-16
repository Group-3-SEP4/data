

using System.Linq;
using WebService.Repository.Context;
using WebService.Repository.Context.DatabaseSQL;

namespace WebService.Repository.DAO.Measurement
{
    public class MeasurementDAO : IMeasurementDAO
    {
        private EnvironmentContext context;

        public MeasurementDAO(EnvironmentContext context)
        {
            this.context = context;
        }

        public Context.Measurement GetMeasurement(string deviceEUI)
        {
            return context.Measurement.AsEnumerable().Where(m => m.DeviceEui.Equals(deviceEUI)).Last();
        }
    }
}
