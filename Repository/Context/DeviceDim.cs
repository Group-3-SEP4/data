using System;
using System.Collections.Generic;

namespace WebService.Repository.Context.DatabaseSQL
{
    public partial class DeviceDim
    {
        public DeviceDim()
        {
            FMeasurement = new HashSet<FMeasurement>();
        }

        public int DeviceDimKey { get; set; }
        public string DeviceEui { get; set; }
        public int RoomId { get; set; }
        public string Name { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }

        public virtual ICollection<FMeasurement> FMeasurement { get; set; }
    }
}
