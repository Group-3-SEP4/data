using WebService.Repository.Context;


namespace WebService.Repository.DAO.Measurement
{
    interface IMeasurementDAO
    {
        Context.Measurement GetMeasurement(string deviceEUI);
    }
}
