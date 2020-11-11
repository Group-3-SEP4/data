using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebService.Models.Shared
{
    [Table("CarbonDioxideReading")]
    public partial class CarbonDioxideReading
    {
        public int CarbrId { get; set; }
        public int RoomId { get; set; }
        public int Timestamp { get; set; }
        public int Value { get; set; }

        public virtual Room Room { get; set; }
    }
}
