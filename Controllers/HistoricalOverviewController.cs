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
        public List<HistoricalOverview> GetHistoricalOverview([FromQuery(Name = "deviceEUI")] string deviceEUI)
        {
            return _repo.GetHistoricalOverview(deviceEUI);
        }
    }
}