using System;

namespace WebService.Repository.Context
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
