using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebService.Context
{
    public class CO2Reading
    {
        public int Reading { get; set; }

        public CO2Reading(int reading)
        {
            this.Reading = reading;
        }
    }
}
