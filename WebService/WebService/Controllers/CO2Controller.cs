using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebService.Context;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CO2Controller : ControllerBase
    {
        private CO2Context _context;

        public CO2Controller(CO2Context context)
        {
            _context = context;
        }

        [HttpGet]
        public double GetCurrentCO2()
        {
            var rng = new Random();
            return rng.Next(410, 1100);
        }
    }
}
