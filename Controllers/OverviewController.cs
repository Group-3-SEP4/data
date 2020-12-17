using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebService.Repository;
using WebService.Repository.Context;

namespace WebService.Controllers
{
    /// <summary>
    /// This controller is for obtaining overview information for the night overview functionality 
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
        /// This method gets the overview of the night and week for the night overview page on android device
        /// Specifically gets the avg, min and max of both last day and week
        /// </summary>
        /// <param name="deviceEUI"></param>
        /// <returns>An anonymous object that holds both values for today and past week</returns>
        [HttpGet]
        public ActionResult GetOverview([FromQuery(Name = "deviceEUI")] string deviceEUI)
        {
            OverviewModel nightOverview = _repo.GetOverviewToday(deviceEUI);
            OverviewModel weekOverview = _repo.GetOverviewLastWeek(deviceEUI);

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