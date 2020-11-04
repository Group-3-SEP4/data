using System;
using System.Collections.Generic;

namespace WebService.Models
{
    public partial class CarbonDioxideReading
    {
        public int CarbrId { get; set; }
        public int RoomId { get; set; }
        public int Timestamp { get; set; }
        public int Value { get; set; }

        public virtual Room Room { get; set; }
    }
}
