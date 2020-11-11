using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebService.Context;
using WebService.Models;

namespace WebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CO2Controller : ControllerBase
    {
        private EnviormentContext _context;

        public CO2Controller(EnviormentContext context)
        {
            _context = context;
        }

        [HttpGet]
        public int GetCurrentCO2()
        {
            var rng = new Random();
            _context.Add(new Settings {LastUpdated = DateTime.Now, TemperatureSetpoint = rng.Next(4, 30), PpmMin = rng.Next(100, 410), PpmMax = rng.Next(410, 3000) });
            _context.SaveChanges();
            //This is what we will need for live CO2
            /*
            SELECT CHARGEID, CHARGETYPE, MAX(SERVICEMONTH) AS "MostRecentServiceMonth"
            FROM INVOICE
            GROUP BY CHARGEID, CHARGETYPE
            */
            Settings setting = _context.Settings.Single(b => b.SettingsId == 1);
            Console.WriteLine("This is a new settings entity " + setting.SettingsId + " " + setting.LastUpdated + " " + setting.PpmMin + " " + setting.PpmMin );

            return rng.Next(410, 1100); //Replace when fully implemented 
        }
    }
}
