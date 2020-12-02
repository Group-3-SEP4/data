using Microsoft.AspNetCore.Mvc;
using WebService.DAO.Repository;

namespace WebService.Controllers
{
    /// <summary>
    /// This is the controller for getting or posting data related to co2. It has 2 get methods currently.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CO2Controller : ControllerBase
    {
        private readonly IDbRepository _repo;

        public CO2Controller(IDbRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// This method returns current co2 in the room.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public int GetCurrentCO2()
        {
            return _repo.GetCo2Reading();
        }
    }
}