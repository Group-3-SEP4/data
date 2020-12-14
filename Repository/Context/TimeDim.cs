using System;
using System.Collections.Generic;

namespace WebService.Repository.Context.DatabaseSQL
{
    public partial class TimeDim
    {
        public TimeDim()
        {
            FMeasurement = new HashSet<FMeasurement>();
        }

        public int TimeDimKey { get; set; }
        public TimeSpan Time { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }

        public virtual ICollection<FMeasurement> FMeasurement { get; set; }
    }
}
