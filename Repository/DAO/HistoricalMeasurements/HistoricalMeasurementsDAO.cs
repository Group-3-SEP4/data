using WebService.Repository.Context.DatabaseSQL;

namespace WebService.Repository.DAO.HistoricalMeasurements
{
    public class HistoricalMeasurementsDAO : IHistoricalMeasurementsDAO
    {
        private EnviormentContext _context;

        public HistoricalMeasurementsDAO(EnviormentContext context)
        {
            _context = context;
        }
    }
}