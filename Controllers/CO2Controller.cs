using Microsoft.AspNetCore.Mvc;
using WebService.DAO.Repository;
using WebService.Models;

namespace WebService.Controllers
{
    /// <summary>
    /// This is the controller for getting or posting data related to co2. It has 2 get methods currently.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CO2Controller : ControllerBase
    {
        private readonly IDBRepository _repo;

        public CO2Controller(IDBRepository repo)
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
            return _repo.GetCO2Reading();
        }
        
        /// <summary>
        /// This method gets a specific value of CO2. I didn't write this
        /// </summary>
        /// <returns></returns>
        [HttpGet("value")]
        public int GetCurrentCO2Value()
        {
            return _repo.GetCO2Reading();
        }
    }
}