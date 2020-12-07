using Microsoft.AspNetCore.Mvc;
using WebService.DAO.Repository;
using WebService.Models;

namespace WebService.Controllers
{
    /// <summary>
    /// This is the controller for getting or posting data related to measurements.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class MeasurementsController : ControllerBase
    {
        private readonly IDbRepository _repo;

        public MeasurementsController(IDbRepository repo) {
            _repo = repo;
        }

        /// <summary>
        /// This method returns all measurements. Specifically most recent ones. Uses device id to do it.
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns>Object of measurement that contains temperature, humidity, co2, and servo position</returns>
        [HttpGet]
        public Measurement GetCurrentMeasurement([FromQuery(Name = "deviceId")] string deviceId) {
            return _repo.GetMeasurement(deviceId);
        }
    }
}