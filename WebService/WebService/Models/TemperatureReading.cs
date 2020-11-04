using System;
using System.Collections.Generic;

namespace WebService.Models
{
    public partial class TemperatureReading
    {
        public int TemprId { get; set; }
        public int RoomId { get; set; }
        public DateTime Timestamp { get; set; }
        public double Value { get; set; }

        public virtual Room Room { get; set; }
    }
}
