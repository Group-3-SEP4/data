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
        private ApplicationDBContext _context;

        public CO2Controller(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public int GetCurrentCO2()
        {
            var rng = new Random();
            return rng.Next(410, 1100);
        }
    }
}
