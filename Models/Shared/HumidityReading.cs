﻿using System;
using System.Collections.Generic;

namespace WebService.Models.Shared
{
    public partial class HumidityReading
    {
        public int HumrId { get; set; }
        public int RoomId { get; set; }
        public int Timestamp { get; set; }
        public int Value { get; set; }

        public virtual Room Room { get; set; }
    }
}
