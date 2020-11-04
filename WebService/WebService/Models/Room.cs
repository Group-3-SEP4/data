using System;
using System.Collections.Generic;

namespace WebService.Models
{
    public partial class Room
    {
        public Room()
        {
            CarbonDioxideReading = new HashSet<CarbonDioxideReading>();
            HumidityReading = new HashSet<HumidityReading>();
            TemperatureReading = new HashSet<TemperatureReading>();
        }

        public int RoomId { get; set; }
        public int SettingsId { get; set; }
        public string Name { get; set; }

        public virtual Settings Settings { get; set; }
        public virtual ICollection<CarbonDioxideReading> CarbonDioxideReading { get; set; }
        public virtual ICollection<HumidityReading> HumidityReading { get; set; }
        public virtual ICollection<TemperatureReading> TemperatureReading { get; set; }
    }
}
