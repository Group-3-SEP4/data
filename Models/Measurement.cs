using System;
using System.Collections.Generic;

namespace WebService.Models
{
    public partial class Measurement
    {
        public int MeasurementId { get; set; }
        public DateTime Timestamp { get; set; }
        public int HumidityPercentage { get; set; }
        public int CarbonDioxide { get; set; }
        public double Temperature { get; set; }
        public int ServoPositionPercentage { get; set; }
        public string DeviceId { get; set; }
    }
}
