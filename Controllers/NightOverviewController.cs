using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebService.Repository;
using WebService.Repository.Context;
using WebService.Repository.Context.DatabaseSQL;

namespace WebService.Controllers
{
    /// <summary>
    /// This is the controller for getting or posting data related to measurements.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class NightOverviewController : ControllerBase
    {
        private readonly IDbRepository _repo;

        public NightOverviewController(IDbRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// This method returns all measurements needed for the night overview.
        /// </summary>
        /// <returns> Object of measurement that contains temperature, humidity, co2, and servo position</returns>
        [HttpGet("Today")]
        public List<FMeasurementOverview> GetNightOverview([FromQuery(Name = "deviceEUI")] string deviceEUI)
        {
            return _repo.GetOverviewToday(deviceEUI);
        }

        [HttpGet("LastWeek")]
        public List<FMeasurementOverview> GetLastWeekOverview([FromQuery(Name = "deviceEUI")] string deviceEUI)
        {
            return _repo.GetOverviewLastWeek(deviceEUI);
        }
    }
}