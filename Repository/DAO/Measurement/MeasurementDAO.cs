

using System.Linq;
using WebService.Repository.Context;
using WebService.Repository.Context.DatabaseSQL;

namespace WebService.Repository.DAO.Measurement
{
    public class MeasurementDAO : IMeasurementDAO
    {
        private EnviormentContext context;

        public MeasurementDAO(EnviormentContext context)
        {
            this.context = context;
        }

        public Context.Measurement GetMeasurement(string deviceEUI)
        {
            return context.Measurement.AsEnumerable().Where(m => m.DeviceEui.Equals(deviceEUI)).Last();
        }
    }
}
