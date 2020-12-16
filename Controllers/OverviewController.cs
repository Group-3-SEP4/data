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
    public class OverviewController : ControllerBase
    {
        private readonly IDbRepository _repo;

        public OverviewController(IDbRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// This method returns all measurements needed for the night overview.
        /// </summary>
        /// <returns> Object of measurement that contains temperature, humidity, co2, and servo position</returns>
        /// <summary>
        /// This method returns all measurements needed for the night overview.
        /// </summary>
        /// <returns> Object of measurement that contains temperature, humidity, co2, and servo position</returns>
        [HttpGet]
        public ActionResult MyGetNightOverview([FromQuery(Name = "deviceEUI")] string deviceEUI)
        {
            OverviewModel nightOverview = _repo.GetOverviewToday(deviceEUI).First();
            OverviewModel weekOverview = _repo.GetOverviewLastWeek(deviceEUI).First();

            var overview = new
            {
                tempMin = nightOverview.tempMin,
                tempMax = nightOverview.tempMax,
                tempAvg = nightOverview.tempAvg,
                humiMin = nightOverview.humiMin,
                humiMax = nightOverview.humiMax,
                humiAvg = nightOverview.humiAvg,
                co2Min = nightOverview.co2Min,
                co2Max = nightOverview.co2Max,
                co2Avg = nightOverview.co2Avg,
                tempMin7days = weekOverview.tempMin,
                tempMax7days = weekOverview.tempMax,
                tempAvg7days = weekOverview.tempAvg,
                humiMin7days = weekOverview.humiMin,
                humiMax7days = weekOverview.humiMax,
                humiAvg7days = weekOverview.humiAvg,
                co2Min7days = weekOverview.co2Min,
                co2Max7days = weekOverview.co2Max,
                co2Avg7days = weekOverview.co2Avg
            };

            return Ok(overview);
        }
    }
}