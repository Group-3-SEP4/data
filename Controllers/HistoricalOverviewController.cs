using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebService.Repository;
using WebService.Repository.Context;

namespace WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HistoricalOverviewController : ControllerBase
    {
        private readonly IDbRepository _repo;

        public HistoricalOverviewController(IDbRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public DetailedMeasurements GetHistoricalOverview([FromQuery(Name = "deviceEUI")] string deviceEUI
            , [FromQuery(Name = "validFrom")] string validFrom
            , [FromQuery(Name = "validTo")] string validTo)
        {
            return _repo.GetHistoricalOverview(deviceEUI, validFrom, validTo);
        }
    }
}