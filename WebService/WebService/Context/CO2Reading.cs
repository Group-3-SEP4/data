using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Context
{
    public class CO2Reading
    {
        public int reading { get; set; }

        public CO2Reading(int reading)
        {
            this.reading = reading;
        }
    }
}
