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

        [HttpGet]
        public CarbonDioxideReading GetCurrentCO2()
        {
            return _Repo.GetCO2Reading();
        }
        
        [HttpGet("value")]
        public int GetCurrentCO2Value()
        {
            
            return _Repo.GetCO2Reading().Value;
        }
    } 
}
