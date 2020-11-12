using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebService.Context;
using WebService.DAO;
using WebService.Models;
using WebService.Models.Shared;

namespace WebService.Controllers
{
    /// <summary>
    /// This is the controller for getting or posting data related to co2. It has 2 get methods currently
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class CO2Controller : ControllerBase
    {
        
        private EnviormentContext _context;
        private DbRepository _Repo;

        public CO2Controller(EnviormentContext context)
        {
            _context = context;
            _Repo = new DbRepository(_context);
        }

        /// <summary>
        /// This method returns current co2 in the room.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public CarbonDioxideReading GetCurrentCO2()
        {
            return _Repo.GetCO2Reading();
        }
        
        /// <summary>
        /// This method gets a specific value of co2 or whatever. I didnt write this
        /// </summary>
        /// <returns></returns>
        [HttpGet("value")]
        public int GetCurrentCO2Value()
        {
            
            return _Repo.GetCO2Reading().Value;
        }
    } 
}
