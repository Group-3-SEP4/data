using System;
using System.Collections.Generic;

namespace WebService.Models
{
    public partial class Settings
    {
        public int SettingsId { get; set; }
        public DateTime LastUpdated { get; set; }
        public double TemperatureSetpoint { get; set; }
        public int PpmMin { get; set; }
        public int PpmMax { get; set; }
        public DateTime? SentToDevice { get; set; }
    }
}
