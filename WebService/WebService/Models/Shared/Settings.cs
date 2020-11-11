using System;
using System.Collections.Generic;

namespace WebService.Models.Shared
{
    public partial class Settings
    {
        public Settings()
        {
            Room = new HashSet<Room>();
        }

        public int SettingsId { get; set; }
        public DateTime LastUpdated { get; set; }
        public double TemperatureSetpoint { get; set; }
        public int PpmMin { get; set; }
        public int PpmMax { get; set; }

        public virtual ICollection<Room> Room { get; set; }
    }
}
