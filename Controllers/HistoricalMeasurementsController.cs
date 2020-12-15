using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers
{
    public class HistoricalMeasurementsController : Controller
    {
        /// <summary>
        /// This is the controller for getting or posting data related to measurements.
        /// </summary>
        [ApiController]
        [Route("[controller]")]
        public class MeasurementsController : ControllerBase {
            private readonly IDbRepository _repo;

            public MeasurementsController(IDbRepository repo) {
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