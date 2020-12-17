using Microsoft.AspNetCore.Mvc;
using WebService.Repository;
using WebService.Repository.Context;

namespace WebService.Controllers
{
    /// <summary>
    /// This controller is for getting the values of a specified time period
    /// that go to show the avg,min and max of humidity, temperature and
    /// carbon dioxide every hour of that time period
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HistoricalOverviewController : ControllerBase
    {
        private readonly IDbRepository _repo;

        public HistoricalOverviewController(IDbRepository repo)
        {
            _repo = repo;
        }
        /// <summary>
        /// Get values for specified date
        /// </summary>
        /// <param name="deviceEUI"></param>
        /// <param name="validFrom"></param>
        /// <param name="validTo"></param>
        /// <returns>Object that holds a lists of humidity, temperature, and carbon dioxide</returns>
        [HttpGet]
        public DetailedMeasurements GetHistoricalOverview([FromQuery(Name = "deviceEUI")] string deviceEUI
            , [FromQuery(Name = "validFrom")] string validFrom
            , [FromQuery(Name = "validTo")] string validTo)
        {
            return _repo.GetHistoricalOverview(deviceEUI, validFrom, validTo);
        }
    }
}