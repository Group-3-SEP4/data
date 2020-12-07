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
    public class MeasurementController : ControllerBase {
        private readonly IDbRepository _repo;

        public MeasurementController(IDbRepository repo) {
            _repo = repo;
        }

        /// <summary>
        /// This method returns all measurements. Specifically most recent ones. Uses device id to do it.
        /// </summary>
        /// <param name="deviceEUI"></param>
        /// <returns>Object of measurement that contains temperature, humidity, co2, and servo position</returns>
        [HttpGet]
        public Measurement GetCurrentMeasurement([FromQuery(Name = "deviceEUI")] string deviceEUI) {
            return _repo.GetMeasurement(deviceEUI);
        }
    }
}